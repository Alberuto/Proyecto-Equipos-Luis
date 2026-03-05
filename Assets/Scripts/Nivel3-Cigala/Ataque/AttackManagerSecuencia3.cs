using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;
public class AttackManagerSecuencia3 : MonoBehaviour {

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

    // 🎵 SECUENCIA
    private List<string> todasNotas = new List<string> { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    private List<string> secuenciaActual = new List<string>();
    private List<string> inputJugador = new List<string>();
    private int faseActual = 1; // 1=2teclas, 2=3teclas, 3=4teclas

    void Start() {
        audioSource = GetComponent<AudioSource>();
        tiempoRestante = tiempoTotal;
        vidasActuales = vidasMax;
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
    // Método auxiliar para convertir string de attack selector en List<string> para comparación
    private List<string> ConvertirSecuenciaStringALista(string secuencia)
    {

        List<string> listaNotas = new List<string>();
        string notaActual = "";

        for (int i = 0; i < secuencia.Length; i++)
        {

            char c = secuencia[i];

            if (c == '#')
            {
                notaActual += c;
            }
            else
            {
                // Si hay nota anterior, la guardamos
                if (!string.IsNullOrEmpty(notaActual))
                {

                    listaNotas.Add(notaActual);
                }
                notaActual = c.ToString();
            }
        }
        // Guardar última nota
        if (!string.IsNullOrEmpty(notaActual))
        {

            listaNotas.Add(notaActual);
        }
        return listaNotas;
    }
    public void IniciarAtaque(string secuencia, int damageBase) {
        secuenciaActual.Clear();

        secuenciaActual = ConvertirSecuenciaStringALista(secuencia);


        inputJugador.Clear();
        secuenciaText.text = $"<color=yellow>FASE {faseActual}: {string.Join(" → ", secuenciaActual)}</color>";
        Debug.Log($"🎯 Nueva secuencia FASE {faseActual}: {string.Join("→", secuenciaActual)}");
    }
    private void SiguienteFase() {

        // 🏆 NIVEL COMPLETO
        PlayerPrefs.DeleteKey("FuryTutorialCompletado");
        PlayerPrefs.DeleteKey("FuryTutorialFallado");
        PlayerPrefs.SetInt("Nivel0bCompletado", 1);
        PlayerPrefs.Save();
        FinSecuencia("🎉 ¡COMBO PERFECTO! Nivel 1 desbloqueado");
        StartCoroutine(VolverConVictoria());
    }
    private void PerderVida() {
        vidasActuales--;
        PlayerTake player = FindObjectOfType<PlayerTake>();
        player.fallo = true;
        inputJugador.Clear();
        if (audioSource && sonidoError) audioSource.PlayOneShot(sonidoError);

        if (vidasActuales <= 0) {
            PlayerPrefs.DeleteKey("FuryTutorialCompletado");
            PlayerPrefs.SetInt("FuryTutorialFallado", 1);
            PlayerPrefs.Save();
            FinSecuencia("💀 Sin vidas");
            StartCoroutine(volverNivel0());
        }
    }
    private void ActualizarUI() {
        progresoText.text = $"Progreso: {inputJugador.Count}/{secuenciaActual.Count}";
        vidasText.text = $"<color=red>♥ {vidasActuales}</color>";
        if (tiempoText != null) {
            tiempoText.text = $"Tiempo: {tiempoRestante:F1}s";
        }
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