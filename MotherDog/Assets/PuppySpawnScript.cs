using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppySpawnScript : MonoBehaviour
{
    [SerializeField] GameObject spawnEntity;
    
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
            GameObject pup = Instantiate(spawnEntity);
            pup.GetComponent<PuppyScript>().id = _id;
            Debug.Log(_id);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
