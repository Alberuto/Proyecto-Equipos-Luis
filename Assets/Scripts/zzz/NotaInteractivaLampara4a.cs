using UnityEngine;

public class NotaInteractivaLampara4a : MonoBehaviour {

    [Header("Nota Musical - FASE 1")]
    public string nombreNota = "C";  // C, D, E, F...

    [SerializeField] private Material materialNormal;
    [SerializeField] private Material materialIluminado;
    private Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
        if (materialNormal == null) materialNormal = rend.material;
        gameObject.layer = LayerMask.NameToLayer("Notas");
    }
    public void IluminarTemporal() {
        if (materialIluminado != null) {
            rend.material = materialIluminado;
            Invoke(nameof(ResetMaterial), 0.5f);
        }
    }
    void ResetMaterial() {

        rend.material = materialNormal;
    }

    // 🆕 ESPECÍFICO FASE 1: Notifica AttackManagerTutorial
    public void Iluminar() {
        IluminarTemporal();
        SecuenciaManager4a secuencia = FindObjectOfType<SecuenciaManager4a>();
        if (secuencia != null) {
            secuencia.AgregarNota(nombreNota);
        }
        Debug.Log($"✨ Nota {nombreNota} iluminada - FASE 1");
    }
}