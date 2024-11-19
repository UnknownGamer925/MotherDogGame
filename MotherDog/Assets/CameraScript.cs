using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
  
    [SerializeField] private float min_pitch;
    [SerializeField] private float max_pitch;
    [SerializeField] private float min_yaw;
    [SerializeField] private float max_yaw;

    private float mouse_x;
    private float mouse_y;

    //private float current_pitch;
    //private float current_yaw;

    private Transform player;
    //private Vector3 distance;
    
    
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("MotherDog").transform;
        Cursor.lockState = CursorLockMode.Locked;
        //distance = pivot.position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        mouse_x = Input.GetAxis("Mouse X");
        mouse_y = Input.GetAxis("Mouse Y");

        //current_pitch = Mathf.Clamp(current_pitch - mouse_y, min_pitch, max_pitch);
        //current_yaw = Mathf.Clamp(current_yaw - -mouse_x, min_yaw, max_yaw);

        transform.position = player.position;
        Vector3 euler = this.transform.eulerAngles;
        this.transform.eulerAngles = new Vector3((euler.x - mouse_y), (euler.y + mouse_x), 0f);
        
        
        
        //transform.position = pivot.position - distance;

        //pivot.transform.eulerAngles += new Vector3(mouse_x, mouse_y, 0f);

    }
}
