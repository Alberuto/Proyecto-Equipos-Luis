using UnityEngine;

public class LamparaController : MonoBehaviour {

    [SerializeField] private GameObject lamparaApagada;
    [SerializeField] private GameObject lamparaEncendida;
    [SerializeField] private float distanciaRayo = 10f;
    [SerializeField] private LayerMask layerNotas = -1; // Layer "Notas"

    private bool estaEncendida = false;
    private SecuenciaManager secuenciaManager;

    void Start() {
        secuenciaManager = FindObjectOfType<SecuenciaManager>();
        lamparaEncendida.SetActive(false);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){ // Tu botón
            ToggleLampara();
        }
        if (estaEncendida) {
            HacerRaycast();
        }
    }
    void ToggleLampara() {
        estaEncendida = !estaEncendida;
        lamparaApagada.SetActive(!estaEncendida);
        lamparaEncendida.SetActive(estaEncendida);
        Debug.Log(estaEncendida ? "?? LÁMPARA ON - Iluminando..." : "?? LÁMPARA OFF");
    }
    void HacerRaycast() {
        Ray rayo = new Ray(lamparaEncendida.transform.position, lamparaEncendida.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, distanciaRayo, layerNotas))
        {
            // ?? ¿Es una NOTA?
            NotaInteractiva nota = hit.collider.GetComponent<NotaInteractiva>();
            if (nota != null)
            {
                Debug.Log($"?? Iluminada Nota: {nota.numeroNota}");
                secuenciaManager.AgregarANotaIluminada(nota.numeroNota);

                // Visual feedback
                nota.IluminarTemporal();
            }
        }
        Debug.DrawRay(rayo.origin, rayo.direction * distanciaRayo, Color.yellow);
    }
}