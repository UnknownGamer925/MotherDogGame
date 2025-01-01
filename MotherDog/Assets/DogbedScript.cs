using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogbedScript : MonoBehaviour
{
    MotherDogScript mother;
    
    
    public void MotherLink(bool link)
    {
        if (link == true)
        {
            mother.DogbedCall += AddPuppy;
        }
        else if (link == false) 
        {
            mother.DogbedCall -= AddPuppy;
        }
    }
    
    public void AddPuppy(GameObject newpup)
    {
        newpup.transform.parent = transform;
        newpup.transform.position = transform.position + new Vector3(0, 1, 0);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        mother = FindObjectOfType<MotherDogScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MotherLink(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            MotherLink(false);
        }
    }
}
