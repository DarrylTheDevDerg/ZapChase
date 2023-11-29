using UnityEngine;

public class movetest : MonoBehaviour
{
    public float velocidad = 5.0f; // Velocidad de movimiento del jugador
    private float velocidadRotacion = 360.0f; // Velocidad de rotación en grados por segundo

    private void Update()
    {
        // Obtén las entradas del eje horizontal (izquierda/derecha) y vertical (arriba/abajo)
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcula el vector de movimiento basado en las entradas
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);

        // Si hay entrada de movimiento
        if (movimiento != Vector3.zero)
        {
            // Obtiene la rotación actual del jugador
            Quaternion rotacionActual = transform.rotation;

            // Calcula la rotación deseada basada en las entradas
            Quaternion rotacionDeseada = Quaternion.LookRotation(movimiento, Vector3.up);

            // Interpola suavemente entre la rotación actual y la rotación deseada
            transform.rotation = Quaternion.RotateTowards(rotacionActual, rotacionDeseada, velocidadRotacion * Time.deltaTime);
        }

        // Mueve al jugador en la dirección del movimiento relativo a la orientación actual
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
}
