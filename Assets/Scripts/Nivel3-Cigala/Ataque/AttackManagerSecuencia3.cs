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

    private List<string> secuenciaActual = new List<string>();
    private List<string> inputJugador = new List<string>();
    private int faseActual = 1; // 1=2teclas, 2=3teclas, 3=4teclas

    [SerializeField] private CombatManager3 combatManager;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        tiempoRestante = tiempoTotal;
        vidasActuales = vidasMax;
        ActualizarUI();
    }
    void Update() {
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0) PerderVida(); // ("⏰ Tiempo agotado");
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
                    FinalizarAtaque();
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
    private List<string> ConvertirSecuenciaStringALista(string secuencia) {
        List<string> listaNotas = new List<string>();
        string notaActual = "";
        for (int i = 0; i < secuencia.Length; i++) {
            char c = secuencia[i];
            if (c == '#')  {
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
    public void IniciarAtaque(string secuencia, int damageBase) {
        secuenciaActual.Clear();
        secuenciaActual = ConvertirSecuenciaStringALista(secuencia);
        inputJugador.Clear();
        secuenciaText.text = $"<color=yellow>FASE {faseActual}: {string.Join(" → ", secuenciaActual)}</color>";
        Debug.Log($"🎯 Nueva secuencia FASE {faseActual}: {string.Join("→", secuenciaActual)}");
    }
    private void PerderVida() {
        vidasActuales--;
        PlayerTake player = FindObjectOfType<PlayerTake>();
        player.fallo = true;
        inputJugador.Clear();
        if (audioSource && sonidoError) 
            audioSource.PlayOneShot(sonidoError);
        if (vidasActuales <= 0) {
            PlayerPrefs.SetInt("Fallo", 1);
            PlayerPrefs.Save();
            FinalizarAtaque();
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
        // 🆕 LEER PlayerPrefs en lugar de CombatManager
        float vidaBossPersistente = PlayerPrefs.GetFloat("Nivel1VidaBoss", 100f);
        float vidaBossMax = 100f;
        Debug.Log($"🎯 Vida Boss PlayerPrefs: {vidaBossPersistente}/{vidaBossMax}");
        if (vidaBossPersistente <= 0) {
            Debug.Log("🎉 ¡BOSS DERROTADO! NIVEL 2");
            SceneManager.LoadScene("Nivel3");
        }
        else if (vidaBossPersistente > 50f) {
            Debug.Log("🛡️ Boss fuerte → Nivel3-Defensa");
            SceneManager.LoadScene("Nivel3-Defensa");
        }
        else if (vidaBossPersistente <= 50f && (PlayerPrefs.GetInt("Fury", 0) == 0)) {
            PlayerPrefs.SetInt("Fury", 1);
            SceneManager.LoadScene("Nivel3");
        }
        else {
            SceneManager.LoadScene("Nivel3-Fury");
        }
    }
    private void FinalizarAtaque() {
        int attackValue = PlayerPrefs.GetInt("AttackValue", 0); // 🆕 Leer valor base del ataque desde PlayerPrefs
        int danoFinal = attackValue * 12; //heredar el valor del ataque base del attack selector y multiplicar por 12 combos

        Debug.Log($"⏰ Tiempo terminado! Ataques exitosos: secuencias → {danoFinal} daño");
        if (combatManager != null) {
            Debug.Log("⚔️ CombatManager OK → RecibirAtaque()");
            combatManager.RecibirAtaque(danoFinal);
        }
        else {
            Debug.LogError("❌ CombatManager NO ASIGNADO en AttackManager!");
        }
        PlayerPrefs.SetInt("AtaqueDaño", danoFinal);
        PlayerPrefs.SetFloat("VidaJugador", combatManager.vidaJugadorActual);
        PlayerPrefs.SetFloat("VidaBoss", combatManager.vidaBossActual);
        PlayerPrefs.Save();
        StartCoroutine(DecidirSiguienteFase());
    }
}