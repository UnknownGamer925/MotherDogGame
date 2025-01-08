using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityScript : MonoBehaviour
{
    [SerializeField] GameObject parent1;
    private PuppyScript main;
    private MotherDogScript mother;
    
    // Start is called before the first frame update
    void Start()
    {
        main = parent1.GetComponent<PuppyScript>();
        mother = mother = FindObjectOfType<MotherDogScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && mother != null)
        {
            main.HandleComms(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && mother != null)
        {
            main.HandleComms(false);
        }
    }
}
