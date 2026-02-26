using UnityEngine;
using System.Collections.Generic;

public class SecuenciaManager4a : MonoBehaviour { 

    [Header("FASE 1 - LÁMPARA")]
    public bool fase1_NotaIndividual = true;  // ← Activar aquí

    private AttackManagerTutorial4a attackManager;

    void Start() {
        attackManager = FindObjectOfType<AttackManagerTutorial4a>();
    }

    // Llamado por NotaInteractiva cuando lámpara ilumina
    public void AgregarNota(string nombreNota) {

        if (!fase1_NotaIndividual) return;

        Debug.Log($"🔦 Lámpara iluminó: {nombreNota}");
        if (attackManager != null) {
            attackManager.NotificarNotaIluminada(nombreNota);
        }
    }
}