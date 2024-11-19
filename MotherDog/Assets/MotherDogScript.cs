using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class MotherDogScript : MonoBehaviour, iDog
{
    int SPEED;

    public MotherDogScript()
    {
        SPEED = 20;
    }
    
    
    
    public void Movement()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal") * SPEED * Time.deltaTime, 0, 0);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            transform.position += new Vector3(0, 0, Input.GetAxis("Vertical") * SPEED * Time.deltaTime);
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
