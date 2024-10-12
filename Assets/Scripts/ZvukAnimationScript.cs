using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZvukAnimationScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       // anim = gameObject.GetComponent<Animator>();
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.relativeVelocity.magnitude >= 8)
        {
            anim.Play("2");
        }
        Debug.Log(other.relativeVelocity.magnitude);
        
    }
}
