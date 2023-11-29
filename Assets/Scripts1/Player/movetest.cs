using UnityEngine;

public class movetest : MonoBehaviour
{
    public float velocidad = 5.0f; // Velocidad de movimiento del jugador
    private float velocidadRotacion = 360.0f; // Velocidad de rotaci�n en grados por segundo

    private void Update()
    {
        // Obt�n las entradas del eje horizontal (izquierda/derecha) y vertical (arriba/abajo)
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcula el vector de movimiento basado en las entradas
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);

        // Si hay entrada de movimiento
        if (movimiento != Vector3.zero)
        {
            // Obtiene la rotaci�n actual del jugador
            Quaternion rotacionActual = transform.rotation;

            // Calcula la rotaci�n deseada basada en las entradas
            Quaternion rotacionDeseada = Quaternion.LookRotation(movimiento, Vector3.up);

            // Interpola suavemente entre la rotaci�n actual y la rotaci�n deseada
            transform.rotation = Quaternion.RotateTowards(rotacionActual, rotacionDeseada, velocidadRotacion * Time.deltaTime);
        }

        // Mueve al jugador en la direcci�n del movimiento relativo a la orientaci�n actual
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
}
