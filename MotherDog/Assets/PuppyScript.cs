using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PuppyScript: MonoBehaviour, iDog
{
    public int id;
    private NavMeshAgent agent;
    private MotherDogScript mother;
    [SerializeField] int maxDistance;
    Vector3 random_point;


    public void Movement()
    {
        if (agent.remainingDistance <= 1 || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            random_point = ReturnPoint();
        }

        agent.SetDestination(random_point);

        
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
        Debug.Log("Puppy Code: " + id);
    }

    private Vector3 ReturnPoint()
    {
        Vector3 point = Random.insideUnitSphere * maxDistance;
        NavMeshHit hit;
        NavMesh.SamplePosition(point, out hit, maxDistance, 1);
        Vector3 final_position = hit.position;
        final_position.y = 2.6f;

        return final_position;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        mother = FindObjectOfType<MotherDogScript>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Movement();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Destination: " + agent.remainingDistance);
            Debug.Log("Current: " + agent.stoppingDistance);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && mother != null)
        {
            HandleComms(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && mother != null)
        {
            HandleComms(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       random_point = ReturnPoint();
    }
}
