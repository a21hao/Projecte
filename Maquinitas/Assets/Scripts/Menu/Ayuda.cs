using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ayuda : MonoBehaviour
{
    public GameObject gameObjectToToggle;

    private bool isOpen = false;

    public void Toggle()
    {
        isOpen = !isOpen; // Cambia el estado del bot�n

        // Activa o desactiva el GameObject seg�n el estado del bot�n
        if (gameObjectToToggle != null)
        {
            gameObjectToToggle.SetActive(isOpen);
        }
    }
}
