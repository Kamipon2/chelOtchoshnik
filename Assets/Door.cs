using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject door; 

    public void ToggleDoor()
    {
        if (isOpen)
        {
            // Закрыть дверь
            door.transform.Rotate(0, -90, 0);
        }
        else
        {
            // Открыть дверь
            door.transform.Rotate(0, 90, 0); 
        }
        isOpen = !isOpen; 
    }
}