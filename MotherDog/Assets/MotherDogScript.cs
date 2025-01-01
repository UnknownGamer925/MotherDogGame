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

    [SerializeField] private GameObject puppyInMouth = null;
    //private Animator animator;

    Camera cam;

    // Observer pattern
    public delegate void PuppyCheckDelegate(int x);
    public event PuppyCheckDelegate PuppyCheck;

    public delegate void DogbedCallDelegate(GameObject puppy);
    public event DogbedCallDelegate DogbedCall;

    public enum State
    {
        Empty,
        Holding,
    }
    public State state;


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
        if (pickup_list != null && enable == true && pickup_list.Count > 0)
        {
            puppyInMouth = pickup_list[0];
            puppyInMouth.GetComponent<PuppyScript>().Pickup(true);
            puppyInMouth.transform.parent = transform;
            puppyInMouth.transform.position = transform.position + transform.forward + new Vector3 (0, 0.5f, 0.5f);
            state = State.Holding;
        }
        else if (pickup_list != null && enable == false)
        {
            GameObject spawner = GameObject.Find("PuppySpawner");
            puppyInMouth.GetComponent<PuppyScript>().Pickup(false);
            if (spawner != null)
            {
                puppyInMouth.transform.parent = spawner.transform;
            }
            else
            {
                puppyInMouth.transform.parent = null;
            }
            pickup_list.Clear();
            state = State.Empty;
        }
        else
        {
            Debug.Log("No dogs in sight");
            state = State.Empty;
        }

    }

    private void Action()
    {
        if (state == State.Empty)
        {

            Pickup(true);
        }
        else if (state == State.Holding)
        {
            if (DogbedCall == null)
            {
                Pickup(false);
            }
            else
            {
                DogbedCall(puppyInMouth);
                pickup_list.Clear();
                state = State.Empty;
            }
            
            
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        //animator = transform.GetChild(0).GetComponent<Animator>();
        state = State.Empty;

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
            Action();
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
            pickup_list.Clear();
        }
    }
}
