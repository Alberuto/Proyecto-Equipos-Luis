using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AttackManager : MonoBehaviour {
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI cronometroText;
    [SerializeField] private TextMeshProUGUI comboNombreText;
    [SerializeField] private TextMeshProUGUI multiplicadorText;

    [Header("Secuencia objetivo")]
    private string secuenciaHeredada = "";
    private List<string> secuenciaObjetivo = new List<string>();
    private List<string> secuenciaJugador = new List<string>();
    private bool ataqueActivo = false;
    private int intentosExitosos = 0;
    private float tiempoRestante = 30f;
    private bool tiempoTerminado = false;

    [Header("Audio")]
    private AudioSource feedbackAudio;
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoError;

    [Header("Combat")]
    [SerializeField] private CombatManager combatManager;

    [Header("Flash")]
    [SerializeField] private FlashEffect flashEffect;

    [Header("Config")]
    [SerializeField] private float tiempoTotal=30f;
    [SerializeField] private bool nivelDificil = false;

    private static readonly string[] nombresCombo = new string[] {
        "Impacto inicial",       // 1
        "Imparable",             // 2
        "Demoledor",             // 3
        "Brutal",                // 4
        "Ritmo ardiente",        // 5
        "Aplastante",            // 6
        "Poder desatado",        // 7
        "Legendario",            // 8
        "Ejecución perfecta",    // 9
        "Más allá del infinito", // 10
        "Límite roto",           // 11
        "Dodecafonismo supremo"  // 12
    };

    private int valorPuntosRiff = 1;

    void Start() {

        Debug.Log("🎸 AttackManager INICIADO");
        Debug.Log("🔊 AudioSource: " + (GetComponent<AudioSource>() != null));
        feedbackAudio = GetComponent<AudioSource>();
        IniciarCronometro();
    }
    void Update() {

        if (ataqueActivo && !tiempoTerminado) {
            tiempoRestante -= Time.deltaTime;
            ActualizarUI();

            if (tiempoRestante <= 0) {
                FinalizarAtaque();
            }
        }
    }
    private void ActualizarUI() {

        cronometroText.text = $"Tiempo: {tiempoRestante:F1}s";
        multiplicadorText.text = $"X {intentosExitosos}";
        int nivel = Mathf.Clamp(intentosExitosos, 0, 12);
        if (nivel <= 0)
            comboNombreText.text = "";
        else
            comboNombreText.text = nombresCombo[nivel - 1];
    }
    private void FinalizarAtaque() {

        tiempoTerminado = true;
        ataqueActivo = false;
        int danoFinal = intentosExitosos * valorPuntosRiff;

        Debug.Log($"⏰ Tiempo terminado! Ataques exitosos: {intentosExitosos} , (valor {valorPuntosRiff}) , secuencias → {danoFinal} daño");

        if (combatManager != null) {
            Debug.Log("⚔️ CombatManager OK → RecibirAtaque()");
            combatManager.RecibirAtaque(danoFinal);
        }
        else { 
            Debug.LogError("❌ CombatManager NO ASIGNADO en AttackManager!");
        }
        PlayerPrefs.SetInt("Nivel1IntentosExitosos", intentosExitosos);
        PlayerPrefs.SetInt("Nivel1AtaqueDaño", danoFinal);
        PlayerPrefs.SetFloat("Nivel1VidaJugador", combatManager.vidaJugadorActual);
        PlayerPrefs.SetFloat("Nivel1VidaBoss", combatManager.vidaBossActual);
        PlayerPrefs.Save();
        Debug.Log("▶️ INICIANDO DecidirSiguienteFase()");
        StartCoroutine(DecidirSiguienteFase());
    }
    // Método auxiliar para convertir string de attack selector en List<string> para comparación
    private List<string> ConvertirSecuenciaStringALista(string secuencia) {

        List<string> listaNotas = new List<string>();
        string notaActual = "";

        for (int i = 0; i < secuencia.Length; i++) {
            char c = secuencia[i];
            if (c == '#') {
                notaActual += c;
            }
            else {
                // Si hay nota anterior, la guardamos
                if (!string.IsNullOrEmpty(notaActual)) {

                    listaNotas.Add(notaActual);
                }
                notaActual = c.ToString();
            }
        }
        // Guardar última nota
        if (!string.IsNullOrEmpty(notaActual)) {

            listaNotas.Add(notaActual);
        }
        return listaNotas;
    }
    // Llamado desde AttackSelector
    public void IniciarAtaque(string secuencia, int damageBase) {

        valorPuntosRiff = damageBase;
        secuenciaObjetivo = ConvertirSecuenciaStringALista(secuencia); 
        secuenciaHeredada = secuencia;
        secuenciaJugador.Clear();
        ataqueActivo = true;
        intentosExitosos = 0; // reset combo x turno
        ActualizarUI();
        Debug.Log("¡Ataque iniciado! Objetivo: [" + string.Join(", ", secuenciaObjetivo) + "]");
    }
    public void RegistrarNota(string nota) {

        if (!ataqueActivo || tiempoTerminado) return;
        secuenciaJugador.Add(nota);
        Debug.Log("Nota: " + nota + " | Progreso: [" + string.Join(", ", secuenciaJugador) + "]");
        bool coincideCompleta = secuenciaJugador.Count == secuenciaObjetivo.Count;
        bool errorEnPosicion = false;

        for (int i = 0; i < secuenciaJugador.Count; i++) {
            if (secuenciaJugador[i] != secuenciaObjetivo[i]) {
                errorEnPosicion = true;
                break;
            }
        }
        if (coincideCompleta && !errorEnPosicion) {
            if (nivelDificil) {
                // 🎯 NIVEL3-5: x12 DIRECTO (sin contar intentos)
                intentosExitosos = 12;
                Debug.Log("🔥 MODO DIFÍCIL → COMBO x12 FIJO!");
            }
            // 🆕 ÉXITO SECUENCIA → Reiniciar para siguiente intento
            intentosExitosos = Mathf.Min(intentosExitosos + 1, 12);
            //sonido acierto
            if (feedbackAudio != null && sonidoCorrecto != null) {
                feedbackAudio.PlayOneShot(sonidoCorrecto);  
            }
            // 🆕 FLASH COMBO
            if (flashEffect != null) {
                flashEffect.FlashCombo(intentosExitosos);
            }
            Debug.Log($"✅ SECUENCIA {intentosExitosos} EXITOSA!");
            ReiniciarSecuencia();  // ← NUEVA FUNCIÓN
            ActualizarUI();
            return;
        }
        else if (errorEnPosicion) {
            // 🆕 ERROR → Reiniciar secuencia actual
            Debug.Log("❌ Error en posición!");
            if (feedbackAudio != null && sonidoError != null)
                feedbackAudio.PlayOneShot(sonidoError);
            ReiniciarSecuencia();
        }
    }
    public void IniciarCronometro() {
        tiempoRestante = tiempoTotal;
        tiempoTerminado = false;
        ActualizarUI();
    }
    private void ReiniciarSecuencia() {
        secuenciaJugador.Clear(); // NO limpiamos secuenciaObjetivo (permanece igual)
    }
    private IEnumerator DecidirSiguienteFase() {

        yield return new WaitForSeconds(1.5f);
        // 🆕 LEER PlayerPrefs en lugar de CombatManager
        float vidaBossPersistente = PlayerPrefs.GetFloat("Nivel1VidaBoss", 100f);
        float vidaBossMax = 100f;
        Debug.Log($"🎯 Vida Boss PlayerPrefs: {vidaBossPersistente}/{vidaBossMax}");

        if (vidaBossPersistente <= 0) {
            Debug.Log("🎉 ¡BOSS DERROTADO! NIVEL 2");
            PlayerPrefs.SetInt("Nivel", 1);
            SceneManager.LoadScene("Nivel1");
        }
        else if (vidaBossPersistente > 50f) {
            Debug.Log("🛡️ Boss fuerte → Nivel1-Defensa");
            SceneManager.LoadScene("Nivel1-Defensa");
        }
        else if (vidaBossPersistente <= 50f && (PlayerPrefs.GetInt("Fury", 0) == 0)) {
            PlayerPrefs.SetInt("Fury", 1);
            SceneManager.LoadScene("Nivel1");
        }
        else {
            SceneManager.LoadScene("Nivel1-Fury");
        }
    }
}