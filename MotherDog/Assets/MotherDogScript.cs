using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class MotherDogScript : MonoBehaviour, iDog
{
    [SerializeField] int MOVE_SPEED = 20;
    [SerializeField] int ROTATE_SPEED = 5;
    private float cooldown = 0;
    private List<GameObject> pickup_list = new List<GameObject>();
    private bool pickedup = false;

    Camera cam;

    // Observer pattern
    public delegate void PuppyCheckDelegate(int x);
    public event PuppyCheckDelegate PuppyCheck;


    public void Movement()
    { 
        //Local variables
        Vector3 relative_forward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z);
        Vector3 relative_right = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z);
        Quaternion RotateTo = Quaternion.identity;
       
        // Rotate & Move player along x-axis relative to camera-facing direction
        if (Input.GetAxis("Horizontal") != 0)
        {     
            //Rotate
            RotateTo = Quaternion.LookRotation(Input.GetAxis("Horizontal") * relative_right);
            transform.rotation = Quaternion.Slerp(transform.rotation, RotateTo, Time.deltaTime * ROTATE_SPEED);

            //Move
            transform.position += Input.GetAxis("Horizontal") * new Vector3(cam.transform.right.x, 0f, cam.transform.right.z) * MOVE_SPEED * Time.deltaTime;
        }

        // Rotate & Move player along z-axis relative to camera-facing direction
        if (Input.GetAxis("Vertical") != 0)
        {
            //Rotate
            RotateTo = Quaternion.LookRotation(Input.GetAxis("Vertical") * relative_forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, RotateTo, Time.deltaTime * ROTATE_SPEED);
            
            //Move
            transform.position += Input.GetAxis("Vertical") * new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z) * MOVE_SPEED * Time.deltaTime;
        }
    }


    public void HandleComms(bool enable)
    {
        if (PuppyCheck != null && cooldown <= 0)
        {
            PuppyCheck(2);
            cooldown = 5f;
        }
        else if (cooldown > 0)
        {
            Debug.Log("-----COOLDOWN ACTIVE-----");
        }
        else
        {
            Debug.Log("-----NO NEARBY PUPS-----");
        }
    }

    public void Pickup(bool enable) 
    {
        if (pickup_list != null && enable == true)
        {
            pickup_list[0].GetComponent<PuppyScript>().Pickup(true);
            pickup_list[0].transform.parent = transform;
            pickup_list[0].transform.position = transform.position + transform.forward + new Vector3 (0, 0.5f, 0.5f);
        }
        else if (pickup_list != null && enable == false)
        {
            GameObject spawner = GameObject.Find("PuppySpawner");
            pickup_list[0].GetComponent<PuppyScript>().Pickup(false);
            if (spawner != null)
            {
                pickup_list[0].transform.parent = spawner.transform;
            }
            else
            {
                pickup_list[0].transform.parent = null;
            }
            

        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleComms(true);
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (pickedup == false)
            {
                Pickup(true);
                pickedup = true;
            }
            else
            {
                Pickup(false);
                pickedup = false;
            }

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag ==  "Puppy")
        {
            pickup_list.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Puppy")
        {
            pickup_list.Remove(other.gameObject);
        }
    }
}
