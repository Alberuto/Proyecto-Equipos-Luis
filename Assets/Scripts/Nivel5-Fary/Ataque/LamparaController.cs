using UnityEngine;

public class LamparaController : MonoBehaviour {

    [SerializeField] private GameObject lamparaApagada;
    [SerializeField] private GameObject lamparaEncendida;
    [SerializeField] private LayerMask layerNotas = -1; // Layer "Notas"

    private bool estaEncendida = false;

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
        // ← Spotlight Range/Inspector controla distancia
        Ray rayo = new Ray(lamparaEncendida.transform.position, lamparaEncendida.transform.forward);
        if (Physics.Raycast(rayo, out RaycastHit hit, layerNotas)) {
            NotaInteractiva nota = hit.collider.GetComponent<NotaInteractiva>();
            if (nota != null) {
                nota.Iluminar();
                Debug.Log($"🎯 Nota iluminada: {nota.nombreNota}");
            }
        }
        Debug.DrawRay(rayo.origin, rayo.direction * 50, Color.yellow); // Visual
    }
}