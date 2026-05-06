using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;
public class AttackManagerSecuencia5 : MonoBehaviour {

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI secuenciaText;
    [SerializeField] private TextMeshProUGUI progresoText;
    [SerializeField] private TextMeshProUGUI vidasText;
    [SerializeField] private TextMeshProUGUI tiempoText;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoError;
    [SerializeField] private AudioClip sonidoCombo;

    [Header("Config")]
    [SerializeField] private float tiempoTotal = 45f;
    [SerializeField] private int vidasMax = 3;
    private float tiempoRestante;
    private int vidasActuales;

    [Header("Flash")]
    [SerializeField] private FlashEffect flashEffect;

    [SerializeField] private CombatManager5 combatManager;

    // 🎵 SECUENCIA    private List<string> todasNotas = new List<string> { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
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
        if (tiempoRestante <= 0)
            StartCoroutine(DecidirSiguienteFase());
        ActualizarUI();
    }
    public void RegistrarNotaJugador(string nota) {
        inputJugador.Add(nota);
        Debug.Log($"🎹 [{string.Join("→", inputJugador)}] vs [{string.Join("→", secuenciaActual)}]");

        // Verificar paso actual
        if (inputJugador.Count <= secuenciaActual.Count) {

            if (inputJugador[^1] == secuenciaActual[inputJugador.Count - 1]) {
                Debug.Log("✅ Paso correcto!");
                if (flashEffect != null) {
                    flashEffect.FlashCombo(1);
                }
                if (inputJugador.Count == secuenciaActual.Count) {
                    Debug.Log("🏆 SECUENCIA COMPLETA!");
                    if (audioSource && sonidoCombo) 
                        audioSource.PlayOneShot(sonidoCombo);
                    if (flashEffect != null) {
                        flashEffect.FlashCombo(6);
                    }
                    combatManager.RecibirAtaque(PlayerPrefs.GetInt("AttackValue"));
                    StartCoroutine(DecidirSiguienteFase());
                }
            }
            else {
                Debug.Log("❌ Paso incorrecto!");
                PerderVida();
            }
        }
        ActualizarUI();
    }
    private List<string> ConvertirSecuenciaStringALista(string secuencia)  {
        List<string> listaNotas = new List<string>();
        string notaActual = "";
        for (int i = 0; i < secuencia.Length; i++)  {
            char c = secuencia[i];
            if (c == '#') {
                notaActual += c;
            }
            else { // Si hay nota anterior, la guardamos
                if (!string.IsNullOrEmpty(notaActual))   {
                    listaNotas.Add(notaActual);
                }
                notaActual = c.ToString();
            }
        }// Guardar última nota
        if (!string.IsNullOrEmpty(notaActual)) {
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
    private void PerderVida() {
        if (flashEffect != null)  {
            flashEffect.FlashCombo(12);
        }
        vidasActuales--;
        PlayerTake player = FindObjectOfType<PlayerTake>();
        player.fallo = true;
        inputJugador.Clear();
        if (audioSource && sonidoError) 
            audioSource.PlayOneShot(sonidoError);
        if (vidasActuales <= 0) {
            StartCoroutine(DecidirSiguienteFase());
        }
    }
    private void ActualizarUI() {
        progresoText.text = $"Progreso: {inputJugador.Count}/{secuenciaActual.Count}";
        vidasText.text = $"<color=red>♥ {vidasActuales}</color>";
        if (tiempoText != null) {
            tiempoText.text = $"Tiempo: {tiempoRestante:F1}s";
        }
    }
    private IEnumerator DecidirSiguienteFase() {
        yield return new WaitForSeconds(1.5f);
        float vidaBossPersistente = PlayerPrefs.GetFloat("VidaBoss", 100f);
        float vidaBossMax = 100;
        Debug.Log($"🎯 Vida Boss PlayerPrefs: vida persiste{vidaBossPersistente}/{vidaBossMax} vida bos max");
        Debug.Log($"🎯 Vida Boss CombatManager: {combatManager.vidaBossActual}/{combatManager.vidaBossMax}");
        Debug.Log($"🎯 Vida Jugador PlayerPrefs: {PlayerPrefs.GetFloat("VidaJugador", 100f)}/100");
        Debug.Log($"🎯 Vida Jugador CombatManager: {combatManager.vidaJugadorActual}/{combatManager.vidaJugadorMax}");
        Debug.Log($"🎯 Fury Status: {PlayerPrefs.GetInt("Fury", 0)} (0=No Fury, 1=Fury Activo)");
        Debug.Log($"🎯 Fallo Status: {PlayerPrefs.GetInt("Fallo", 0)} (0=No Fallo, 1=Jugador Falló)");
        Debug.Log($"🎯 Decisión de siguiente fase basada en vidaBossPersistente: {vidaBossPersistente} y Fury: {PlayerPrefs.GetInt("Fury", 0)}");

        if (vidaBossPersistente <= 0)  {
            Debug.Log("🎉 ¡BOSS DERROTADO! NIVEL 2");
            PlayerPrefs.SetInt("Nivel", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Nivel5");
        }
        else if (vidaBossPersistente > 50f) {
            Debug.Log("🛡️ Boss fuerte → Nivel3-Defensa");
            SceneManager.LoadScene("Nivel5-Defensa");
        }
        else if (vidaBossPersistente <= 50f && (PlayerPrefs.GetInt("Fury", 0) == 0)) {
            PlayerPrefs.SetInt("Fury", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Nivel5");
        }
        else {
            SceneManager.LoadScene("Nivel5-Fury");
        }
    }
}