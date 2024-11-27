using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuppyScript : MonoBehaviour, iDog
{
    private NavMeshAgent agent;
    
    public void Movement()
    {
        
    }

    public void Call()
    {
        Debug.Log("Call");
    }

    public void Respond()
    {
        Debug.Log("Respond");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //int var = MotherDogScript.Func();
        //Debug.Log(var);
        //Debug.Log(MotherDogScript.num);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
