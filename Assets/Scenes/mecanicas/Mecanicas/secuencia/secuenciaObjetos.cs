using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SecuenciaObjetos : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI secuenciaText;
    [SerializeField] private TextMeshProUGUI progresoText;
    [SerializeField] private TextMeshProUGUI vidasText;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoOK;
    [SerializeField] private AudioClip sonidoError;

    [Header("Config")]
    [SerializeField] private float tiempoTotal = 45f;
    [SerializeField] private int vidasMax = 3;
    private float tiempoRestante;
    private int vidasActuales;

    private List<string> todasNotas = new List<string> { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    private List<string> secuenciaActual = new List<string>();
    private List<string> inputJugador = new List<string>();

    /*
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        tiempoRestante = tiempoTotal;
        vidasActuales = vidasMax;
        GenerarNuevaSecuencia();
        ActualizarUI();
    }

    void Update()
    {
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0) FinSecuencia("⏰ Tiempo agotado");
        ActualizarUI();
    }

    public void RegistrarNotaJugador(string nota)
    {
        inputJugador.Add(nota);
        Debug.Log($"🎹 [{string.Join("→", inputJugador)}] vs [{string.Join("→", secuenciaActual)}]");

        // Reproducir nota
        if (audioSource) audioSource.PlayOneShot(sonidoOK);

        // Verificar paso actual
        if (inputJugador.Count <= secuenciaActual.Count)
        {

            if (inputJugador[^1] == secuenciaActual[inputJugador.Count - 1])
            {
                Debug.Log("✅ Paso correcto!");
                if (inputJugador.Count == secuenciaActual.Count)
                {
                    Debug.Log("🏆 SECUENCIA COMPLETA!");
                }
            }
            else
            {
                Debug.Log("❌ Paso incorrecto!");
                PerderVida();
            }
        }
        ActualizarUI();
    }

    private void GenerarNuevaSecuencia()
    {
        secuenciaActual.Clear();
        int longitud = faseActual + 1; // Fase1=2, Fase2=3, Fase3=4

        for (int i = 0; i < longitud; i++)
        {

            int index = Random.Range(0, todasNotas.Count);
            secuenciaActual.Add(todasNotas[index]);
        }
        inputJugador.Clear();
        secuenciaText.text = $"<color=yellow>FASE {faseActual}: {string.Join(" → ", secuenciaActual)}</color>";
        Debug.Log($"🎯 Nueva secuencia FASE {faseActual}: {string.Join("→", secuenciaActual)}");
    }*/
}
