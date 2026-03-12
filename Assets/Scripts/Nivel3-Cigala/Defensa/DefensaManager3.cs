using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class DefensaManager3 : MonoBehaviour {

    [Header("Resumen Ataque")]
    [SerializeField] private GameObject canvasResumen;
    [SerializeField] private TextMeshProUGUI textoCombo, textoDamage, textoBossVida, textoJugadorVida;

    [Header("Sliders Vida UI")]
    [SerializeField] private Slider sliderVidaJugador;
    [SerializeField] private Slider sliderVidaBoss;
    [SerializeField] private TextMeshProUGUI textoVidaBossDefensa;
    [SerializeField] private TextMeshProUGUI textoVidaJugadorDefensa;

    [Header("Tiempo de Defensa")]
    [SerializeField] private float tiempoDefensaBoss = 40f;

    [Header("Cronómetro Defensa")]
    [SerializeField] private TextMeshProUGUI cronometroDefensa;

    private bool defensaIniciada = false;
    private float tiempoDefensaRestante;
    private bool cronometroActivo = false;
    float vidaJugador;
    DefenseMusicUniversal musica;

    void Start() {
        Debug.Log("🔍 [DEFENSA] Start() INICIO");
        musica = FindObjectOfType<DefenseMusicUniversal>();
        int combos = 12; //en los niveles que se introduzca cogiendo o iluminando objetos siempre sera x12
        int damage = PlayerPrefs.GetInt("AtaqueDaño", 0);
        vidaJugador = PlayerPrefs.GetFloat("VidaJugador");
        float vidaBoss = PlayerPrefs.GetFloat("VidaBoss");
        Debug.Log($"📊 Resumen Ataque: x{combos} combos, {damage} daño");
        MostrarResumen(combos, damage, GameManager.instance.vidaPlayer, vidaBoss);
    }
    void Update() {
        // 🆕 Solo cuenta SI defensa iniciada Y cronómetro activo
        if (defensaIniciada && cronometroActivo && tiempoDefensaRestante > 0) {
            tiempoDefensaRestante -= Time.deltaTime;
            textoVidaJugadorDefensa.text = $"Jugador: {GameManager.instance.vidaPlayer:F0}/100";//try
            ActualizarCronometroUI();
        }
    }
    private void ActualizarCronometroUI() {
        if (cronometroDefensa != null) {
            cronometroDefensa.text = $"Defensa: {tiempoDefensaRestante:F1}s";
            cronometroDefensa.gameObject.SetActive(defensaIniciada);  // Oculto en resumen
        }
    }
    void MostrarResumen(int combos, int damage,float vidaJugador, float vidaBoss) {
        canvasResumen.SetActive(true);
        textoCombo.text = $"Combo x{combos}";
        textoDamage.text = $"Daño realizado con tu ultimo ataque: {damage}";
        textoBossVida.text = $"Vida restante del boss: {vidaBoss:F0}/100";
        textoJugadorVida.text = $"Vida restante del jugador: {vidaJugador:F0}/100";
        if (sliderVidaJugador != null) {
            sliderVidaJugador.maxValue = 100f;
            sliderVidaJugador.value = vidaJugador;
        }
        if (sliderVidaBoss != null) {
            sliderVidaBoss.maxValue = 100f;
            sliderVidaBoss.value = vidaBoss;
        }
        StartCoroutine(AutoCerrar(3f));
    }
    IEnumerator AutoCerrar(float segundos) {
        yield return new WaitForSeconds(segundos);
        canvasResumen.SetActive(false);
        tiempoDefensaRestante = tiempoDefensaBoss;      // ← 1. Reset 25s
        cronometroActivo = true;                        // ← 2. Activar Update()
        defensaIniciada = true;                       // ← 3. Marcar inicio defensa
        cronometroDefensa.gameObject.SetActive(true);
        // 🆕 MOSTRAR textos DEFENSA
        if (textoVidaBossDefensa != null) {
            textoVidaBossDefensa.text = $"Boss: {PlayerPrefs.GetFloat("Nivel1VidaBoss", 100f):F0}/100";
        }
        if (textoVidaJugadorDefensa != null) {
            textoVidaJugadorDefensa.text = $"Jugador: {PlayerPrefs.GetFloat("Nivel1VidaJugador", 100f):F0}/100";
        }
        Debug.Log("🛡️ FASE DEFENSA INICIADA");
        if (musica != null) {
            musica.ReproducirMusica();
        }
        // Aquí: discos/monedas/torito del compañero
        Debug.Log($"🛡️ FASE DEFENSA INICIADA - {tiempoDefensaBoss}s Kiko");
        defensaIniciada = true;
        // 🆕 TIMER 25s → NUEVO ATAQUE
        yield return new WaitForSeconds(tiempoDefensaBoss);
        FinalizarDefensa();
    }
    private void FinalizarDefensa() {
        PlayerPrefs.SetFloat("VidaJugador", GameManager.instance.vidaPlayer);
        Debug.Log($"⏰ TIEMPO DEFENSA FINALIZADO - Vida Jugador: {GameManager.instance.vidaPlayer:F0}/100");
        Debug.Log($"\n vidaJugador"+vidaJugador);
        if (vidaJugador <= 0) {
            PlayerPrefs.SetInt("Fallo", 1);
            SceneManager.LoadScene("Nivel3");
        }
        Debug.Log("🏁 DEFENSA FINALIZADA → NUEVO ATAQUE");
        if (musica != null) {
            musica.SiguienteTurno();
        }
        SceneManager.LoadScene("Nivel3-Ataque");
    }
}