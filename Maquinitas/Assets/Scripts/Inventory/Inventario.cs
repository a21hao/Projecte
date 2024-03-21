using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Inventario : MonoBehaviour
{
    public GraphicRaycaster graphRay;
    public static Transform canvas;
    public GameObject objetoSeleccionado;
    public Transform exParent;
    public Transform contenido;
    public ScrollRect scrollRect;
    public List<GameObject> slots;

    private PointerEventData pointerData;
    private List<RaycastResult> raycastResults;
    private bool objetoArrastrado = false;


    private void Awake()
    {

        pointerData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();

        canvas = transform.parent.transform;
    }

    private void Update()
    {
        Arrastrar();
    }

    void Arrastrar()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            pointerData.position = Mouse.current.position.ReadValue();
            graphRay.Raycast(pointerData, raycastResults);
            if (raycastResults.Count > 0)
            {
                if (raycastResults[0].gameObject.GetComponent<Item>())
                {
                    objetoSeleccionado = raycastResults[0].gameObject;
                    exParent = objetoSeleccionado.transform.parent.transform;
                    exParent.GetComponent<Image>().fillCenter = false;
                    objetoSeleccionado.transform.SetParent(canvas);
                    objetoArrastrado = true; // Establecer el indicador de que se est� arrastrando un objeto
                    scrollRect.enabled = false; // Desactivar el ScrollRect mientras se arrastra un objeto
                }
            }
        }

        if (objetoSeleccionado != null && Mouse.current.leftButton.isPressed)
        {
            objetoSeleccionado.GetComponent<RectTransform>().localPosition = CanvasScreen(Mouse.current.position.ReadValue());
        }

        if (objetoSeleccionado != null && !Mouse.current.leftButton.isPressed)
        {
            pointerData.position = Mouse.current.position.ReadValue();
            raycastResults.Clear();
            graphRay.Raycast(pointerData, raycastResults);
            objetoSeleccionado.transform.SetParent(exParent);
            if (raycastResults.Count > 0)
            {
                foreach (var resultado in raycastResults)
                {
                    if (resultado.gameObject == objetoSeleccionado) continue;
                    if (resultado.gameObject.CompareTag("Slot"))
                    {
                        if (resultado.gameObject.GetComponentInChildren<Item>() == null)
                        {
                            objetoSeleccionado.transform.SetParent(resultado.gameObject.transform);
                            Debug.Log("Slot Libre");
                        }
                    }
                    if (resultado.gameObject.CompareTag("Item"))
                    {
                        if (resultado.gameObject.GetComponentInChildren<Item>().GetID() == objetoSeleccionado.GetComponent<Item>().GetID())
                        {
                            Debug.Log("ID igual");
                            resultado.gameObject.GetComponentInChildren<Item>().SetCantidad(resultado.gameObject.GetComponentInChildren<Item>().GetCantidad() + objetoSeleccionado.GetComponent<Item>().GetCantidad());
                            Destroy(objetoSeleccionado.gameObject);
                        }
                        else
                        {
                            Debug.Log("ID diferente");
                            objetoSeleccionado.transform.SetParent(resultado.gameObject.transform.parent);
                            resultado.gameObject.transform.SetParent(exParent);
                            resultado.gameObject.transform.localPosition = Vector3.zero;
                        }
                    }
                }
            }
            objetoSeleccionado.transform.localPosition = Vector3.zero;
            objetoSeleccionado = null;
            objetoArrastrado = false; // Restablecer el indicador de que no se est� arrastrando ning�n objeto
            scrollRect.enabled = true; // Reactivar el ScrollRect despu�s de soltar el objeto
        }
        raycastResults.Clear();
    }

    public GameObject GetSlotVacio()
    {
        foreach (Transform child in contenido)
        {
            if (child.childCount == 0)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public Vector2 CanvasScreen(Vector2 screenPos)
    {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPos);
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }
}
