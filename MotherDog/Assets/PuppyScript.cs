using UnityEngine;
using UnityEngine.AI;


public class PuppyScript: MonoBehaviour, iDog
{
    //public identifier
    public int id;

    //Audio clip references
    [SerializeField] AudioClip bark_clip;
    [SerializeField] AudioClip response_clip;

    //Component/Script references
    private NavMeshAgent agent;
    internal MotherDogScript mother;
    private Animator animator;
    private AudioSource audio1;

    //Variables
    [SerializeField] int maxDistance;
    Vector3 random_point;
    private float cooldown = 0;
    private float response_time = 0;

    // State enumerator
    public enum State
    {
        Barking,
        Quiet,
        Held,
    }
    public State state;

    // Move to random point in the NavMesh
    public void Movement()
    {
        if (agent.remainingDistance <= 1 || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            random_point = ReturnPoint(); //Defines new random point on NavMesh
        }

        agent.SetDestination(random_point); //Moves towards new point on NavMesh

        
    }

    //Subcribes/Unsubscribes from player broardcast
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

    //Called by Broardcast. Responds to mother's call
    public void Respond() 
    {
        if (state != State.Held)
        {
            animator.SetBool("Barking", false);
            animator.SetBool("Mother", true);
            audio1.Stop();
            audio1.PlayOneShot(response_clip);
            response_time = 1f;
        }
    }

    //Handles player interaction to pick up puppy
    public void Pickup(bool enable)
    {
        if (enable == true) //sets puppy to held state. Stops barking, disables movement & disables physics
        {
            state = State.Held;
            animator.SetBool("Mother", false);
            GetComponent<Rigidbody>().isKinematic = true;
            agent.updatePosition = false;
            animator.SetBool("Barking", false);
            
        }
        else //Returns puppy to Queit mode. Re-enables movement and enables physics
        {
            state = State.Quiet;
            GetComponent<Rigidbody>().isKinematic = false;
            agent.updatePosition = true;
        }
        
    }

    //Selects and Returns random point on NavMesh (within radius)
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
        //initialise Component/Script references
        mother = FindObjectOfType<MotherDogScript>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        audio1 = transform.GetChild(0).GetComponent<AudioSource>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state != State.Held && response_time <= 0) //Normal loop (movement with random barking)
        {
            //Call Movement function
            Movement();
            animator.SetBool("Mother", false); 

            //Repeating timer, each time it reaches zero the state alternates (Bearking / Quiet) and the timer is reset
            if (cooldown <= 0)
            {
                if (state == State.Quiet)
                {
                    animator.SetBool("Barking", false);
                    audio1.Stop();
                    cooldown = Random.Range(3, 10);
                    state = State.Barking;
                    
                }
                else
                {
                    animator.SetBool("Barking", true);
                    audio1.PlayOneShot(bark_clip);
                    cooldown = 1f;
                    state = State.Quiet;
                }
            }
            else
            {
                cooldown -= Time.deltaTime; //counts down timer 
            }
        }
        else if (response_time > 0)
        {
            response_time -= Time.deltaTime; //counts down timer while responding to mother
        }

    }

    
    //When puppy collides with anything, repick new direction to head in
    private void OnCollisionEnter(Collision collision)
    {
       random_point = ReturnPoint();
    }
}
