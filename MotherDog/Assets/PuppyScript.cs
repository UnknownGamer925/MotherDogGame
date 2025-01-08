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
    internal MotherDogScript mother;
    private Animator animator;
    [SerializeField] int maxDistance;
    Vector3 random_point;
    private float cooldown = 0;
    private float response_time = 0;


    public enum State
    {
        Barking,
        Quiet,
        Held,
    }
    public State state;

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
        if (state != State.Held)
        {
            animator.SetBool("Barking", false);
            animator.SetBool("Mother", true);
            response_time = 1f;
        }
    }

    public void Pickup(bool enable)
    {
        if (enable == true)
        {
            state = State.Held;
            animator.SetBool("Mother", false);
            GetComponent<Rigidbody>().isKinematic = true;
            agent.updatePosition = false;
            animator.SetBool("Barking", false);
        }
        else
        {
            state = State.Quiet;
            GetComponent<Rigidbody>().isKinematic = false;
            agent.updatePosition = true;
        }
        
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
        if (state != State.Held && response_time <= 0)
        {
            Movement();
            animator.SetBool("Mother", false);

            if (cooldown <= 0)
            {
                if (state == State.Quiet)
                {
                    animator.SetBool("Barking", false);
                    cooldown = Random.Range(3, 10);
                    state = State.Barking;
                }
                else
                {
                    animator.SetBool("Barking", true);
                    cooldown = 1f;
                    state = State.Quiet;
                }
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
        else if (response_time > 0)
        {
            response_time -= Time.deltaTime;
        }

    }

    

    private void OnCollisionEnter(Collision collision)
    {
       random_point = ReturnPoint();
    }
}
