using UnityEngine;

public class NotaInteractiva : MonoBehaviour {

    public int numeroNota = 0;  // 1, 2, 3, 4...
    [SerializeField] private Material materialNormal;
    [SerializeField] private Material materialIluminado;
    private Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
        gameObject.layer = LayerMask.NameToLayer("Notas"); // Layer específico
    }
    public void IluminarTemporal() {
        rend.material = materialIluminado;
        Invoke(nameof(ResetMaterial), 0.5f);
    }
    void ResetMaterial() {
        rend.material = materialNormal;
    }
}