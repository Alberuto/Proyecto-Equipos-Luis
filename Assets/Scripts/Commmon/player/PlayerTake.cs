using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
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
    public bool fallo = false;
    private int dificultad = 12;

    private void Start()
    {
        tocaDiscosObject = GameObject.FindGameObjectWithTag("tocaDiscos");
        if (tocaDiscosObject != null)
        {
            tocaDiscos = tocaDiscosObject.GetComponent<tocaDiscosManager>();
        }
        
        //dificultad = GameManager.getDificultad();
    }
    private void Update()
    {
        // actualizar el estado de si hay un objeto cogido
       // cogido = GetComponentInChildren<cogerInstrumento>().objetoCogido();
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
                    NotaObjeto notaObj = objetoCogido.GetComponent<NotaObjeto>();
                    if (notaObj != null)
                        ReproducirNotaObjeto(notaObj);

                    instrumentoEntregado = true;
                    // Debug.Log("Instrumento entregado: " + instrumentoEntregado);

                    // 🆕 TUTORIAL 2A: Registrar objeto cogido
                    AttackManagerTutorialObjetos2A tutorial2a = FindObjectOfType<AttackManagerTutorialObjetos2A>();
                    if (tutorial2a != null)
                    {
                        tutorial2a.RegistrarObjetoCogido(objetoCogido.tag);
                        Debug.Log($"🎯 2A Tutorial: {objetoCogido.tag} registrado");
                    }

                    AttackManagerSecuenciaA2 tutorial2a2 = FindObjectOfType<AttackManagerSecuenciaA2>();
                    if (tutorial2a2 != null)
                    {
                        tutorial2a2.RegistrarNotaJugador(objetoCogido.tag);
                        Debug.Log($"🎯 2A2 Tutorial: {objetoCogido.tag} registrado");
                    }
                    AttackManagerSecuencia3 attackMgr = FindObjectOfType<AttackManagerSecuencia3>();
                    if (attackMgr != null)
                    {
                        attackMgr.RegistrarNotaJugador(objetoCogido.tag);
                        Debug.Log($"🎯 3 ataque: {objetoCogido.tag} registrado");
                    }
                                       

                    //compruebo si falla la nota para no guadarla en el tocadiscos
                    if (fallo)
                    {
                        Destroy(objetoCogido);
                        Debug.Log("fallo");
                        fallo = false;
                    }
                    else
                    {
                        tocaDiscos.setNota(objetoCogido);
                    }
                    cogido = false;
                    Debug.Log("cogido del player take: "+cogido);
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
            if (tocaDiscos != null)
            {
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
    }
    private void ReproducirNotaObjeto(NotaObjeto notaObj) {
        AudioSource playerAudio = GetComponent<AudioSource>();
        if (playerAudio != null && notaObj.notaClip != null) {
            playerAudio.PlayOneShot(notaObj.notaClip);
            Debug.Log($"🎵 Reproducido: {notaObj}");
        }
        else {
            Debug.LogWarning("❌ Sin AudioSource en Player o sin notaClip");
        }
    }
    public void añadirInstrumento(GameObject objeto) {
        objetoCogido = objeto;
    }
    public void eliminarInstrumento() {
        if (objetoCogido != null) {   
            objetoCogido = null;
        }
    }
    public GameObject getObjetoCogido() {
        return objetoCogido;
    }
    public void InstrumentoCogido(bool objeto) {
        cogido = objeto;
    }
    public void SetCoger(bool valor) {
        coger = valor;
    }
    public bool InstrumentoEntregado() {
        return instrumentoEntregado;
    }
    public void SetInstrumentoEntregado(bool valor) {
        instrumentoEntregado = valor;
    }
    // corrutina para esperar un tiempo para coger un objeto, sino coger se pone a false en el script cogerInstrumento la parte de Update
    IEnumerator delay() {
        yield return new WaitForSeconds(timeDelay);
        //Debug.LogError("Espera " + timeDelay + " segundos");
        espera = true;
    }
}