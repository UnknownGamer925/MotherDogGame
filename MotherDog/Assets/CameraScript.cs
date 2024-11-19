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

    private float current_pitch;
    private float current_yaw;

    private Transform player;
    
    
    // Start is called before the first frame update
    void Start()
    {

        player = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        mouse_x = Input.GetAxis("Mouse X");
        mouse_y = Input.GetAxis("Mouse Y");

        current_pitch = Mathf.Clamp(current_pitch - mouse_y, min_pitch, max_pitch);
        current_yaw = Mathf.Clamp(current_yaw - -mouse_x, min_yaw, max_yaw);

        this.transform.eulerAngles = new Vector3(current_pitch, player.eulerAngles.y + current_yaw, 0f);
    }
}
