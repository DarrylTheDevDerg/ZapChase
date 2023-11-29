using UnityEngine;

public class GrabObject : MonoBehaviour
{
    // Pivote en el objeto donde el jugador agarrará el objeto
    public Transform grabPoint;

    // Radio máximo del área de detección para agarrar el objeto
    public float grabRadius = 3f;

    // Tag del tipo de objeto que se puede agarrar
    public string grabbableTag = "Grabbable";

    // Fuerza del resorte que conecta al jugador con el objeto agarrado
    public float springForce = 500f;

    // Referencia al objeto actualmente agarrado
    private GameObject grabbedObject;

    // Componente SpringJoint para conectar al jugador con el objeto agarrado
    private SpringJoint playerSpringJoint;

    void Update()
    {
        // Detectar entrada de usuario para agarrar o soltar el objeto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsGrabbing())
            {
                TryGrabObject();
            }
            else
            {
                ReleaseObject();
            }
        }

        // Si el objeto está agarrado, actualizar su posición y rotación
        if (IsGrabbing() && grabbedObject != null)
        {
            UpdateGrabbedObjectPosition();
        }
    }

    void TryGrabObject()
    {
        // Realizar un overlap sphere desde la posición del grabPoint
        Collider[] colliders = Physics.OverlapSphere(grabPoint.position, grabRadius);

        foreach (var collider in colliders)
        {
            // Verificar si el objeto tiene el tag correcto
            if (collider.CompareTag(grabbableTag))
            {
                // Verificar si el objeto tiene un Rigidbody
                Rigidbody hitRigidbody = collider.GetComponent<Rigidbody>();
                if (hitRigidbody != null)
                {
                    // Agarrar el objeto
                    Grab(hitRigidbody.gameObject);
                    break; // Salir del bucle después de agarrar el primer objeto dentro del radio
                }
            }
        }
    }

    public void Grab(GameObject objToGrab)
    {
        // Establecer el objeto como agarrado
        grabbedObject = objToGrab;

        // Configurar el resorte en el jugador para conectarlo con el objeto agarrado
        playerSpringJoint = gameObject.AddComponent<SpringJoint>();
        playerSpringJoint.connectedBody = grabbedObject.GetComponent<Rigidbody>();
        playerSpringJoint.spring = springForce;
        playerSpringJoint.autoConfigureConnectedAnchor = false;
        playerSpringJoint.connectedAnchor = Vector3.zero;
    }

    public void GrabEnemy(Enemy enemyToGrab)
    {
        // Establecer el objeto como agarrado
        grabbedObject = enemyToGrab.gameObject;

        // Configurar el resorte en el jugador para conectarlo con el objeto agarrado
        playerSpringJoint = gameObject.AddComponent<SpringJoint>();
        playerSpringJoint.connectedBody = grabbedObject.GetComponent<Rigidbody>();
        playerSpringJoint.spring = springForce;
        playerSpringJoint.autoConfigureConnectedAnchor = false;
        playerSpringJoint.connectedAnchor = Vector3.zero;

        // Puedes ajustar otros parámetros del resorte según tus necesidades

        // Agregar código adicional si es necesario al agarrar el objeto
    }

    public void ReleaseObject()
    {
        // Restablecer el objeto como no agarrado
        grabbedObject = null;

        // Eliminar el resorte conectado al objeto agarrado
        if (playerSpringJoint != null)
        {
            Destroy(playerSpringJoint);
        }

        // Agregar cualquier código adicional para soltar el objeto
    }

    void UpdateGrabbedObjectPosition()
    {
        // Actualizar la posición y rotación del objeto agarrado al pivote específico
        grabbedObject.transform.position = grabPoint.position;
        grabbedObject.transform.rotation = grabPoint.rotation;
    }

    bool IsGrabbing()
    {
        return grabbedObject != null;
    }

    // Método auxiliar para visualizar el área de detección en el Editor de Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(grabPoint.position, grabRadius);
    }
}
