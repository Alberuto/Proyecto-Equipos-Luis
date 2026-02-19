// Assets/Scripts/Nivel0b-Secuencias/PianoInputManagerSecuencias.cs
using UnityEngine;
using System.Collections.Generic;

public class PianoInputManagerSecuencias : MonoBehaviour {

    [System.Serializable]
    public class PianoKey {
        public string nombreNota;
        public KeyCode inputKey;
        public GameObject teclaObjeto;
    }
    [Header("Teclas")]
    public List<PianoKey> teclas = new List<PianoKey>();

    [Header("Secuencias")]
    public AttackManagerSecuencia secuenciaManager;

    void Start() {
        if (secuenciaManager == null)
            secuenciaManager = FindObjectOfType<AttackManagerSecuencia>();
    }
    void Update() {
        foreach (PianoKey tecla in teclas) {
            if (Input.GetKeyDown(tecla.inputKey)) {
                ActivarTecla(tecla);
            }
        }
    }
    public void ActivarTecla(PianoKey tecla) {
        Debug.Log($"🎹 [{tecla.nombreNota}] F6-F12");

        if (secuenciaManager != null) {
            secuenciaManager.RegistrarNotaJugador(tecla.nombreNota);
        }
        Debug.Log($"🔊 Reproduciendo {tecla.nombreNota}");
    }
}