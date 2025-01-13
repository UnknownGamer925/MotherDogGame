using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuppySpawnScript : MonoBehaviour
{
    //Prefab to be spawned
    [SerializeField] GameObject spawnEntity;

    //Variables
    [SerializeField] int total_pups = 10;
    [SerializeField] public int good_pups = 5;

    private Vector3 ReturnPoint() // Gets and returns random point on Navmesh
    {
        Vector3 point = Random.insideUnitSphere * 15;
        NavMeshHit hit;
        NavMesh.SamplePosition(point, out hit, 15, 1);
        Vector3 final_position = hit.position;
        final_position.y = 2.6f;

        return final_position;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Edge case to ensure appropriate number of pups are spawned
        if (good_pups >= total_pups)
        {
            good_pups = total_pups / 2;
        }
        
        // Loop for creating and instantiating puppies
        int _id;
        for (int i = 0; i < total_pups; i++)
        {
            if (i < good_pups)
            {
                _id = 0;
            }
            else
            {
                _id = 1;
            }
            
            Vector3 spawnpoint = ReturnPoint(); //Defines spawnpoint
            GameObject pup = Instantiate(spawnEntity, spawnpoint, Quaternion.identity, transform); //Instantiate puppy at spawn point
            pup.GetComponent<PuppyScript>().id = _id; // allocates puppy ID
        }
    }
}
