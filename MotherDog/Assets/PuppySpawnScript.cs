using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuppySpawnScript : MonoBehaviour
{
    [SerializeField] GameObject spawnEntity;

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
        int good_pups = 0;
        int _id;
        for (int i = 0; i < 10; i++)
        {
            if (good_pups < 5)
            {
                _id = Random.Range(0, 2);
                if (_id == 1)
                {
                    good_pups++;
                }
            }
            else
            {
                _id = 1;
            }
            Vector3 spawnpoint = ReturnPoint();
            GameObject pup = Instantiate(spawnEntity, spawnpoint, Quaternion.identity, transform);
            pup.GetComponent<PuppyScript>().id = _id;
            Debug.Log(_id);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
