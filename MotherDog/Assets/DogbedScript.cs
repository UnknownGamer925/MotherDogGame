using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogbedScript : MonoBehaviour
{
    MotherDogScript mother;
    private float puptimer = 0;
    public float maxtimer;
    private bool empty = true;

    public delegate void UIControlDelegate(float time, int control);
    public event UIControlDelegate UIControl;
    
    public void MotherLink(bool link)
    {
        if (link == true)
        {
            mother.DogbedCall += BroadcastHandler;
        }
        else if (link == false) 
        {
            mother.DogbedCall -= BroadcastHandler;
        }
    }
    
    public void BroadcastHandler(bool select, GameObject obj)
    {
        if (select)
        {
            Interact(obj);
        }
        else if (!select)
        {
            Interact();
            Debug.Log("WWWWWWWWWW");
        }
    }


    public void Interact(GameObject newpup)
    {
        newpup.transform.parent = transform;
        newpup.transform.position = transform.position + new Vector3(0, 1, 0);
        puptimer = maxtimer;
        empty = false;
    }
    public void Interact()
    {
        puptimer = maxtimer;
    }

    public void ReleaseDogs()
    {
        for (int i = transform.childCount-1; i > 0; i--)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "Puppy") 
            {
                child.transform.position = transform.position + new Vector3(-7, 0.5f, 0);
                child.GetComponent<PuppyScript>().Pickup(false);
                child.transform.parent = GameObject.Find("PuppySpawner").transform;
            }
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        mother = FindObjectOfType<MotherDogScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (empty == false)
        {
            puptimer -= Time.deltaTime;
            UIControl(puptimer, 0);
        }
        
        if (puptimer <= 0 && empty == false)
        {
            UIControl(0, 1);
            empty = true;
            ReleaseDogs();
        }
    }

    bool linked = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && linked == false)
        {
            MotherLink(true);
            linked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && linked == true)
        {
            MotherLink(false);
            linked = false;
        }
    }
}
