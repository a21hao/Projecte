using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VentanaCompra : MonoBehaviour
{
    public ObjetoTienda objetoTienda;

    [SerializeField] TextMeshProUGUI nombreText;
    [SerializeField] Image spriteImage;
    [SerializeField] TextMeshProUGUI precioText;
    [SerializeField] TextMeshProUGUI descripcionText;
    [SerializeField] Slider cantidadSlider;
    [SerializeField] TextMeshProUGUI cantidadTexto;

    private void Start()
    {
        if (objetoTienda != null)
        {
            nombreText.text = objetoTienda.nombreText;
            spriteImage.sprite = objetoTienda.spriteImage;
            precioText.text = objetoTienda.precioObjeto;
            descripcionText.text = objetoTienda.descripcionObjeto;
        }
    }

    // Actualizar el texto de cantidad seg�n el valor del slider
    public void ActualizarCantidadTexto(float cantidad)
    {
        cantidadTexto.text = cantidad.ToString();
    }

    // M�todo para comprar
    public void Comprar()
    {
        int cantidad = (int)cantidadSlider.value;
        // Aqu� puedes implementar la l�gica para realizar la compra, usando la referencia a la tienda
        //tienda.Comprar();
        // Cerrar la ventana de compra
        CerrarVentana();
    }

    // M�todo para cancelar la compra y cerrar la ventana
    public void Cancelar()
    {
        // Cerrar la ventana de compra
        CerrarVentana();
    }

    // M�todo para cerrar la ventana de compra
    private void CerrarVentana()
    {
        Destroy(gameObject);
    }
}
