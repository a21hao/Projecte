using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private bool arrastrando = false;
    private RectTransform rectTransform;
    private Vector2 posicionInicialMouse;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Verificar si el clic ocurri� en la parte superior de la ventana
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, eventData.position))
        {
            arrastrando = true;
            // Guardar la posici�n inicial del rat�n en relaci�n con la ventana
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out posicionInicialMouse);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (arrastrando)
        {
            Vector2 posicionRaton = eventData.position;
            // Convertir la posici�n del rat�n a la posici�n local de la ventana
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, posicionRaton, eventData.pressEventCamera, out posicionRaton);
            // Calcular la posici�n de la ventana sumando la diferencia entre la posici�n del rat�n y la posici�n inicial del rat�n
            Vector2 nuevaPosicion = posicionRaton - posicionInicialMouse;
            rectTransform.localPosition = nuevaPosicion;
        }
    }
}