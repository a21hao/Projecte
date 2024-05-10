using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    public Transform target; // El objeto que la c�mara seguir� (por ejemplo, el jugador)
    public float distance = 10f; // Distancia de la c�mara al objetivo
    public float height = 5f; // Altura de la c�mara sobre el objetivo

    private void Start()
    {
        IsometicCamera();
    }

    private void IsometicCamera()
    {
        // Calcula la posici�n de la c�mara
        Vector3 offset = new Vector3(0f, height, -distance);
        Vector3 desiredPosition = target.position + offset;

        // Establece la posici�n directamente sin interpolaci�n
        transform.position = desiredPosition;

        // Mira hacia el objetivo
        //transform.LookAt(target);
        Vector3 lookDirection = target.position - transform.position;

        // Calcula la rotaci�n necesaria para mirar hacia el punto
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        // Aplica la rotaci�n al objeto
        transform.rotation = targetRotation;
    }
}
