using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AttackManagerTutorialObjetos2A : MonoBehaviour {

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
        if (tiempoRestante <= 0f) {

            tiempoRestante = 0f;
            VolverConFracaso();
            StartCoroutine(VolverConFracaso());
        }
        ActualizarUI();
    }

    private void ElegirSiguienteNota() {

        if (indiceNotaActual >= secuenciaTutorial.Count) {
            VolverConVictoria();
        }
        notaObjetivoActual = secuenciaTutorial[indiceNotaActual];
        Debug.Log($"🎯 Objetivo {indiceNotaActual + 1}/12: {notaObjetivoActual}");
    }

    public void RegistrarObjetoCogido(string tagObjeto) {
        if (!tutorialActivo)
            return;

        Debug.Log($"🎹 '{tagObjeto}' vs '{notaObjetivoActual}' ({indiceNotaActual + 1}/12)");

        if (tagObjeto == notaObjetivoActual)
        {
            objetosCogidosCorrectos++;
            Debug.Log($"✅ {tagObjeto} correcto! ({objetosCogidosCorrectos}/12)");

            if (audioSource && sonidoOK)
                audioSource.PlayOneShot(sonidoOK);

            if (objetosCogidosCorrectos >= 12)
            {
                Debug.Log("🏆 ¡12 OBJETOS CORRECTOS! TUTORIAL COMPLETADO!");
                VolverConVictoria();
                return;
            }

            indiceNotaActual++;
            ElegirSiguienteNota();
        }
        else
        {
            Debug.Log($"❌ {tagObjeto} ≠ {notaObjetivoActual}");
            PerderVida();
        }

        ActualizarUI();
    }
    private void PerderVida()  {
        if (audioSource && sonidoError) 
            audioSource.PlayOneShot(sonidoError);

        vidasActuales--;

        if (vidasActuales <= 0) {
            VolverConFracaso();
            StartCoroutine(VolverConFracaso());
        }
        else {
            ElegirSiguienteNota();  // ← Empezar de nuevo desde C
        }
        ActualizarUI();
    }
    private void ActualizarUI()  {
        if (tiempoText) tiempoText.text = $"Tiempo: {tiempoRestante:F1}s";
        if (vidasText) vidasText.text = $"<color=red>♥ Vidas: {vidasActuales}/{vidasMax}";
        if (nombreJugadorText) nombreJugadorText.text = $"Jugador";

        if (objetivoText)
        {
            objetivoText.text = $"2A Coge: <color=yellow>{notaObjetivoActual}</color> ({objetosCogidosCorrectos}/12)";
        }
    }
    private IEnumerator VolverConVictoria() {

        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("Nivel0bCompletado", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Nivel2");  // ← CanvasController0 gestiona
    }
    private IEnumerator VolverConFracaso() {

        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("Fallo", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Nivel2");  // ← Reintentar
    }
}