using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;
public class AttackManagerSecuencia : MonoBehaviour {

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI secuenciaText;
    [SerializeField] private TextMeshProUGUI progresoText;
    [SerializeField] private TextMeshProUGUI vidasText;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoOK;
    [SerializeField] private AudioClip sonidoError;
    [SerializeField] private AudioClip sonidoCombo;

    [Header("Config")]
    [SerializeField] private float tiempoTotal = 45f;
    [SerializeField] private int vidasMax = 3;
    private float tiempoRestante;
    private int vidasActuales;

    // 🎵 SECUENCIA
    private List<string> todasNotas = new List<string> { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    private List<string> secuenciaActual = new List<string>();
    private List<string> inputJugador = new List<string>();
    private int faseActual = 1; // 1=2teclas, 2=3teclas, 3=4teclas

    void Start() {
        audioSource = GetComponent<AudioSource>();
        tiempoRestante = tiempoTotal;
        vidasActuales = vidasMax;
        GenerarNuevaSecuencia();
        ActualizarUI();
    }
    void Update() {
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0) FinSecuencia("⏰ Tiempo agotado");
        ActualizarUI();
    }
    public void RegistrarNotaJugador(string nota) {
        inputJugador.Add(nota);
        Debug.Log($"🎹 [{string.Join("→", inputJugador)}] vs [{string.Join("→", secuenciaActual)}]");

        // Reproducir nota
        if (audioSource) audioSource.PlayOneShot(sonidoOK);

        // Verificar paso actual
        if (inputJugador.Count <= secuenciaActual.Count) {

            if (inputJugador[^1] == secuenciaActual[inputJugador.Count - 1]) {
                Debug.Log("✅ Paso correcto!");
                if (inputJugador.Count == secuenciaActual.Count) {
                    Debug.Log("🏆 SECUENCIA COMPLETA!");
                    if (audioSource && sonidoCombo) audioSource.PlayOneShot(sonidoCombo);
                    SiguienteFase();
                }
            }
            else {
                Debug.Log("❌ Paso incorrecto!");
                PerderVida();
            }
        }
        ActualizarUI();
    }
    private void GenerarNuevaSecuencia() {
        secuenciaActual.Clear();
        int longitud = faseActual + 1; // Fase1=2, Fase2=3, Fase3=4

        for (int i = 0; i < longitud; i++) {

            int index = Random.Range(0, todasNotas.Count);
            secuenciaActual.Add(todasNotas[index]);
        }
        inputJugador.Clear();
        secuenciaText.text = $"<color=yellow>FASE {faseActual}: {string.Join(" → ", secuenciaActual)}</color>";
        Debug.Log($"🎯 Nueva secuencia FASE {faseActual}: {string.Join("→", secuenciaActual)}");
    }
    private void SiguienteFase() {

        if (faseActual < 3) {
            faseActual++;
            GenerarNuevaSecuencia();
        }
        else {
            // 🏆 NIVEL COMPLETO
            PlayerPrefs.SetInt("Nivel0bCompletado", 1);
            PlayerPrefs.Save();
            FinSecuencia("🎉 ¡COMBO PERFECTO! Nivel 1 desbloqueado");
            StartCoroutine(VolverConVictoria());
        }
    }
    private void PerderVida() {
        vidasActuales--;
        inputJugador.Clear();
        if (audioSource && sonidoError) audioSource.PlayOneShot(sonidoError);

        if (vidasActuales <= 0) {
            PlayerPrefs.SetInt("Nivel0bFallado", 1);
            PlayerPrefs.Save();
            FinSecuencia("💀 Sin vidas");
            StartCoroutine(volverNivel0());
        }
        else {
            GenerarNuevaSecuencia();
        }
    }
    private void ActualizarUI() {
        progresoText.text = $"Progreso: {inputJugador.Count}/{secuenciaActual.Count}";
        vidasText.text = $"<color=red>♥ {vidasActuales}</color>";
    }
    private void FinSecuencia(string motivo) {
        secuenciaText.text = $"<color=red>{motivo}</color>";
    }
    private IEnumerator VolverConVictoria() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Nivel0");
    }
    private IEnumerator volverNivel0() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Nivel0");
    }
}