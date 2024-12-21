using System.Collections;
using System.Collections.Concurrent;
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
    Vector3 random_point;


    public void Movement()
    {
        if (transform.position == random_point)
        {
            random_point = ReturnPoint();
        }
        transform.position += random_point;
        Debug.Log(transform.position);
        
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

    private Vector3 ReturnPoint()
    {
        Vector3 point = Random.insideUnitSphere * maxDistance;
        NavMeshHit hit;
        NavMesh.SamplePosition(point, out hit, maxDistance, 1);
        Vector3 final_position = hit.position;

        return final_position;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        mother = FindObjectOfType<MotherDogScript>();
        random_point = ReturnPoint();
        
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
