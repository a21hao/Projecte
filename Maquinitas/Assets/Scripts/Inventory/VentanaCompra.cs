using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VentanaCompra : MonoBehaviour
{
    public ObjetoTienda objetoTienda;

    private MoneyManager moneyManager;

    [SerializeField] private TextMeshProUGUI nombreText;
    [SerializeField] private Image spriteImage;
    [SerializeField] private TextMeshProUGUI precioText;
    [SerializeField] private TextMeshProUGUI descripcionText;
    [SerializeField] private Slider cantidadSlider;
    [SerializeField] private TextMeshProUGUI cantidadTexto;

    private void Start()
    {
        if (objetoTienda != null)
        {
            nombreText.text = objetoTienda.nombreText;
            spriteImage.sprite = objetoTienda.spriteImage;
            precioText.text = objetoTienda.precioObjeto;
            descripcionText.text = objetoTienda.descripcionObjeto;
        }
        cantidadSlider.maxValue = 999;
        //ActualizarCantidadTexto(cantidadSlider.value);
    }

    // Actualizar el texto de cantidad seg�n el valor del slider
    public void ActualizarCantidadTexto(float cantidad)
    {
        cantidadTexto.text = Mathf.RoundToInt(cantidad).ToString();
        if (objetoTienda != null)
        {
            float precio = float.Parse(objetoTienda.precioObjeto);
            float precioTotal = (cantidad * precio);
            precioText.text = Mathf.RoundToInt(precioTotal).ToString();
         }
    }

    // M�todo para comprar
    public void Comprar()
    {
        int cantidad = (int)cantidadSlider.value;
        if (moneyManager != null && objetoTienda != null)
        {
            float precio = float.Parse(objetoTienda.precioObjeto);
            int precioTotal = Mathf.RoundToInt(cantidad * precio);
            MoneyManager.DecrementarDinero(precioTotal);
        }
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
