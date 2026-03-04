using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AttackManagerTutorialObjetos2A : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI tiempoText;
    [SerializeField] private TextMeshProUGUI objetivoText;
    [SerializeField] private TextMeshProUGUI nombreJugadorText;
    [SerializeField] private TextMeshProUGUI vidasText;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoOK;
    [SerializeField] private AudioClip sonidoError;

    [Header("Config")]
    [SerializeField] private float duracionTutorial = 120f;
    [SerializeField] private int vidasMax = 3;

    private readonly List<string> secuenciaTutorial = new List<string> {
        "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"
    };

    private int indiceNotaActual = 0;  // ← 0 a 11 (12 objetos)
    private string notaObjetivoActual;
    private float tiempoRestante;
    private int vidasActuales;
    private bool tutorialActivo = false;
    private int objetosCogidosCorrectos = 0;  // ← CONTADOR SEGURO

    void Start() {
        nombreJugadorText.text = "Jugador";
        tiempoRestante = duracionTutorial;
        vidasActuales = vidasMax;
        tutorialActivo = true;
        objetosCogidosCorrectos = 0;
        indiceNotaActual = 0;
        ElegirSiguienteNota();
        ActualizarUI();
        Debug.Log("🚀 TUTORIAL OBJETOS 2A INICIADO! Necesitas 12 objetos");
    }

    void Update() {
        if (!tutorialActivo) return;
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0f)
        {
            tiempoRestante = 0f;
            FinTutorial("⏰ Tiempo agotado");
        }
        ActualizarUI();
    }

    private void ElegirSiguienteNota() {

        if (indiceNotaActual >= secuenciaTutorial.Count) {
            return;  // ← NO completar aquí
        }
        notaObjetivoActual = secuenciaTutorial[indiceNotaActual];
        Debug.Log($"🎯 Objetivo {indiceNotaActual + 1}/12: {notaObjetivoActual}");
    }

    public void RegistrarObjetoCogido(string tagObjeto) {

        if (!tutorialActivo) return;

        Debug.Log($"🎹 '{tagObjeto}' vs '{notaObjetivoActual}' ({indiceNotaActual + 1}/12)");

        if (tagObjeto == notaObjetivoActual) {

            // ✅ OBJETO CORRECTO
            objetosCogidosCorrectos++;  // ← INCREMENTAR CONTADOR
            indiceNotaActual++;         // ← SIGUIENTE POSICIÓN

            if (audioSource && sonidoOK) audioSource.PlayOneShot(sonidoOK);
            Debug.Log($"✅ {tagObjeto} correcto! ({objetosCogidosCorrectos}/12)");

            // 🛑 SÓLO COMPLETAR DESPUÉS DE 12 OBJETOS
            if (objetosCogidosCorrectos >= 12) {
                Debug.Log("🏆 ¡12 OBJETOS CORRECTOS! TUTORIAL COMPLETADO!");
                CompletarTutorial();
                return;
            }
            ElegirSiguienteNota();  // ← Siguiente objetivo
        }
        else
        {
            Debug.Log($"❌ {tagObjeto} ≠ {notaObjetivoActual}");
            PerderVida();
        }
        ActualizarUI();
    }

    private void CompletarTutorial()
    {
        tutorialActivo = false;
        FinTutorial("🎉 ¡TUTORIAL 2A PERFECTO! 12/12 objetos correctos");

        // PlayerPrefs CORRECTOS para CanvasController0 de Nivel2
        PlayerPrefs.SetInt("Nivel0bCompletado", 1);
        PlayerPrefs.SetInt("NivelActual", 2);
        PlayerPrefs.DeleteKey("FuryTutorialFallado");
        PlayerPrefs.Save();

        Debug.Log("✅ Tutorial 2A completado - Nivel0bCompletado=1");
        StartCoroutine(VolverConVictoria());
    }

    private void PerderVida()
    {
        if (audioSource && sonidoError) audioSource.PlayOneShot(sonidoError);
        vidasActuales--;
        objetosCogidosCorrectos = 0;  // ← REINICIAR CONTADOR
        indiceNotaActual = 0;         // ← EMPEZAR DESDE C

        if (vidasActuales <= 0)
        {
            PlayerPrefs.SetInt("FuryTutorialFallado", 1);
            PlayerPrefs.SetInt("Nivel0bCompletado", 0);
            PlayerPrefs.Save();
            FinTutorial("💀 Sin vidas");
            StartCoroutine(VolverConFracaso());
        }
        else
        {
            ElegirSiguienteNota();  // ← Empezar de nuevo desde C
        }
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        if (tiempoText) tiempoText.text = $"Tiempo: {tiempoRestante:F1}s";
        if (vidasText) vidasText.text = $"<color=red>♥ Vidas: {vidasActuales}/{vidasMax}";
        if (nombreJugadorText) nombreJugadorText.text = $"Jugador";

        if (objetivoText)
        {
            objetivoText.text = $"2A Coge: <color=yellow>{notaObjetivoActual}</color> ({objetosCogidosCorrectos}/12)";
        }
    }

    private void FinTutorial(string motivo)
    {
        if (objetivoText) objetivoText.text = $"<color=red>{motivo}</color>";
        tutorialActivo = false;
        Debug.Log($"🏁 Fin tutorial 2A: {motivo}");
    }

    private IEnumerator VolverConVictoria()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Nivel2");  // ← CanvasController0 gestiona
    }

    private IEnumerator VolverConFracaso()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Nivel2");  // ← Reintentar
    }
}
