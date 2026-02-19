using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackManagerTutorial : MonoBehaviour {

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI tiempoText;
    [SerializeField] private TextMeshProUGUI notaObjetivoText;
    [SerializeField] private TextMeshProUGUI nombreJugadorText;
    [SerializeField] private TextMeshProUGUI vidasText;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoOK;
    [SerializeField] private AudioClip sonidoError;

    [Header("Config")]
    [SerializeField] private float duracionTutorial = 300f;
    [SerializeField] private int vidasMax = 5;

    // Notas disponibles
    private readonly List<string> notas = new List<string> {

        "C", "C#", "D", "D#", "E", "F",
        "F#", "G", "G#", "A", "A#", "B"

    };

    private string notaObjetivoActual;
    private float tiempoRestante;
    private int vidasActuales;
    private bool tutorialActivo = false;

    void Start() {

        // Aquí puedes poner el nombre real
        nombreJugadorText.text = "Jugador";
        tiempoRestante = duracionTutorial;
        vidasActuales = vidasMax;
        tutorialActivo = true;
        ElegirNuevaNotaObjetivo();
        ActualizarUI();
        Debug.Log("🚀 TUTORIAL INICIADO!");
    }
    void Update() {

        if (!tutorialActivo) return;

        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0f) {
            tiempoRestante = 0f;
            FinTutorial("⏰ Tiempo agotado");
        }
        ActualizarUI();
    }
    private void ElegirNuevaNotaObjetivo() {

        int index = Random.Range(0, notas.Count);
        notaObjetivoActual = notas[index];
        notaObjetivoText.text = $"Toca: {notaObjetivoActual}";
        Debug.Log($"🎯 Nueva nota objetivo: {notaObjetivoActual}");
        if (notas.Count == 0) {
            notaObjetivoText.text = "Tutorial completado";
            FinTutorial("🎉 ¡PERFECTO! Todas las notas acertadas");
            return;
        }
    }
    private System.Collections.IEnumerator VolverConFury() {
        yield return new WaitForSeconds(2f); // Pausa victoria
        Debug.Log("🎸 Cargando Nivel0 + CanvasFury...");
        SceneManager.LoadScene("Nivel0");
        // CanvasFury se activa desde Nivel0 (ver abajo)
    }
    private void ActualizarUI() {
        tiempoText.text = $"Tiempo: {tiempoRestante:F1}s";
        vidasText.text = $"Vidas: {vidasActuales}/{vidasMax}";
    }
    private void FinTutorial(string motivo) {
        tutorialActivo = false;
        Debug.Log($"🏁 Fin del tutorial: {motivo}");
        // Aquí más adelante: cargar otra escena, mostrar panel, etc.
    }
    // LLAMADO POR TeclaPianoTutorial
    public void RegistrarNotaJugador(string nota) {

        Debug.Log($"🎯 '{nota}' vs '{notaObjetivoActual}' | Vidas: {vidasActuales}");
        if (!tutorialActivo) return;
        Debug.Log($"🎹 Jugador tocó: {nota} | Objetivo: {notaObjetivoActual}");

     
        if (nota == notaObjetivoActual) {  // Acierto
            if (notas.Count == 1) {        // Esta es la ÚLTIMA nota
                FinTutorial("🎉 ¡PERFECTO! Tutorial completado"); 
                PlayerPrefs.SetInt("FuryTutorialCompletado", 1); // GUARDAR COMPLETADO
                PlayerPrefs.SetInt("FuryTutorialFallado", 0);
                PlayerPrefs.Save();
                StartCoroutine(VolverConFury());
                return;
            }
            if (sonidoOK != null && audioSource != null)
                audioSource.PlayOneShot(sonidoOK);
            Debug.Log("✅ Nota correcta");
            notas.Remove(notaObjetivoActual); // Evitar repetir la misma nota
            ElegirNuevaNotaObjetivo();
        }
        else {
            // Fallo
            if (sonidoError != null && audioSource != null)
                audioSource.PlayOneShot(sonidoError);

            vidasActuales--;
            Debug.Log($"❌ Nota incorrecta. Vidas restantes: {vidasActuales}");

            if (vidasActuales <= 0) { // GUARDAR FALLADO
                PlayerPrefs.SetInt("FuryTutorialFallado", 1);
                PlayerPrefs.SetInt("FuryTutorialCompletado", 0);
                PlayerPrefs.Save();
                FinTutorial("💀 Sin vidas");
                StartCoroutine(VolverConFury());
            }
        }
        ActualizarUI();
    }
}