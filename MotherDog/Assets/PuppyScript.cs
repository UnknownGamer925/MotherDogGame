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
    private Animator animator;
    [SerializeField] int maxDistance;
    Vector3 random_point;
    private float cooldown = 0;
    private bool barkstate = true;

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
        if (enable == true && id == 0)
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
        mother = FindObjectOfType<MotherDogScript>(); // <-- initialised in Start
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Movement();


        if (cooldown <= 0)
        {
            if (barkstate == true)
            {
                animator.SetBool("Barking", false);
                cooldown = Random.Range(3, 10);
                barkstate = false;
            }
            else
            {
                animator.SetBool("Barking", true);
                cooldown = Random.Range(2, 5);
                barkstate = true;
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
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
