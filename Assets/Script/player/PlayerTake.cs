using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
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
    private GameObject objetoCogido;
    public bool instrumentoEntregado = false;

    private void Update()
    {
        // actualizar el estado de si hay un objeto cogido
        cogido = GetComponentInChildren<cogerInstrumento>().objetoCogido();
    }

    // funcion para cambiar el estado de coger al pulsar el boton(E por ahora)
    private void OnCoger(InputValue value)
    {
        //Debug.LogError("Pulsado boton coger");
        if (value.isPressed)
            espera = false;
        if (coger == true)
        {
            coger = false;
        }
        else if (coger == false)
        {
            coger = true;
            StopCoroutine("delay");
            StartCoroutine(delay());
        }
    }
    private void OnInteractuar(InputValue value)
    {
        if (value.isPressed)
        {
            if (cogido == true)
            {
                instrumentoEntregado = true;
                Debug.Log("Instrumento entregado: " + instrumentoEntregado);
            }
        }
    }
    public void añadirInstrumento(GameObject objeto)
    {
        objetoCogido = objeto;
    }
    public void eliminarInstrumento(GameObject objeto)
    {
        objetoCogido = objeto;
    }
    public GameObject getObjetoCogido()
    {
        return objetoCogido;
    }
    public bool InstrumentoCogido()
    {
        return cogido;
    }
    public void SetCoger(bool valor)
    {
        coger = valor;
    }
    public bool InstrumentoEntregado()
    {
        return instrumentoEntregado;
    }
    public void SetInstrumentoEntregado(bool valor)
    {
        instrumentoEntregado = valor;
    }

    // corrutina para esperar un tiempo para coger un objeto, sino coger se pone a false en el script cogerInstrumento la parte de Update
    IEnumerator delay()
    {
        yield return new WaitForSeconds(timeDelay);
        //Debug.LogError("Espera " + timeDelay + " segundos");
        espera = true;
    }
}