using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PuppyScript: MonoBehaviour, iDog
{
    private NavMeshAgent agent;
    private MotherDogScript mother;
    [SerializeField] int maxDistance;

    

    public void Movement()
    {
        Vector3 random_point = Random.insideUnitSphere * maxDistance;

        random_point += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(random_point, out hit, maxDistance, 1);
        Vector3 final_position = hit.position;
    }

    public void HandleComms(bool enable)
    {
        if (enable == true)
        {
            mother.PuppyCheck += Respond;

        }
        else
        {
            mother.PuppyCheck -= Respond;
        }
    }

    public void Respond() 
    {
        Debug.Log("Puppy Respond");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        mother = FindObjectOfType<MotherDogScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HandleComms(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            HandleComms(false);
        }
    }
}
