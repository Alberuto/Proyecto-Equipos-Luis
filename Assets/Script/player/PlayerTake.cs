using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// script para gestionar la accion de coger y soltar objetos
public class PlayerTake : MonoBehaviour
{
    // atributos publicos para gestionar el estado de coger y soltar
    public bool coger = false;
    public bool espera = false;
    public bool cogido;
    public float timeDelay = 1f;

    private void Update()
    {
        // actualizar el estado de si hay un objeto cogido
        cogido = GetComponentInChildren<cogerInstrumento>().objetoCogido();
    }

    // funcion para cambiar el estado de coger al pulsar el boton(E por ahora)
    private void OnCoger(InputValue value)
    {
        Debug.LogError("Pulsado boton interactuar");
        if (value.isPressed)
            espera = false;
        if (coger == true)
        {
            coger = false;
        }
        else if (coger == false)
        {
            coger = true;
            StartCoroutine("delay");
        }
    }

    // corrutina para esperar un tiempo para coger un objeto, sino coger se pone a false en el script cogerInstrumento la parte de Update
    IEnumerator delay()
    {
        yield return new WaitForSeconds(timeDelay);
        Debug.LogError("Espera " + timeDelay + " segundos");
        espera = true;
    }
}
