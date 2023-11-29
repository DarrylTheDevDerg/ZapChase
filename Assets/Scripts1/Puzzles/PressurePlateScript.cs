using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public GameObject door; // Asigna la puerta en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Grabbable") || other.CompareTag("Enemy"))
        {
            Debug.Log("Object entered the pressure plate.");
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Grabbable") || other.CompareTag("Enemy"))
        {
            Debug.Log("Object exited the pressure plate.");
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        if (door != null)
        {
            Debug.Log("Opening the door.");
            // Agrega código para abrir la puerta, por ejemplo, desactivando el GameObject de la puerta
            door.SetActive(false);
        }
    }

    private void CloseDoor()
    {
        if (door != null)
        {
            Debug.Log("Closing the door.");
            // Agrega código para cerrar la puerta, por ejemplo, activando el GameObject de la puerta
            door.SetActive(true);
        }
    }
}
