using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class MotherDogScript : MonoBehaviour, iDog
{
    int SPEED;
    Vector3 right_face = new Vector3(0f, 90f, 0f);
    Vector3 left_face = new Vector3(0f, 270f, 0f);

    public MotherDogScript()
    {
        SPEED = 20;
    }
    
    
    
    public void Movement()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (transform.eulerAngles.y > 270f || transform.eulerAngles.y < 90f)
            {
                transform.eulerAngles += new Vector3(0f,  0.5f, 0f);
            }
            else if (transform.eulerAngles.y < 270f || transform.eulerAngles.y > 90f)
            {
                transform.eulerAngles += new Vector3(0f, -0.5f, 0f);
            }
            
            transform.position += new Vector3(Input.GetAxis("Horizontal") * SPEED * Time.deltaTime, 0, 0);

        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (transform.eulerAngles.y > 270f || transform.eulerAngles.y < 90f)
            {
                transform.eulerAngles += new Vector3(0f, -0.5f, 0f);
            }
            else if (transform.eulerAngles.y < 270f || transform.eulerAngles.y > 90f)
            {
                transform.eulerAngles += new Vector3(0f, 0.5f, 0f);
            }

            transform.position += new Vector3(Input.GetAxis("Horizontal") * SPEED * Time.deltaTime, 0, 0);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            transform.position += Input.GetAxis("Vertical") * transform.GetChild(1).GetComponent<Camera>().transform.forward * SPEED * Time.deltaTime;
        }
    }
    
    public void Call()
    {
        Debug.Log("call command");
    }

    public void Respond()
    {
        Debug.Log("respond command");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
