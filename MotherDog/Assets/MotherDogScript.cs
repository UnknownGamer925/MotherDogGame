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

    //public delegate void Function();
    //Function function;
    //public static int num = 9;
   //public static int Func() { Debug.Log(num); return num; }

    Camera cam;
 

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

    public delegate void PuppyCheckDelegate();
    public event PuppyCheckDelegate PuppyCheck;

    public void HandleComms(bool enable)
    {
        if (enable == true)
        {
            //Dog.Recieve += Respond;

        }
        else
        {
            //Dog.Recieve -= Respond;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        //function += Func;
        //function_global += Func;
        //HandleComms(true);
        
    }
    

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (PuppyCheck != null)
        {
            PuppyCheck();
        }
    }


}
