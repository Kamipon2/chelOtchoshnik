using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickScript : MonoBehaviour
{
    // Start is called before the first frame update
    public RaycastHit hit;
    public Transform cube;
    private GameObject consirvate;
    public ScrollScript scroll;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit);
        if (Input.GetKeyDown(KeyCode.E))//если нажата клавиша Е
        {
             if(hit.collider.gameObject.GetComponent<Rigidbody>())
             {
                if(consirvate != null)
                {
                    consirvate.GetComponent<Rigidbody>().isKinematic = false;
                    consirvate.GetComponent<BoxCollider>().enabled = true;
                    consirvate.transform.SetParent(null);
                    consirvate.layer = 0;
                    consirvate = null;
                    scroll.Out();
                }
                hit.collider.transform.position = cube.position;
                hit.collider.transform.rotation = cube.rotation;
                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                hit.collider.transform.SetParent(gameObject.transform);
                hit.collider.gameObject.layer = 7;
                consirvate = hit.collider.gameObject;
                scroll.Get(consirvate);
                

             }
        }
        if (Input.GetKeyDown(KeyCode.Q))//если нажата клавиша Е
        {
             
                consirvate.GetComponent<Rigidbody>().isKinematic = false;
                consirvate.GetComponent<BoxCollider>().enabled = true;
                consirvate.transform.SetParent(null);
                consirvate.layer = 0;
                consirvate = null;
                scroll.Out();

             
        }
    }
}
