using UnityEngine;

public class playerPOV : MonoBehaviour
{
    public Transform Target;
    public bool FreezeX;
    public bool FreezeY;
    public bool FreezeZ;
    void Update()
    {
        Transform platez;
        transform.LookAt(Target);
        if(FreezeX)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        if(FreezeY)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
        if(FreezeZ)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }

    }
}
