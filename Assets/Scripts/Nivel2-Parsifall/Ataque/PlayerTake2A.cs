using UnityEngine;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTake2A : MonoBehaviour
{
    [Header("Controles")]
    public bool coger = false;
    public bool cogido = false;
    public bool espera = false;
    public bool instrumentoEntregado = false;

    private GameObject instrumentoActual;
    private float tiempoEspera = 2f;
    private float tiempoInicioEspera;

    void Update() {
        // 🆕 LÍNEA 31 SIMPLIFICADA - SIN tocaDiscos
        if (espera && !cogido) {
            if (Time.time - tiempoInicioEspera > tiempoEspera) {
                coger = false;
                espera = false;
            }
        }
    }
    public void añadirInstrumento(GameObject instrumento) {
        instrumentoActual = instrumento;
        cogido = true;
        espera = true;
        tiempoInicioEspera = Time.time;
    }
    public void eliminarInstrumento() {
        instrumentoActual = null;
        cogido = false;
        espera = false;
    }
    // SIMPLIFICADO - SIN tocaDiscos
    public GameObject getObjetoCogido() {
        return instrumentoActual;
    }

    public bool InstrumentoEntregado() {
        return instrumentoEntregado;
    }
    public void SetCoger(bool state) {
        coger = state;
    }
    public void SetInstrumentoEntregado(bool state) {
        instrumentoEntregado = state;
    }
}