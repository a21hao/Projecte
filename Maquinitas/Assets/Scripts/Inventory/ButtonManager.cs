using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject amazing;
    public GameObject almacen;
    public GameObject mapa;
    public GameObject perfil;
    public GameObject ajustes;
    public GameObject upgrades;
    [SerializeField] private GameObject tablet;
    [SerializeField] private GameObject calendar;
    private Animator almacenAnimator;
    private Animator amazingAnimator;
    private Animator tabletAnimator;
    private Animator mapaAnimator;
    private Animator perfilAnimator;
    private Animator upgradesAnimator;
    [SerializeField] private Animator ajustesAnimator;
    [SerializeField] private Animator calendarAnimator;
    private bool canUseToggleTablet;
    private bool isTabletInUse;
    
    [SerializeField]
    private GameObject cameraMap;
    private CameraMapMoving cmm;
    private CameraZoomOrthografic czo;

    private void Start()
    {
        //Debug.Log("StartEntered");
        cmm = cameraMap.GetComponent<CameraMapMoving>();
        czo = cameraMap.GetComponent<CameraZoomOrthografic>();
        upgradesAnimator = upgrades.GetComponent<Animator>();
        canUseToggleTablet = true;
        isTabletInUse = false;
        perfilAnimator = perfil.GetComponent<Animator>();
        almacenAnimator = almacen.GetComponent<Animator>();
        amazingAnimator = amazing.GetComponent<Animator>();
        tabletAnimator = tablet.GetComponent<Animator>();       
        mapaAnimator = mapa.GetComponent<Animator>();
        ajustesAnimator = ajustes.GetComponent<Animator>();
        calendarAnimator = calendar.GetComponent<Animator>();
    }

    IEnumerator ToggleGameObject(GameObject obj, Animator animator)
    {
        animator.SetTrigger("Abrir");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length*5);
        obj.SetActive(!obj.activeSelf);
        if(obj == tablet)
        {
            Debug.Log("he entrado");
            
        }
    }


    public void ToggleAmazing()
    {
        //StartCoroutine(ToggleGameObject(amazing, amazingAnimator));

        amazingAnimator.SetTrigger("Abrir");
        if (!amazing.activeSelf) amazing.SetActive(true);
    }

    public void ToggleAlmacen()
    {
        //StartCoroutine(ToggleGameObject(almacen, almacenAnimator));
        almacenAnimator.SetTrigger("Abrir");
        ObjectivesAndStats.Instance.cumplirObjetivoAbreElAlmacen();
        if (!almacen.activeSelf) almacen.SetActive(true);
    }

    public void ToggleMapa()
    {
        //StartCoroutine(ToggleGameObject(mapa, mapaAnimator));
        //mapaAnimator.SetTrigger("Abrir");
        if (!mapa.activeSelf) mapa.SetActive(true);
        else mapa.SetActive(false);
    }

    public void TogglePerfil()
    {
        //StartCoroutine(ToggleGameObject(perfil, perfilAnimator));
        perfilAnimator.SetTrigger("Abrir");
        if (!perfil.activeSelf) perfil.SetActive(true);
    }

    public void ToggleAjustes()
    {
        //StartCoroutine(ToggleGameObject(ajustes, ajustesAnimator));
        ajustesAnimator.SetTrigger("Abrir");
        if (!ajustes.activeSelf) ajustes.SetActive(true);
    }

    public void ToggleUpgrades()
    {
        upgradesAnimator.SetTrigger("Abrir");
        if (!upgrades.activeSelf) upgrades.SetActive(true);
        //StartCoroutine(ToggleGameObject(upgrades, upgradesAnimator));

    }

    public void ToggleTablet()
    {
        //StartCoroutine(ToggleGameObject(tablet, tabletAnimator));
        if(canUseToggleTablet)
        {
            if (!tablet.activeSelf) tablet.SetActive(true);
            tabletAnimator.SetTrigger("Abrir");
            cmm.CanUseCameraMap(isTabletInUse);
            czo.CanZoom(isTabletInUse);
            isTabletInUse = !isTabletInUse;
            
        }
        
    }

    public void ToggleCalendar()
    {
        //obj.SetActive(!obj.activeSelf);
        calendarAnimator.SetTrigger("Abrir");
        if (!calendar.activeSelf) calendar.SetActive(true);
    }


    public void ToggleAll()
    {
        ToggleAmazing();
        ToggleMapa();
        ToggleAlmacen();
        ToggleTablet();
        TogglePerfil();
        ToggleAjustes();
        ToggleTablet();
    }

    public void CanUseToggleTablet(bool canUse)
    {
        canUseToggleTablet = canUse;
    }

}
