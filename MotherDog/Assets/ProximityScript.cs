using UnityEngine;

public class ProximityScript : MonoBehaviour
{
    //Object/Script References
    [SerializeField] GameObject parent1;
    private PuppyScript main;
    private MotherDogScript mother;
    
    // Start is called before the first frame update
    void Start()
    {
        //Getting object references
        main = parent1.GetComponent<PuppyScript>(); //main puppy object (parent)
        mother = FindObjectOfType<MotherDogScript>();
    }

    //subscribe puppy to broardcast when sphere collides with player
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && mother != null)
        {
            main.HandleComms(true);
        }
    }

    //unsubscribe puppy to broardcast when player leaves sphere collision
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && mother != null)
        {
            main.HandleComms(false);
        }
    }
}
