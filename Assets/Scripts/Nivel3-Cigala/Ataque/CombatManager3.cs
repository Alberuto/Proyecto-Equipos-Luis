using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager3 : MonoBehaviour {

    [Header("Vida")]
    [SerializeField] private Slider vidaJugador;
    [SerializeField] private Slider vidaBoss;
    [SerializeField] private TextMeshProUGUI textoVidaJugador;
    [SerializeField] private TextMeshProUGUI textoVidaBoss;

    [Header("Referencias")]
    [SerializeField] private AttackManagerSecuencia3 attackManager;

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
        // Si NO hay datos guardados → valores por defecto 100/100 jugador y boss
        if (PlayerPrefs.HasKey("VidaJugador")) {
            vidaJugadorActual = PlayerPrefs.GetFloat("VidaJugador", vidaJugadorMax);
            vidaBossActual = PlayerPrefs.GetFloat("VidaBoss", vidaBossMax);
            Debug.Log($"⚔️ CombatManager: Vida cargada J:{vidaJugadorActual} B:{vidaBossActual}");
        }
    }
    public void RecibirAtaque(int damageTotal) {

        vidaBossActual -= damageTotal;
        vidaBossActual = Mathf.Max(0, vidaBossActual); // para que no baje de 0
        Debug.Log($"⚔️ Daño aplicado: {damageTotal:F1}");
        PlayerPrefs.SetInt("VidaBoss", (int) vidaBossActual);

        ActualizarUI();
        if (vidaBossActual <= 0) {
            Debug.Log("🎉 ¡BOSS DERROTADO!");
        }
        if (vidaJugadorActual <= 0) {
            PlayerPrefs.SetInt("Fallo", 1);
            SceneManager.LoadScene("Nivel3");
        }
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
}