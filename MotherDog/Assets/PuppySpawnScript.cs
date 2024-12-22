using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuppySpawnScript : MonoBehaviour
{
    [SerializeField] GameObject spawnEntity;
    [SerializeField] int total_pups = 10;
    [SerializeField] int good_pups = 5;

    private Vector3 ReturnPoint()
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
        if (good_pups >= total_pups)
        {
            good_pups = total_pups / 2;
        }
        
        
        int _id;
        for (int i = 0; i < total_pups; i++)
        {
            if (i <= good_pups)
            {
                _id = 0;
            }
            else
            {
                _id = 1;
            }
            
            Vector3 spawnpoint = ReturnPoint();
            GameObject pup = Instantiate(spawnEntity, spawnpoint, Quaternion.identity, transform);
            pup.GetComponent<PuppyScript>().id = _id;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
