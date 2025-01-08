using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    public GameObject dogbedref;
    private float MaxFill;
    private float textdelay = 0;

    private GameObject timerUI;
    private GameObject textUI;
    
    // Start is called before the first frame update
    void Start()
    {
        dogbedref.GetComponent<DogbedScript>().UIControl += CanvasControl;
        MaxFill = dogbedref.GetComponent<DogbedScript>().maxtimer;

        timerUI = transform.GetChild(0).gameObject;
        textUI = transform.GetChild(1).gameObject;
        
        textUI.SetActive(false);
        timerUI.GetComponent<Image>().color = Color.gray;
    }

    void CanvasControl(float time, int control)
    {
        switch (control)
        {
            case 0:
                timerUI.GetComponent<Image>().color = new Color32(6, 192, 255, 255);
                timerUI.GetComponent<Image>().fillAmount = time / MaxFill;

                if (timerUI.GetComponent<Image>().fillAmount < 0.66f && timerUI.GetComponent<Image>().fillAmount > 0.33f)
                {
                    timerUI.GetComponent<Image>().color = new Color32(252, 132, 4, 255);
                }
                else if (timerUI.GetComponent<Image>().fillAmount < 0.33f)
                {
                    timerUI.GetComponent<Image>().color = Color.red;
                }
                break;
            case 1:
                textUI.gameObject.SetActive(true);
                textUI.GetComponent<TextMeshProUGUI>().SetText("TIMER EXPIRED: PUPPIES RELEASED");
                textdelay = 3f;
                timerUI.GetComponent<Image>().color = Color.gray;
                timerUI.GetComponent<Image>().fillAmount = 1;
                break;
            case 2:
                textUI.gameObject.SetActive(true);
                textUI.GetComponent<TextMeshProUGUI>().SetText("ROGUE CHILD: PUPPIES RELEASED");
                textdelay = 3f;
                timerUI.GetComponent<Image>().color = Color.gray;
                timerUI.GetComponent<Image>().fillAmount = 1;
                break;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (textdelay > 0)
        {
            textdelay -= Time.deltaTime;
        }

        if (textdelay < 0)
        {
            textUI.gameObject.SetActive(false);
        }

        
    }
}
