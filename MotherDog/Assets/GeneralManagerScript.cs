using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GeneralManagerScript : MonoBehaviour
{
    public float gameTimer;
    public GameObject Textref;
    private TextMeshProUGUI text;
    private Vector3 MinSecSubsec;
    
    
    private string SubsecFormat(int subsec)
    {
        string format;
        if (subsec < 10)
        {
            format = "0" + subsec;
        }
        else
        {
            format = subsec.ToString();
        }
        return format;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        text = Textref.GetComponent<TextMeshProUGUI>();

        if (gameTimer <= 0)
        {
            gameTimer = 5;
        }
        MinSecSubsec = new Vector3(gameTimer, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (MinSecSubsec.x <= 0 && MinSecSubsec.y <= 0 && MinSecSubsec.z <= 0)
        {

        }
        else
        {
            if (MinSecSubsec.y <= 0)
            {
                MinSecSubsec.x--;
                MinSecSubsec.y = 60f;
            }

            MinSecSubsec.y -= Time.deltaTime;
            MinSecSubsec.z = (int)(((decimal)MinSecSubsec.y % 1) * 100);
        }

        text.SetText((int)MinSecSubsec.x + " : " + System.Math.Truncate(MinSecSubsec.y) + " : " + SubsecFormat((int)MinSecSubsec.z));
    }
}
