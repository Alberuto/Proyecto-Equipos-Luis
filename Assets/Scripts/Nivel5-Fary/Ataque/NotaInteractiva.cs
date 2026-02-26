using UnityEngine;

public class NotaInteractiva : MonoBehaviour {

    [Header("Nota Musical")]
    public string nombreNota = "C";
    
    [SerializeField] private Material materialNormal;
    [SerializeField] private Material materialIluminado;
    private Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
        if (materialNormal == null) materialNormal = rend.material;
        gameObject.layer = LayerMask.NameToLayer("Notas"); // Layer específico
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
    public void Iluminar() {
        // Brilla 0.5s + llama SecuenciaManager
        IluminarTemporal();

        // ENCONTRAR SecuenciaManager y avisar
        SecuenciaManager secuencia = FindObjectOfType<SecuenciaManager>();
        if (secuencia != null) {
            secuencia.AgregarNota(nombreNota);
        }
    }
    void ResetColor() {
        GetComponent<Renderer>().material.color = Color.white;
    }
}