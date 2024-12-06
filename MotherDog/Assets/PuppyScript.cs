using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PuppyScript : Dog, iDog
{
    private NavMeshAgent agent;
    
    public override void Movement()
    {
        
    }

    public override void Call()
    {
        Debug.Log("Call");
    }

    public void Respond() 
    {
        Debug.Log("Puppy!");
    }


    public override void HandleComms(bool enable)
    {
        if (enable == true)
        {
            //.Recieve += Respond;
        }
        else
        {
            //Dog.Recieve -= Respond;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //int var = MotherDogScript.Func();
        //Debug.Log(var);
        //Debug.Log(MotherDogScript.num);
        //HandleComms(true);
        Recieve rec = Respond;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
