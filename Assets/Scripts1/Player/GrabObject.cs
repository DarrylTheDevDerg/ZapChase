using UnityEngine;

public class GrabObject : MonoBehaviour
{
    // Pivote en el objeto donde el jugador agarrar� el objeto
    public Transform grabPoint;

    // Radio m�ximo del �rea de detecci�n para agarrar el objeto
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

        // Si el objeto est� agarrado, actualizar su posici�n y rotaci�n
        if (IsGrabbing() && grabbedObject != null)
        {
            UpdateGrabbedObjectPosition();
        }
    }

    void TryGrabObject()
    {
        // Realizar un overlap sphere desde la posici�n del grabPoint
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
                    break; // Salir del bucle despu�s de agarrar el primer objeto dentro del radio
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

        // Puedes ajustar otros par�metros del resorte seg�n tus necesidades

        // Agregar c�digo adicional si es necesario al agarrar el objeto
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

        // Agregar cualquier c�digo adicional para soltar el objeto
    }

    void UpdateGrabbedObjectPosition()
    {
        // Actualizar la posici�n y rotaci�n del objeto agarrado al pivote espec�fico
        grabbedObject.transform.position = grabPoint.position;
        grabbedObject.transform.rotation = grabPoint.rotation;
    }

    bool IsGrabbing()
    {
        return grabbedObject != null;
    }

    // M�todo auxiliar para visualizar el �rea de detecci�n en el Editor de Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(grabPoint.position, grabRadius);
    }
}
