using System.Collections;
using TMPro;
using UnityEngine;

public class DefensaManager : MonoBehaviour {

    [SerializeField] private GameObject canvasResumen;
    [SerializeField] private TextMeshProUGUI textoCombo, textoDamage, textoBossVida, textoJugadorVida;
    [SerializeField] private CombatManager combatManager;
    public enum TipoCombo {
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
        DominioSupremo = 12
    }
    void Start() {
        int combos = PlayerPrefs.GetInt("Nivel1IntentosExitosos", 0);
        int damage = PlayerPrefs.GetInt("Nivel1AtaqueDaño", 0);
        Debug.Log($"📊 Resumen Ataque: x{combos} combos, {damage} daño");
        MostrarResumen(combos, damage);
    }
    void MostrarResumen(int combos, int damage) {
        canvasResumen.SetActive(true);
        TipoCombo tipoCombo = ObtenerTipoComboPorMultiplicador(combos);
        textoCombo.text = $"Combo: x{combos} es {tipoCombo}";
        textoDamage.text = $"Daño: {damage}";
        textoBossVida.text = $"Boss: {combatManager.vidaBossActual:F0}/{combatManager.vidaBossMax}";
        StartCoroutine(AutoCerrar(3f));
    }
    IEnumerator AutoCerrar(float segundos) {
        yield return new WaitForSeconds(segundos);
        canvasResumen.SetActive(false);
        Debug.Log("🛡️ FASE DEFENSA INICIADA");
        // Aquí: discos/monedas/torito del compañero
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
            12 => TipoCombo.DominioSupremo,
            _ => TipoCombo.Ninguno
        };
    }
}