using UnityEngine;
using System.Collections.Generic;

public class PianoInputManagerTutorial : MonoBehaviour{

    [System.Serializable]
    public class PianoKey {
        public string nombreNota;
        public KeyCode inputKey;
        public GameObject teclaObjeto;
    }

    [Header("Teclas Piano")]
    public List<PianoKey> teclas = new List<PianoKey>();

    [Header("Tutorial")]
    public AttackManagerTutorial0 tutorialManager;

    void Start() {
        // Auto-asignar si no está asignado
        if (tutorialManager == null)
            tutorialManager = FindObjectOfType<AttackManagerTutorial0>();
    }

    void Update() {

        foreach (PianoKey tecla in teclas) {
            if (Input.GetKeyDown(tecla.inputKey)) {
                ActivarTecla(tecla);
            }
        }
    }
    public void ActivarTecla(PianoKey tecla) {

        Debug.Log($"🎹 Tecla {tecla.nombreNota} activada: {tecla.inputKey}");

        // 🔥 TUTORIAL MANAGER
        if (tutorialManager != null) {
            tutorialManager.RegistrarNotaJugador(tecla.nombreNota);
        }
        else {
            Debug.LogError("❌ AttackManagerTutorial NO encontrado!");
        }
        // Sonido/visual de la tecla (mismo que antes)
        if (tecla.teclaObjeto != null) {
            // Aquí tu lógica de animación/sonido existente
            Debug.Log($"🔊 Reproduciendo {tecla.nombreNota}");
        }
    }
}