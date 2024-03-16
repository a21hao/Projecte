using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjetoTienda : MonoBehaviour
{
    // Referencias a los componentes UI para mostrar informaci�n del objeto
    public TextMeshProUGUI nombreText;
    public Image spriteImage;
    public TextMeshProUGUI precioObjeto;
    public TextMeshProUGUI descripcionObjeto;

    // M�todo para configurar el nombre del objeto
    public void SetNombre(string nombre)
    {
        nombreText.text = nombre;
    }

    // M�todo para configurar el sprite del objeto
    public void SetSprite(Sprite sprite)
    {
        spriteImage.sprite = sprite;
    }

    // M�todo para configurar la descripci�n del objeto
    public void SetPrecio(string precio)
    {
        precioObjeto.text = precio;
    }
    public void SetDescripcion(string descripcion)
    {
        descripcionObjeto.text = descripcion;
    }
}
