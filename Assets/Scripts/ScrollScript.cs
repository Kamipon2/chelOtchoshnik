using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{

    public GameObject icons;
    public GameObject icons1;
    public GameObject icons2;
    public GameObject icons3;
    public GameObject icons4;
    public int iconscount;
    public int ItemPick;
    public int i;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mouseScrollDelta);
        //bool i = true;
        if(Input.mouseScrollDelta != new Vector2(0f, 0f))
        {   
            StartCoroutine("iconsStart");
        }
        if(Input.mouseScrollDelta == new Vector2(0f, 1f))
        {   
            ItemPick--;
        }
        if(Input.mouseScrollDelta == new Vector2(0f, -1f))
        {   
            ItemPick++;
        }
        if(ItemPick == 0)
        {
            LeanTween.cancel(icons1);
            LeanTween.scale(icons1, new Vector3(1.5f, 1.5f, 1.0f), 0.2f);
        }
        else
        {
            LeanTween.cancel(icons1);
            LeanTween.scale(icons1, new Vector3(1.0f, 1.0f, 1.0f), 0.2f);
        }
        if(ItemPick == 1)
        {
            LeanTween.cancel(icons2);
            LeanTween.scale(icons2, new Vector3(1.5f, 1.5f, 1.0f), 0.2f);
        }
        else
        {
            LeanTween.cancel(icons2);
            LeanTween.scale(icons2, new Vector3(1.0f, 1.0f, 1.0f), 0.2f);
        }
        if(ItemPick == 2)
        {
            LeanTween.cancel(icons3);
            LeanTween.scale(icons3, new Vector3(1.5f, 1.5f, 1.0f), 0.2f);
        }
        else
        {
            LeanTween.cancel(icons3);
            LeanTween.scale(icons3, new Vector3(1.0f, 1.0f, 1.0f), 0.2f);
        }
        if(ItemPick == 3)
        {
            LeanTween.cancel(icons4);
            LeanTween.scale(icons4, new Vector3(1.5f, 1.5f, 1.0f), 0.2f);
        }
        else
        {
            LeanTween.cancel(icons4);
            LeanTween.scale(icons4, new Vector3(1.0f, 1.0f, 1.0f), 0.2f);
        }
        if(ItemPick > 3)
        {
            ItemPick = 0;
        }
        if(ItemPick < 0)
        {
            ItemPick = 3;
        }
        //LeanTween.scale(icons[ItemPick], new Vector3(1.5f, 1.5f, 1.0f), 0.2f);
        
    }
    IEnumerator iconsStart()
    {
        LeanTween.moveLocal(icons, new Vector3(841,icons.transform.localPosition.y ,0), 1f);
        yield return null;
        
    }
}
