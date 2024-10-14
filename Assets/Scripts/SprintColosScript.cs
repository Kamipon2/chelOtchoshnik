using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SprintColosScript : MonoBehaviour
{

    public float sprint = 1.0f;
    public bool fill;
    public bool enable = true;
    public Sprite[] sprites;
    public Image Mask;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Mask.fillAmount = sprint;
        if(sprint < 1.0f)
        {
            sprint = sprint + 0.001f;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(sprint > 0f && enable)
            {
                sprint = sprint - 0.002f;
            }
            else
            {
                enable = false;
                Mask.sprite = sprites[2];
            }
            
        }
        if(sprint >= 1.0f)
        {
            enable = true;
            Mask.sprite = sprites[1];
        }
    }
}
