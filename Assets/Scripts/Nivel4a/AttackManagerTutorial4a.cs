using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackManagerTutorial4a : MonoBehaviour {

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI tiempoText;
    [SerializeField] private TextMeshProUGUI notaObjetivoText;
    [SerializeField] private TextMeshProUGUI nombreJugadorText;
    [SerializeField] private TextMeshProUGUI vidasText;

    [Header("FASE 1 - LÁMPARA")]
    [SerializeField] private SecuenciaManager4a secuenciaManager;
    [SerializeField] private int aciertosNecesariosFase1 = 3;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoOK;
    [SerializeField] private AudioClip sonidoError;

    [Header("Config")]
    [SerializeField] private float duracionTutorial = 300f;
    [SerializeField] private int vidasMax = 5;

    private readonly List<string> notas = new List<string> {
        "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"
    };

    private string notaObjetivoActual;
    private float tiempoRestante;
    private int vidasActuales;
    private int aciertosConsecutivos = 0;
    private bool faseLamparaActiva = false;
    private bool tutorialActivo = false;

    void Start() {

        nombreJugadorText.text = "Jugador";
        tiempoRestante = duracionTutorial;
        vidasActuales = vidasMax;
        tutorialActivo = true;

        if (secuenciaManager == null)
            secuenciaManager = FindObjectOfType<SecuenciaManager4a>();

        ElegirNuevaNotaObjetivo();
        ActualizarUI();
        Debug.Log("🚀 FASE 1: LÁMPARA + TECLAS INICIADA!");
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

    // 🆕 LÁMPARA ilumina nota → AttackManager confirma tecla
    public void NotificarNotaIluminada(string notaIluminada) {
        if (!faseLamparaActiva || !tutorialActivo) return;

        notaObjetivoText.text = $"Ilumina: {notaIluminada}\nPulsa: {notaIluminada}";
        notaObjetivoActual = notaIluminada;
        Debug.Log($"🔦 Nota iluminada: {notaIluminada}");
    }
    public void RegistrarNotaJugador(string nota) {
        if (!tutorialActivo) return;

        Debug.Log($"🎹 '{nota}' vs '{notaObjetivoActual}'");

        if (faseLamparaActiva) {
            // FASE 1: Lámpara + Teclas
            if (nota == notaObjetivoActual) {
                aciertosConsecutivos++;
                audioSource.PlayOneShot(sonidoOK);
                Debug.Log($"✅ Acierto {aciertosConsecutivos}/{aciertosNecesariosFase1}");

                if (aciertosConsecutivos >= aciertosNecesariosFase1) {
                    FinFase1();
                    return;
                }
                ElegirNuevaNotaObjetivo();
            }
            else {
                aciertosConsecutivos = 0;
                vidasActuales--;
                audioSource.PlayOneShot(sonidoError);
                Debug.Log($"❌ Error. Vidas: {vidasActuales}");

                if (vidasActuales <= 0) {
                    FinTutorial("💀 Sin vidas");
                    return;
                }
                ElegirNuevaNotaObjetivo();
            }
        }
        ActualizarUI();
    }
    void ElegirNuevaNotaObjetivo() {
        int index = Random.Range(0, notas.Count);
        notaObjetivoActual = notas[index];
        notaObjetivoText.text = $"Toca: {notaObjetivoActual}";
    }
    void FinFase1() {
        tutorialActivo = false;
        Debug.Log("🎉 FASE 1 COMPLETADA! 🔦→🎹");
        notaObjetivoText.text = "¡FASE 1 LISTA!\nPróxima: Secuencia lámpara";
        // Aquí PASARÍAS a Fase 2 (SecuenciaManager.fase2_SecuenciaLampara = true)
    }
    void FinTutorial(string motivo) {
        tutorialActivo = false;
        Debug.Log($"🏁 Fin tutorial: {motivo}");
    }
    private void ActualizarUI() {
        tiempoText.text = $"Tiempo: {tiempoRestante:F1}s";
        vidasText.text = $"Vidas: {vidasActuales}/{vidasMax}";
        if (faseLamparaActiva) {
            notaObjetivoText.text = $"Ilumina: {notaObjetivoActual}";
        }
    }
    private System.Collections.IEnumerator VolverConFury() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Nivel0");
    }
    public void ActivarFaseLampara() {
        faseLamparaActiva = true;
        notaObjetivoText.text = "🔦 FASE LÁMPARA ACTIVA\nIlumina una nota";
        Debug.Log("🎉 FASE LÁMPARA ACTIVADA!");
    }
}