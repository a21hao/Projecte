using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VentanaCompra : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nombreText;
    [SerializeField] Image spriteImage;
    [SerializeField] TextMeshProUGUI precioText;
    [SerializeField] TextMeshProUGUI descripcionText;
    [SerializeField] Slider cantidadSlider;
    [SerializeField] TextMeshProUGUI cantidadTexto;

    private Tienda tienda; // Referencia a la tienda

    // Configurar la ventana de compra con la informaci�n del objeto seleccionado
    public void ConfigurarVentana(string nombre, Sprite sprite, string precio, string descripcion)
    {
        nombreText.text = nombre;
        spriteImage.sprite = sprite;
        precioText.text = precio;
        descripcionText.text = descripcion;
    }

    public void ConfigurarVentanaDesdeObjetoTienda(string nombre, Sprite sprite, string precio, string descripcion, Tienda tienda)
    {
        ConfigurarVentana(nombre, sprite, precio, descripcion); // Configurar la ventana con la informaci�n del objeto
        SetTienda(tienda); // Establecer una referencia a la tienda
    }

    // Establecer una referencia a la tienda
    public void SetTienda(Tienda tienda)
    {
        this.tienda = tienda;
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
