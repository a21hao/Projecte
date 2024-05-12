using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PointsVendingMachine;

public class VendingSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabVendingMachine;
    public GameObject prefabVendingMachine2;
    public GameObject prefabVendingMachine3;
    private GameObject vendingInstantiate;
    //private GameObject lastVendingInstantiate;
    private VendingMachineController vmc;
    private VendingMachineController lastvmc;
    private bool isDraging;
    private bool hasClicked;
    [SerializeField]
    private GameObject pointsOfVendingMachineFather;
    private PointsVendingMachine pvm;
    private PointVending pointVending;
    private BoxCollider machineCollider;
    private bool isInGoodPosition;
    void Start()
    {
        isDraging = false;
        hasClicked = false;
        isInGoodPosition = false;
        pvm = pointsOfVendingMachineFather.GetComponent<PointsVendingMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (hasClicked && !Mouse.current.leftButton.isPressed)
        {
            
            Debug.Log("enter");
            vmc.ChangeCanUseCamera(true);
            hasClicked = false;
            if(isInGoodPosition)
            {
                pvm.changePointVendingToBusy(pointVending, true);
                vmc.GetOutImageCanvas();
                machineCollider.enabled = true;
            }
            else
            {
                Destroy(vendingInstantiate);
            }
        }*/
        if (hasClicked && !Mouse.current.leftButton.isPressed)
        {
            hasClicked = false;
            if(lastvmc!=null)
            {
                lastvmc.ChangeCanUseCamera(true);
            }          
            
        }
            if (isDraging)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // Convertir la posici�n del rat�n a una posici�n en el mundo
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 1000));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //Instantiate(prefab, hit.point, Quaternion.identity);
                //Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
                //Vector3 positionSpawn = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
                pointVending = pvm.isNearSomePoint(hit.point);
                
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                if (pointVending != null)
                {
                    vendingInstantiate.transform.position = pointVending.transformPoint.position;
                    vendingInstantiate.transform.rotation = pointVending.transformPoint.rotation;
                    vmc.ChangeToColorUsefull(true);
                    isInGoodPosition = true;
                }
                else
                {
                    
                    vendingInstantiate.transform.position = hit.point;
                    vmc.ChangeToColorUsefull(false);
                    isInGoodPosition = false;
                }
                
                
            }
            if (Mouse.current.leftButton.isPressed && !hasClicked)
            {
                
                isDraging = false;
                hasClicked = true;
                lastvmc = vmc;
                pvm.changeVisibilityPoints(false);
                if (isInGoodPosition && MoneyManager.instance.DineroTotal >= vmc.GetMoneyToBuyMachine())
                {
                    pvm.changePointVendingToBusy(pointVending, true);
                    vmc.GetOutImageCanvas();
                    vmc.GetOutTextCanvas();
                    ObjectivesAndStats.cumplirObjetivoTutorialColocarMaquina();
                    machineCollider.enabled = true;
                    MoneyManager.instance.DecrementarDinero(vmc.GetMoneyToBuyMachine());
                    
                }
                else
                {
                    Destroy(vendingInstantiate);
                }

            }

        }
       
    }

    public void SpawnVendingMachine(int cat)
    {
        hasClicked = false;
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Convertir la posici�n del rat�n a una posici�n en el mundo
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 1000));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Instantiate(prefab, hit.point, Quaternion.identity);
            //Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            //Vector3 positionSpawn = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
            Quaternion rotation = Quaternion.Euler(0, 90, 0);
            //vendingInstantiate = Instantiate(prefabVendingMachine, hit.point, prefabVendingMachine.transform.rotation);
            if(cat == 1) vendingInstantiate = Instantiate(prefabVendingMachine, hit.point, rotation);
            if(cat == 2) vendingInstantiate = Instantiate(prefabVendingMachine2, hit.point, rotation);
            if(cat == 3) vendingInstantiate = Instantiate(prefabVendingMachine3, hit.point, rotation);
            pvm.changeVisibilityPoints(true);
            vmc = vendingInstantiate.GetComponent<VendingMachineController>();
            machineCollider = vendingInstantiate.GetComponent<BoxCollider>();
            machineCollider.enabled = false;
            vmc.ChangeToColorUsefull(false);
            vmc.ChangeCanUseCamera(false);
            if (vendingInstantiate != null)
            {
                isDraging = true;
            }
        }
    }

    public void DragMachine()
    {

    }
 }