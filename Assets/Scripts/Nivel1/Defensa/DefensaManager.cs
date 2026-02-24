using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DefensaManager : MonoBehaviour {

    [Header("Resumen Ataque")]
    [SerializeField] private GameObject canvasResumen;
    [SerializeField] private TextMeshProUGUI textoCombo, textoDamage, textoBossVida, textoJugadorVida;

    [Header("Sliders Vida UI")]
    [SerializeField] private Slider sliderVidaJugador;
    [SerializeField] private Slider sliderVidaBoss;
    [SerializeField] private TextMeshProUGUI textoVidaBossDefensa;
    [SerializeField] private TextMeshProUGUI textoVidaJugadorDefensa;

    [Header("Tiempo de Defensa")]
    [SerializeField] private float tiempoDefensaKiko = 25f;

    [Header("Cronómetro Defensa")]
    [SerializeField] private TextMeshProUGUI cronometroDefensa;

    private bool defensaIniciada = false;
    private float tiempoDefensaRestante;
    private bool cronometroActivo = false;
    public enum TipoCombo
    {
        Ninguno = 0,
        ImpactoInicial = 1,
        Imparable = 2,
        Demoledor = 3,
        Brutal = 4,
        RitmoArdiente = 5,
        Aplastante = 6,
        PoderDesatado = 7,
        Legendario = 8,
        EjecucionPerfecta = 9,
        MasAllaInfinito = 10,
        LimiteRoto = 11,
        DodecafonismoSupremo = 12
    }
    private TipoCombo ObtenerTipoComboPorMultiplicador(int multiplicador) {

        return multiplicador switch {
            1 => TipoCombo.ImpactoInicial,
            2 => TipoCombo.Imparable,
            3 => TipoCombo.Demoledor,
            4 => TipoCombo.Brutal,
            5 => TipoCombo.RitmoArdiente,
            6 => TipoCombo.Aplastante,
            7 => TipoCombo.PoderDesatado,
            8 => TipoCombo.Legendario,
            9 => TipoCombo.EjecucionPerfecta,
            10 => TipoCombo.MasAllaInfinito,
            11 => TipoCombo.LimiteRoto,
            12 => TipoCombo.DodecafonismoSupremo,
            _ => TipoCombo.Ninguno
        };
    }
    void Start() {
        int combos = PlayerPrefs.GetInt("Nivel1IntentosExitosos", 0);
        int damage = PlayerPrefs.GetInt("Nivel1AtaqueDaño", 0);
        float vidaJugador = PlayerPrefs.GetFloat("Nivel1VidaJugador", 100f);
        float vidaBoss = PlayerPrefs.GetFloat("Nivel1VidaBoss", 100f);
        Debug.Log($"📊 Resumen Ataque: x{combos} combos, {damage} daño");
        MostrarResumen(combos, damage,vidaJugador,vidaBoss);
    }
    void Update() {
        // 🆕 Solo cuenta SI defensa iniciada Y cronómetro activo
        if (defensaIniciada && cronometroActivo && tiempoDefensaRestante > 0) {
            tiempoDefensaRestante -= Time.deltaTime;
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
        TipoCombo tipoCombo = ObtenerTipoComboPorMultiplicador(combos);
        textoCombo.text = $"Combo x{combos} : {tipoCombo}";
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
        StartCoroutine(AutoCerrar(5f));
    }
    IEnumerator AutoCerrar(float segundos) {
        yield return new WaitForSeconds(segundos);
        canvasResumen.SetActive(false);

        tiempoDefensaRestante = tiempoDefensaKiko;      // ← 1. Reset 25s
        cronometroActivo = true;                        // ← 2. Activar Update()
        cronometroDefensa.gameObject.SetActive(true);
        // 🆕 MOSTRAR textos DEFENSA
        if (textoVidaBossDefensa != null) {
            textoVidaBossDefensa.text = $"Boss: {PlayerPrefs.GetFloat("Nivel1VidaBoss", 100f):F0}/100";
        }
        if (textoVidaJugadorDefensa != null) {
            textoVidaJugadorDefensa.text = $"Jugador: {PlayerPrefs.GetFloat("Nivel1VidaJugador", 100f):F0}/100";
        }
        Debug.Log("🛡️ FASE DEFENSA INICIADA");
        KikoDefenseMusic musicKiko = FindObjectOfType<KikoDefenseMusic>();
        if (musicKiko != null) {
            musicKiko.ReproducirMusicaKiko();
        }
        // Aquí: discos/monedas/torito del compañero
        Debug.Log($"🛡️ FASE DEFENSA INICIADA - {tiempoDefensaKiko}s Kiko");
        defensaIniciada = true;

        // 🆕 TIMER 25s → NUEVO ATAQUE
        yield return new WaitForSeconds(tiempoDefensaKiko);
        FinalizarDefensa();
    }
    private void FinalizarDefensa() {

        Debug.Log("🏁 DEFENSA FINALIZADA → NUEVO ATAQUE");
        // 🆕 Siguiente trozo Kiko
        KikoDefenseMusic musicKiko = FindObjectOfType<KikoDefenseMusic>();
        if (musicKiko != null) {
            musicKiko.SiguienteTurnoKiko();
        }
        SceneManager.LoadScene("Nivel1-Ataque");
    }
}