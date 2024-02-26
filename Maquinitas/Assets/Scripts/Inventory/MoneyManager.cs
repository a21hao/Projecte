using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    private static int dineroTotal = 50;
    public TextMeshProUGUI textoDinero; 

    public static int DineroTotal
    {
        get { return dineroTotal; }
        set { dineroTotal = value; }
    }

    public static void IncrementarDinero(int cantidad)
    {
        dineroTotal += cantidad;
        ActualizarTextoDinero();
    }

    public static void DecrementarDinero(int cantidad)
    {
        dineroTotal -= cantidad;
        ActualizarTextoDinero();
    }

    private static void ActualizarTextoDinero()
    {
        MoneyManager[] moneyManagers = FindObjectsOfType<MoneyManager>();
        foreach (MoneyManager manager in moneyManagers)
        {
            manager.textoDinero.text = dineroTotal.ToString();
        }
    }
}