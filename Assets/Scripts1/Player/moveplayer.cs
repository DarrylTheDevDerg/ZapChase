using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplayer : MonoBehaviour
{
    public float velocidad = 5.0f; // Velocidad de movimiento del jugador
    private Quaternion rotacionDeseada;
    private float velocidadRotacion = 360.0f; // Velocidad de rotaci�n en grados por segundo
    private Quaternion rotacionOriginal;

    private void Start()
    {
        // Guarda la rotaci�n original del jugador al inicio
        rotacionOriginal = transform.rotation;
    }

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
            rotacionDeseada = Quaternion.LookRotation(movimiento, Vector3.up);

            // Mant�n la rotaci�n en el eje X en -90 grados
            rotacionDeseada.eulerAngles = new Vector3(-90, rotacionDeseada.eulerAngles.y, rotacionDeseada.eulerAngles.z);

            // Gradualmente rota hacia la nueva rotaci�n
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionDeseada, velocidadRotacion * Time.deltaTime);
        }

        // Mueve al jugador en la direcci�n del movimiento
        transform.Translate(movimiento * velocidad * Time.deltaTime, Space.World);
    }
}
