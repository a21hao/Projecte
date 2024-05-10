using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneScript : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [Header("Open Animation")]
    [Space]
    [SerializeField] float openAnimationDuration;
    [Range(1f, 10f)] public float openedScale = 6f;
    [SerializeField] AnimationCurve openAnimationCurve;

    float openAnimationTime = 0f;

    [Space]
    [Header("Close Animation")]
    [Space]
    [SerializeField] float closeAnimationDuration;
    [SerializeField] AnimationCurve closeAnimationCurve;

    float closeAnimationTime = 0f;

    RectTransform phoneRectTransform;
    bool opened = false;
    Vector3 initialPos;
    private Vector2 pointerOffset;

    [SerializeField]
    private GameObject cameraMap;
    private CameraMapMoving cmm;
    private CameraZoomOrthografic czo;
    private bool isInAnimation;

    private void Start()
    {
        phoneRectTransform = GetComponent<RectTransform>();
        initialPos = transform.position;
        cmm = cameraMap.GetComponent<CameraMapMoving>();
        czo = cameraMap.GetComponent<CameraZoomOrthografic>();
        isInAnimation = false;
    }

    public void OnPointerClick()
    {
        if (!opened)
        {
            cmm.CanUseCameraMap(opened);
            czo.CanZoom(opened);
            isInAnimation = true;
            StartCoroutine(StartOpenAnimation());
            
        }
    }

    IEnumerator StartOpenAnimation()
    {
        while(openAnimationTime < openAnimationDuration)
        {
            float time = openAnimationTime / openAnimationDuration;

            float interpolationRatio = openAnimationCurve.Evaluate(time);

            transform.position = Vector3.Lerp(transform.position, transform.parent.position, interpolationRatio);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(openedScale, openedScale, transform.localScale.z), interpolationRatio);

            openAnimationTime += Time.deltaTime;

            yield return null;
        }

        openAnimationTime = 0f;
        opened = true;
        isInAnimation = false;
    }

    public void Close()
    {
        if(opened)
        {
            cmm.CanUseCameraMap(opened);
            czo.CanZoom(opened);
            isInAnimation = true;
            StartCoroutine(StartCloseAnimation());
            
        }
            
    }

    IEnumerator StartCloseAnimation()
    {
        while(closeAnimationTime < closeAnimationDuration) 
        {
            float time = closeAnimationTime / closeAnimationDuration;

            float interpolationRatio = closeAnimationCurve.Evaluate(time);

            transform.position = Vector3.Lerp(transform.position, initialPos, interpolationRatio);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), interpolationRatio);


            closeAnimationTime += Time.deltaTime;

            yield return null;
        }

        closeAnimationTime = 0f;
        opened = false;
        isInAnimation = false;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(opened && !isInAnimation)
            pointerOffset = eventData.position - phoneRectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (opened && !isInAnimation)
            phoneRectTransform.anchoredPosition = eventData.position - pointerOffset;
    }

}
