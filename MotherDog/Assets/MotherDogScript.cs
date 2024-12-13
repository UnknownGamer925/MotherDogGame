using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;

public class MotherDogScript : MonoBehaviour, iDog
{
    [SerializeField] int MOVE_SPEED = 20;
    [SerializeField] int ROTATE_SPEED = 5;
    private float cooldown = 0;

    Camera cam;

    // Observer pattern
    public delegate void PuppyCheckDelegate();
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
            PuppyCheck();
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

    public void Respond() { }
    
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
        
    }


}
