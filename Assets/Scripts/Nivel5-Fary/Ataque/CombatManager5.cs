using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager5 : MonoBehaviour {

    [Header("Vida")]
    [SerializeField] private Slider vidaJugador;
    [SerializeField] private Slider vidaBoss;
    [SerializeField] private TextMeshProUGUI textoVidaJugador;
    [SerializeField] private TextMeshProUGUI textoVidaBoss;

    [Header("Referencias")]
    [SerializeField] private AttackManagerSecuencia5 attackManager;

    // Estado combate
    public float vidaJugadorMax = 100f;
    public float vidaBossMax = 100f;
    public float vidaJugadorActual = 100f;
    public float vidaBossActual = 100f;

    void Start() {
        CargarEstado();
        InicializarUI();
        // attackManager ya está asignado en Inspector
    }
    private void CargarEstado() {
        if (PlayerPrefs.HasKey("VidaJugador")) {
            vidaJugadorActual = PlayerPrefs.GetFloat("VidaJugador", vidaJugadorMax);
            vidaBossActual = PlayerPrefs.GetFloat("VidaBoss", vidaBossMax);
            Debug.Log($"⚔️ CombatManager: Vida cargada J:{vidaJugadorActual} B:{vidaBossActual}");
        }
    }
    public void RecibirAtaque(int damageTotal) {

        vidaBossActual -= damageTotal*12;
        vidaBossActual = Mathf.Max(0, vidaBossActual); // para que no baje de 0
        Debug.Log($"⚔️ Daño aplicado: {damageTotal*12:F1}");
        PlayerPrefs.SetFloat("VidaBoss", vidaBossActual);
        ActualizarUI();

        if (vidaBossActual <= 0)  {
            PlayerPrefs.SetInt("Nivel", 1);
            Debug.Log("🎉 ¡BOSS DERROTADO!");
        }
        if (vidaBossActual <= 49) {
            PlayerPrefs.SetInt("Fury", 1);
        }
        if (vidaJugadorActual <= 0) {
            PlayerPrefs.SetInt("Fallo", 1);
        }
        StartCoroutine(Volver());
    }
    private void InicializarUI() {

        vidaJugador.maxValue = vidaJugadorMax;
        vidaJugador.value = vidaJugadorActual;
        vidaBoss.maxValue = vidaBossMax;
        vidaBoss.value = vidaBossActual;
        ActualizarUI();
    }
    private void ActualizarUI()  {
        vidaJugador.value = vidaJugadorActual;
        vidaBoss.value = vidaBossActual;
        textoVidaJugador.text = $"Jugador: {vidaJugadorActual:F0}/{vidaJugadorMax}";
        textoVidaBoss.text = $"Boss: {vidaBossActual:F0}/{vidaBossMax}";
    }
    IEnumerator Volver() {
        PlayerPrefs.Save();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Nivel5");
    }
}