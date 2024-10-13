using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintColosScript : MonoBehaviour
{

    public float sprint;
    public Transform Mask;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Mask.parent.position = new Vector3(Mask.parent.position.x + sprint, Mask.parent.position.y, Mask.parent.position.z);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            sprint = sprint - 0.00002f;
        }
    }
}
