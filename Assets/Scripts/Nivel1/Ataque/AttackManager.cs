using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AttackManager : MonoBehaviour {
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI cronometroText;
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
    }
    private void FinalizarAtaque() {

        tiempoTerminado = true;
        int danoFinal = intentosExitosos * 10;

        Debug.Log($"⏰ Tiempo terminado! Ataques exitosos: {intentosExitosos} secuencias → {danoFinal} daño");

        PlayerPrefs.SetInt("Nivel1AtaqueDaño", danoFinal);
        PlayerPrefs.SetInt("Nivel1IntentosExitosos", intentosExitosos);
        PlayerPrefs.Save();

        //combatManager.FaseAtaqueTerminada(intentosExitosos);
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
    public void IniciarAtaque(string secuencia) {

        secuenciaObjetivo = ConvertirSecuenciaStringALista(secuencia);  //AQUÍ
        secuenciaHeredada = secuencia;
        secuenciaJugador.Clear();
        ataqueActivo = true;
        Debug.Log("¡Ataque iniciado! Objetivo: [" + string.Join(", ", secuenciaObjetivo) + "]");
    }
    public void RegistrarNota(string nota) {

        if (!ataqueActivo || tiempoTerminado) return;

        secuenciaJugador.Add(nota);
        Debug.Log("Nota: " + nota + " | Progreso: [" + string.Join(", ", secuenciaJugador) + "]");

        if (feedbackAudio != null && sonidoCorrecto != null)
            feedbackAudio.PlayOneShot(sonidoCorrecto);

        bool coincideCompleta = secuenciaJugador.Count == secuenciaObjetivo.Count;
        bool errorEnPosicion = false;

        for (int i = 0; i < secuenciaJugador.Count; i++) {

            if (secuenciaJugador[i] != secuenciaObjetivo[i]) {
                errorEnPosicion = true;
                break;
            }
        }
        if (coincideCompleta && !errorEnPosicion) {

            // 🆕 ÉXITO SECUENCIA → Reiniciar para siguiente intento
            intentosExitosos++;
            Debug.Log($"✅ SECUENCIA {intentosExitosos} EXITOSA!");
            ReiniciarSecuencia();  // ← NUEVA FUNCIÓN
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

        tiempoRestante = 30f;
        tiempoTerminado = false;
        ActualizarUI();
    }
    private void ReiniciarSecuencia() {
        secuenciaJugador.Clear(); // NO limpiamos secuenciaObjetivo (permanece igual)
    }
    private IEnumerator DecidirSiguienteFase() {

        yield return new WaitForSeconds(1.5f);
        // 🆕 Boss >50% vida → Defensa
        // Boss ≤50% → Fury Mode + Lulu poción
        if (combatManager.vidaBossActual > combatManager.vidaBossMax * 0.5f) {
            Debug.Log("🛡️ Boss fuerte → Nivel1-Defensa");
            SceneManager.LoadScene("Nivel1-Defensa");
        }
        else {
            Debug.Log("🔥 Boss débil → Nivel1-Fury (Lulu poción)");
            SceneManager.LoadScene("Nivel1-Fury");
        }
    }
}