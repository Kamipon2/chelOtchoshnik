using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickScript : MonoBehaviour
{
    public RaycastHit hit;
    public Transform cube;
    public GameObject[] consirvate;
    public ScrollScript scroll;

   
    public string requiredItemTag = "key"; 

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit);

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody>())
            {
                
                if (consirvate[scroll.ItemFact] != null)
                {
                    
                    if (consirvate[scroll.ItemFact].CompareTag(requiredItemTag))
                    {
                        
                        DoorScript doorScript = hit.collider.GetComponent<DoorScript>();
                        if (doorScript != null)
                        {
                            doorScript.ToggleDoor();
                            return; 
                        }
                    }

                   
                    consirvate[scroll.ItemFact].GetComponent<Rigidbody>().isKinematic = false;
                    consirvate[scroll.ItemFact].GetComponent<BoxCollider>().enabled = true;
                    consirvate[scroll.ItemFact].transform.SetParent(null);
                    consirvate[scroll.ItemFact].layer = 0;
                    consirvate[scroll.ItemFact] = null;
                    scroll.Out();
                }

                hit.collider.transform.position = cube.position;
                hit.collider.transform.rotation = cube.rotation;
                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                hit.collider.transform.SetParent(gameObject.transform.Find(scroll.ItemFact.ToString()));
                hit.collider.gameObject.layer = 7;
                consirvate[scroll.ItemFact] = hit.collider.gameObject;
                scroll.Get(consirvate[scroll.ItemFact]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            consirvate[scroll.ItemFact].GetComponent<Rigidbody>().isKinematic = false;
            consirvate[scroll.ItemFact].GetComponent<BoxCollider>().enabled = true;
            consirvate[scroll.ItemFact].transform.SetParent(null);
            consirvate[scroll.ItemFact].layer = 0;
            consirvate[scroll.ItemFact] = null;
            scroll.Out();
        }
    }

    public void Set()
    {
        for (int i = 0; i < 4; i++)
        {
            transform.Find(i.ToString()).gameObject.SetActive(i == scroll.ItemFact);
        }
    }
}
