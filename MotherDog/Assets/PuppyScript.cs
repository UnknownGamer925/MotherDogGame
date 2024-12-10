using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PuppyScript: MonoBehaviour, iDog
{
    private NavMeshAgent agent;

    

    public void Movement()
    {
        
    }

    public void HandleComms(bool enable)
    {
        if (enable == true)
        {
            //.Recieve += Respond;
            FindObjectOfType<MotherDogScript>().PuppyCheck += Respond;

        }
        else
        {
            //Dog.Recieve -= Respond;
        }
    }

    void Respond() 
    {
        Debug.Log("Puppy Respond");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //int var = MotherDogScript.Func();
        //Debug.Log(var);
        //Debug.Log(MotherDogScript.num);
        HandleComms(true);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
