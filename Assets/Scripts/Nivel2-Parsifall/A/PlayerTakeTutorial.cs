using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTakeTutorial : MonoBehaviour {

    public bool coger = false;
    public bool espera = false;
    public bool cogido;
    public float timeDelay = 1f;

    private GameObject tocaDiscosObject;
    private tocaDiscosManagerTutorial tocaDiscosTutorial;
    private GameObject objetoCogido;

    private int dificultad = 12;

    void Start() {
        tocaDiscosObject = GameObject.FindGameObjectWithTag("tocaDiscos");
        if (tocaDiscosObject != null)
        {
            tocaDiscosTutorial = tocaDiscosObject.GetComponent<tocaDiscosManagerTutorial>();
        }
    }
    private void OnCoger(InputValue value) {
        if (value.isPressed)
            espera = false;

        if (coger) {
            coger = false;
        }
        else {
            coger = true;
            StopCoroutine("delay");
            StartCoroutine(delay());
        }
    }
    private void OnInteractuar(InputValue value) {

        if (!value.isPressed) return;

        if (cogido && objetoCogido != null) {
            if (tocaDiscosTutorial == null) {
                Debug.LogWarning("tocaDiscosTutorial no asignado en PlayerTakeTutorial");
                return;
            }
            // capacidad actual del tocadiscos tutorial
            int notasActuales = tocaDiscosTutorial.getNotas().Count;
            if (notasActuales >= dificultad) {
                Debug.Log("No se pueden entregar mas instrumentos, dificultad alcanzada");
                return;
            }

            Debug.Log($"🎯 2A Tutorial: entregando {objetoCogido.tag} al tocadiscosTutorial");
            // Aquí se hace TODA la lógica: correcto → hueco, incorrecto → Destroy + vida
            tocaDiscosTutorial.setNota(objetoCogido);
            // limpiar estado del jugador
            cogido = false;
            eliminarInstrumento();
            SetCoger(false);
        }
    }
    public void añadirInstrumento(GameObject objeto) {
        objetoCogido = objeto;
    }
    public void eliminarInstrumento() {
        if (objetoCogido != null)
            objetoCogido = null;
    }
    public void InstrumentoCogido(bool objeto) {
        cogido = objeto;
    }
    public void SetCoger(bool valor) {
        coger = valor;
    }
    IEnumerator delay() {
        yield return new WaitForSeconds(timeDelay);
        espera = true;
    }
}