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
    // tocadiscos interaccion
    private GameObject tocaDiscosObject;
    private tocaDiscosManager tocaDiscos;
    private GameObject objetoCogido;
    public bool instrumentoEntregado = false;
    private int dificultad = 12;

    private void Start()
    {
        tocaDiscosObject = GameObject.FindGameObjectWithTag("tocaDiscos");
        tocaDiscos = tocaDiscosObject.GetComponent<tocaDiscosManager>();
        dificultad = GameManager.getDificultad();
    }
    private void Update()
    {
        // actualizar el estado de si hay un objeto cogido
        cogido = GetComponentInChildren<cogerInstrumento>().objetoCogido();
    }

    // funcion para cambiar el estado de coger al pulsar el boton(E por ahora)
    private void OnCoger(InputValue value)
    {
        //Debug.Log("Pulsado boton coger");
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
                // comprobar si se pueden poner mas instrumentos en el tocadiscos
                if (tocaDiscos.getNotas().Count < dificultad)
                {
                    instrumentoEntregado = true;
                    // Debug.Log("Instrumento entregado: " + instrumentoEntregado);

                    tocaDiscos.setNota(objetoCogido);
                    cogido = false;
                    eliminarInstrumento();
                    SetCoger(false);
                    SetInstrumentoEntregado(false);
                    return;
                }
                else
                {
                    Debug.Log("No se pueden entregar mas instrumentos, dificultad alcanzada");
                }
            }
            // comprobar si se han entregado suficientes instrumentos para activar la secuencia de ataque
            if (tocaDiscos.getNotas().Count >= dificultad && cogido == false)
            {
                Debug.Log("activar secuencia de ataque (comprobar si la secuencia esta bien o no)");
                tocaDiscos.entregarNotas();
                /*if (GameManager.instance.secuenciaCorrecta())
                {
                    Debug.Log("Secuencia correcta, activar ataque");
                }
                else
                {
                    Debug.Log("Secuencia incorrecta, pierde turno");
                }
                */
            }
        }
    }
    public void añadirInstrumento(GameObject objeto)
    {
        objetoCogido = objeto;
    }
    public void eliminarInstrumento()
    {
        if (objetoCogido != null)
        {   
            objetoCogido = null;
        }
        
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