using UnityEngine;
using System.Collections.Generic;

public class Calendario : MonoBehaviour
{
    public string[] meses = { "Primavera", "Verano", "Oto�o", "Invierno" };
    public List<string>[,] actividades = new List<string>[4, 28];
    public int mesActual = 0; // �ndice del mes actual

    // M�todo para avanzar al siguiente mes
    public void SiguienteMes()
    {
        mesActual = (mesActual + 1) % 4;
        MostrarCalendario();
    }

    // M�todo para retroceder al mes anterior
    public void MesAnterior()
    {
        mesActual = (mesActual - 1 + 4) % 4;
        MostrarCalendario();
    }

    // M�todo para mostrar el calendario actual
    public void MostrarCalendario()
    {
        // Aqu� puedes implementar la l�gica para mostrar el calendario en la interfaz de usuario
        // Puedes acceder a las actividades utilizando la matriz 'actividades'
    }

    // M�todo para agregar una actividad a un d�a espec�fico
    public void AgregarActividad(int dia, string actividad)
    {
        if (actividades[mesActual, dia] == null)
        {
            actividades[mesActual, dia] = new List<string>();
        }
        actividades[mesActual, dia].Add(actividad);
    }
}
