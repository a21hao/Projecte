using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MachineInventory : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private Transform slotsparent;
    [SerializeField]
    private int maxSlots = 15;
    [SerializeField]
    private int maxQuantityOfSlot = 4;
    private bool firtItemInserted = false;
    private ObjectsList objects;
    [SerializeField]
    private int maxItemsToPut;
    [SerializeField]
    private GameObject prefabVenta;
    [SerializeField]
    private Transform puntoDeSpawnVenta;
    private List<GameObject> listaDeVentas;
    [SerializeField]
    private float timeSpawner;
    private float counterTime = 0;
    //private ObjectivesAndStats objAndStats;
    
    public class Slot
    {
        public int maxQuantity = 4;
        public Item item = null;
        public int quantity = 0;
        public Transform slotparent;
        public GameObject[] itemsObjects;
    }

    private Slot[] slots;
    void Start()
    {
        //objAndStats = GameObject.Find("Canvas/Men�/Phone/Perfil").gameObject.GetComponent<ObjectivesAndStats>();
        //Debug.Log("Objs and stats " + objAndStats != null);
        listaDeVentas = new List<GameObject>();
        objects = GameObject.Find("ObjectList").GetComponent<ObjectsList>();
        //slotsparent = transform.Find("Slots");
        slots = new Slot[maxSlots];
        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = new Slot();
            slot.maxQuantity = maxQuantityOfSlot;
            slot.slotparent = slotsparent.GetChild(i);
            slot.itemsObjects = new GameObject[4];
            slots[i] = slot;
        }
        

    }

    public void PutItem(Item itemm)
    {
        Debug.Log("Ha entrado maquina");
        int quantityOfitemmInitial = itemm.GetCantidad();
        int quantityOfitemm = itemm.GetCantidad();
        if(quantityOfitemmInitial > maxItemsToPut)
        {            
            itemm.SetCantidad(maxItemsToPut);
            quantityOfitemm = maxItemsToPut;
        }
        
        if (firtItemInserted)
        {
            if (CheckIfMachineIsEmty())
            {
                ObjectivesAndStats.Instance.cumplirObjetivoRellenaMaquinaCuandoEsteVacia();
            }
        }
        int quantityToRest = HasSlotOfThatItem(itemm);
        quantityOfitemmInitial -= quantityToRest;
        quantityOfitemm -= quantityToRest;
        itemm.SetCantidad(quantityOfitemm);
        quantityOfitemmInitial -= putItemsWithoutOthers(itemm);
        itemm.SetCantidad(quantityOfitemmInitial);
        ObjectivesAndStats.Instance.cumplirObjetivoColocaPrimerObjetoEnMaquina();
        firtItemInserted = true;
        
    }

    private int HasSlotOfThatItem(Item itemm)
    {
        int idItem = itemm.GetID();
        int QuantityItemInitial = itemm.GetCantidad();
        int QuantityToRest = 0;
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.GetID() == itemm.GetID() && slots[i].quantity > 0 && slots[i].quantity < 4 & QuantityItemInitial > 0)
                {
                    int slotMaxtoPut = slots[i].maxQuantity - slots[i].quantity;
                    if (QuantityItemInitial >= slotMaxtoPut)
                    {
                        QuantityToRest += slotMaxtoPut;
                        QuantityItemInitial -= slotMaxtoPut;
                        for (int j = slots[i].quantity; j < slots[i].maxQuantity; ++j)
                        {
                           slots[i].itemsObjects[j] = Instantiate(itemm.GetObjeto3d(), slots[i].slotparent.GetChild(j));
                           slots[i].itemsObjects[j].transform.localPosition = Vector3.zero;

                        }

                        slots[i].quantity = 4;
                    }
                    else
                    {
                        for (int j = slots[i].quantity; j < slots[i].quantity + QuantityItemInitial; ++j)
                        {
                            slots[i].itemsObjects[j] = Instantiate(itemm.GetObjeto3d(), slots[i].slotparent.GetChild(j));
                            slots[i].itemsObjects[j].transform.localPosition = Vector3.zero;

                        }
                        QuantityToRest += QuantityItemInitial;
                        slots[i].quantity += QuantityItemInitial;
                        QuantityItemInitial = 0;
                    }

                    slots[i].item.SetInformacion(itemm.nombreText, itemm.spriteImage.sprite, itemm.precioObjeto, itemm.descripcionObjeto, itemm.GetID(), itemm.tipo, slots[i].quantity, itemm.Objeto3d, itemm.precioVenta);
                }
            }
            
            
        }
        return QuantityToRest;
    }

    private int putItemsWithoutOthers(Item itemm)
    {
        int QuantityItemInitial = itemm.GetCantidad();
        int QuantityToRest = 0;
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null && QuantityItemInitial > 0)
            {
                if(QuantityItemInitial <= 4)
                {
                    QuantityToRest += QuantityItemInitial;
                    for (int j = 0; j < QuantityItemInitial; ++j)
                    {
                        slots[i].itemsObjects[j] = Instantiate(itemm.GetObjeto3d(), slots[i].slotparent.GetChild(j));
                        slots[i].itemsObjects[j].transform.localPosition = Vector3.zero;
                    }
                    slots[i].quantity = QuantityItemInitial;
                    QuantityItemInitial = 0;
                    
                }
                else
                {
                    for (int j = 0; j < 4; ++j)
                    {
                        slots[i].itemsObjects[j] = Instantiate(itemm.GetObjeto3d(), slots[i].slotparent.GetChild(j));
                        slots[i].itemsObjects[j].transform.localPosition = Vector3.zero;
                    }
                    QuantityToRest += 4;
                    QuantityItemInitial -= 4;
                    slots[i].quantity = 4;
                }
                Item slotitem = Instantiate(itemm);
                slotitem.SetInformacion(itemm.nombreText, itemm.spriteImage.sprite, itemm.precioObjeto, itemm.descripcionObjeto, itemm.GetID(), itemm.tipo, slots[i].quantity, itemm.Objeto3d, itemm.precioVenta);
                slots[i].item = slotitem;

            }
        }
        return QuantityToRest;
    }
    
    public int VenderItem(int ID, int cantidad)
    {
        int itemsSold = 0;
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                Debug.Log(slots[i].item.GetID());
                if(slots[i].item.GetID() == ID && cantidad > 0 && slots[i].quantity > 0)
                {
                    GameObject prefVenta = Instantiate(prefabVenta, puntoDeSpawnVenta.position, Quaternion.identity);
                    int cantidadVenta = 0;
                    int precioVenta = 0;
                    if (slots[i].quantity > cantidad)
                    {
                        for(int j = slots[i].quantity - 1; j > slots[i].quantity - 1 - cantidad; j--)
                        {
                            Destroy(slots[i].itemsObjects[j]);
                        }
                        itemsSold += cantidad;
                        slots[i].quantity -= cantidad;
                        MoneyManager.instance.IncrementarDinero(cantidad * objects.VendingPriceOfObjectbyId(slots[i].item.GetID())/*slots[i].item.precioVenta*/);
                        cantidadVenta += cantidad;
                        precioVenta += cantidad * objects.VendingPriceOfObjectbyId(slots[i].item.GetID());
                        ObjectivesAndStats.Instance.updateStat(slots[i].item.GetID(), cantidad);
                       // objAndStats.updateStat(slots[i].item.GetID(), cantidad);
                        //SoldItem.Invoke(slots[i].item.GetID(), cantidad);
                        cantidad = 0;
                        
                    }
                    if (cantidad >= slots[i].quantity)
                    {
                        for (int j = 0; j < slots[i].itemsObjects.Length; j++)
                        {
                            if (slots[i].itemsObjects[j] != null)
                            Destroy(slots[i].itemsObjects[j]);
                        }
                        itemsSold += slots[i].quantity;
                        cantidad -= slots[i].quantity;
                        MoneyManager.instance.IncrementarDinero(slots[i].quantity * objects.VendingPriceOfObjectbyId(slots[i].item.GetID()));
                        cantidadVenta += slots[i].quantity;
                        precioVenta += slots[i].quantity * objects.VendingPriceOfObjectbyId(slots[i].item.GetID());
                        ObjectivesAndStats.Instance.updateStat(slots[i].item.GetID(), slots[i].quantity);
                        //objAndStats.updateStat(slots[i].item.GetID(), slots[i].quantity);
                        //SoldItem.Invoke(slots[i].item.GetID(), slots[i].quantity);
                        slots[i].quantity = 0;                        
                        slots[i].item = null;
                    }
                    prefabVenta.GetComponent<VentasPrefab>().SetTextVenta("+" + precioVenta + "¥");
                    prefabVenta.GetComponent<VentasPrefab>().SetImageVenta(slots[i].item.spriteImage.sprite);
                    prefabVenta.GetComponent<VentasPrefab>().SetCantidadVenta(cantidadVenta);
                    prefabVenta.SetActive(false);
                    listaDeVentas.Add(prefVenta);
                } 
                
            }
            
            
        }
        return itemsSold;
    }

    private bool CheckIfMachineIsEmty()
    {
        bool isEmpty = true;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].quantity > 0)
            {
                isEmpty = false;
            }
        }
        return isEmpty;
    }

    private void Update()
    {

        if(listaDeVentas.Count > 0)
        {
            counterTime += Time.deltaTime;
            if(counterTime >= timeSpawner)
            {
                listaDeVentas[0].SetActive(true);
                listaDeVentas.RemoveAt(0);
                counterTime = 0;
            }
        }
        
    }





}
