using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickScript : MonoBehaviour
{
    // Start is called before the first frame update
    public RaycastHit hit;
    public Transform cube;
    public GameObject[] consirvate;
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
                if(consirvate[scroll.ItemFact] != null)
                {
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
        if (Input.GetKeyDown(KeyCode.Q))//если нажата клавиша Е
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
        if(scroll.ItemFact == 0)
        {
            transform.Find("0").gameObject.SetActive(true);
            transform.Find("1").gameObject.SetActive(false);
            transform.Find("2").gameObject.SetActive(false);
            transform.Find("3").gameObject.SetActive(false);
        }
        if(scroll.ItemFact == 1)
        {
            transform.Find("1").gameObject.SetActive(true);
            transform.Find("0").gameObject.SetActive(false);
            transform.Find("2").gameObject.SetActive(false);
            transform.Find("3").gameObject.SetActive(false);
        }
        if(scroll.ItemFact == 2)
        {
            transform.Find("2").gameObject.SetActive(true);
            transform.Find("1").gameObject.SetActive(false);
            transform.Find("0").gameObject.SetActive(false);
            transform.Find("3").gameObject.SetActive(false);
        }
        if(scroll.ItemFact == 3)
        {
            transform.Find("3").gameObject.SetActive(true);
            transform.Find("1").gameObject.SetActive(false);
            transform.Find("2").gameObject.SetActive(false);
            transform.Find("0").gameObject.SetActive(false);
        }
    }
}
