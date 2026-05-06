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
    [SerializeField] private AttackManagerSecuencia5 attackManager;         //asignado en Inspector

    // Estado combate
    public float vidaJugadorMax = 100f;
    public float vidaBossMax = 100f;
    public float vidaJugadorActual = 100f;
    public float vidaBossActual = 100f;

    void Start() {
        CargarEstado();
        InicializarUI();
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
        PlayerPrefs.Save();
        ActualizarUI();
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

        /*vidaJugador.value = vidaJugadorActual;
        vidaBoss.value = vidaBossActual;
        textoVidaJugador.text = $"Jugador: {vidaJugadorActual:F0}/{vidaJugadorMax}";
        textoVidaBoss.text = $"Boss: {vidaBossActual:F0}/{vidaBossMax}";*/
        textoVidaJugador.text = $"Jugador: {PlayerPrefs.GetFloat("VidaJugador", 100f):F0}/100";//try
        vidaJugador.value = PlayerPrefs.GetFloat("VidaJugador", 100f);
        textoVidaBoss.text = $"Boss: {PlayerPrefs.GetFloat("VidaBoss", 100f):F0}/100";
        vidaBoss.value = PlayerPrefs.GetFloat("VidaBoss", 100f);
    }
    IEnumerator Volver() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Nivel5");
    }
}