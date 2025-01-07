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

    public void Respond(int v) 
    {
        Debug.Log("Puppy Code: " + v);
    }

    public void Pickup(bool enable)
    {
        if (enable == true)
        {
            state = State.Held;
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
        if (state != State.Held)
        {
            Movement();
            //Debug.Log(state);

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
                    cooldown = Random.Range(2, 5);
                    state = State.Quiet;
                }
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
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
