using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objeto : MonoBehaviour
{
    [SerializeField] Image imagenObjeto;
    [SerializeField] TextMeshProUGUI textoObjecto;
    [SerializeField] TextMeshProUGUI precioObjeto;
    [SerializeField] TextMeshProUGUI descripcionObjeto;

    private PlantillaObjetos datosObjeto; // Objeto asociado a este script
    private Inventario inventario;

    private void Awake()
    {
        inventario = FindObjectOfType<Inventario>();
    }

    public void CrearObjeto (PlantillaObjetos datos)
    {
        datosObjeto = datos;
        precioObjeto.text = datosObjeto.precioObjeto.ToString();
        imagenObjeto.sprite = datosObjeto.imagenObjeto;
        textoObjecto.text = datosObjeto.nameObjeto;
        textoObjecto.text = datosObjeto.descripcionObjeto;
        precioObjeto.text = datosObjeto.precioObjeto.ToString();
    }

    public void ComprarObjetos(Sprite imageObjeto)
    {
        if (datosObjeto != null)
        {
            inventario.IncluirObjeto(datosObjeto, imageObjeto);
        }
        else
        {
            Debug.LogError("No se ha asignado ning�n objeto a este script.");
        }
    }
}
