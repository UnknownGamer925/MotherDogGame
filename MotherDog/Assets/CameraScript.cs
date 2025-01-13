using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Mouse position
    private float mouse_x;
    private float mouse_y;

    //Player transform
    private Transform player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Get player object reference
        player = GameObject.Find("MotherDog").transform;

        //lock cursor to centre
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        
        //Get Mouse Position
        mouse_x = Input.GetAxis("Mouse X");
        mouse_y = Input.GetAxis("Mouse Y");

        //updates position and rotation based on player & mouse position respetively
        transform.position = player.position;
        Vector3 euler = this.transform.eulerAngles;
        this.transform.eulerAngles = new Vector3((euler.x - mouse_y), (euler.y + mouse_x), 0f);

    }
}
