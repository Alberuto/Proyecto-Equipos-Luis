using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {

    [Header("Vida")]
    [SerializeField] private Slider vidaJugador;
    [SerializeField] private Slider vidaBoss;
    [SerializeField] private TextMeshProUGUI textoVidaJugador;
    [SerializeField] private TextMeshProUGUI textoVidaBoss;

    [Header("Referencias")]
    [SerializeField] private AttackManager attackManager;

    // Estado combate
    private float vidaJugadorMax = 100f;
    private float vidaBossMax = 500f;
    private float vidaJugadorActual = 100f;
    private float vidaBossActual = 500f;
    private float danoPorTurnoMax = 25f; // Límite daño por turno

    void Start() {
        InicializarUI();
        // attackManager ya está asignado en Inspector
    }
    public void RecibirAtaque(int multiplicador) {

        // Calcula daño: base 50 * multiplicador, limitado por turno
        float danoBase = 50f;
        float danoTotal = danoBase * multiplicador;
        float danoAplicado = Mathf.Min(danoTotal, vidaBossActual, danoPorTurnoMax);

        vidaBossActual -= danoAplicado;
        vidaBossActual = Mathf.Max(0, vidaBossActual);

        Debug.Log($"⚔️ Daño aplicado: {danoAplicado:F1} (x{multiplicador})");
        ActualizarUI();

        if (vidaBossActual <= 0)
        {
            Debug.Log("🎉 ¡BOSS DERROTADO!");
        }
    }

    public void FaseAtaqueTerminada(int ataquesExitosos) {

        Debug.Log($"🏆 Turno terminado. Ataques: {ataquesExitosos}");

        // Fase del boss (daño al jugador)
        AtacarJugador();

        // Reset para siguiente turno
        ReiniciarTurno();
    }
    private void AtacarJugador() {

        float danoBoss = 25f; // Daño fijo del boss
        vidaJugadorActual -= danoBoss;
        vidaJugadorActual = Mathf.Max(0, vidaJugadorActual);

        Debug.Log($"👹 Boss ataca: {danoBoss} daño");
        ActualizarUI();

        if (vidaJugadorActual <= 0)
        {
            Debug.Log("💀 ¡GAME OVER!");
        }
    }

    private void ReiniciarTurno() {

        // Reset AttackManager para nuevo turno
        attackManager.GetComponent<AttackManager>().IniciarCronometro();
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