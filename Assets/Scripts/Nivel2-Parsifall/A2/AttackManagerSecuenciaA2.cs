using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;
public class AttackManagerSecuenciaA2 : MonoBehaviour {

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI secuenciaText;
    [SerializeField] private TextMeshProUGUI progresoText;
    [SerializeField] private TextMeshProUGUI vidasText;
    [SerializeField] private TextMeshProUGUI tiempoText;

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

    [Header("Flash")]
    [SerializeField] private FlashEffect flashEffect;

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
                if (inputJugador.Count == secuenciaActual.Count)
                {
                    Debug.Log("🏆 SECUENCIA COMPLETA!");
                    if (flashEffect != null)
                        flashEffect.FlashCombo(6);
                    if (audioSource && sonidoCombo) audioSource.PlayOneShot(sonidoCombo);
                    SiguienteFase();
                }
                else {
                    if (flashEffect != null)
                        flashEffect.FlashCombo(1);
                }
            }
            else {
                Debug.Log("❌ Paso incorrecto!");
                if (flashEffect != null)
                    flashEffect.FlashCombo(12);
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
            PlayerPrefs.SetInt("Nivel", 1); //no se si esto hace que el nivel no se guarde y por eso se borra el progreso
            PlayerPrefs.Save();
            FinSecuencia("🎉 ¡COMBO PERFECTO! Nivel 1 desbloqueado");
            StartCoroutine(Volver());
        }
    }
    private void PerderVida() {
        vidasActuales--;
        PlayerTake player = FindObjectOfType<PlayerTake>();
        player.fallo = true;
        inputJugador.Clear();
        if (audioSource && sonidoError) audioSource.PlayOneShot(sonidoError);

        if (vidasActuales <= 0) {
            PlayerPrefs.SetInt("Fallo", 1);
            PlayerPrefs.Save();
            FinSecuencia("💀 Sin vidas");
            StartCoroutine(Volver());
        }
        else {
            GenerarNuevaSecuencia();
        }
    }
    private void ActualizarUI() {
        progresoText.text = $"Progreso: {inputJugador.Count}/{secuenciaActual.Count}";
        vidasText.text = $"<color=red>♥ Vidas Jugador: {vidasActuales}</color>";
        if (tiempoText != null) {
            tiempoText.text = $"Tiempo: {tiempoRestante:F1}s";
        }
    }
    private void FinSecuencia(string motivo) {
        secuenciaText.text = $"<color=red>{motivo}</color>";
    }
    private IEnumerator Volver() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Nivel2");
    }
}