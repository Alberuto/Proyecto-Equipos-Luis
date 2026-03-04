using GLTFast.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class LamparaController : MonoBehaviour {

    [SerializeField] private GameObject lamparaApagada;
    [SerializeField] private GameObject lamparaEncendida;
    [SerializeField] private LayerMask layerNotas; // Layer "Notas"
    private float distanciaRayo = 5f;

    private bool estaEncendida = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)){ // Tu botón
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
        Transform hijo = lamparaEncendida.transform.Find("Spot Light");
        Ray rayo = new Ray(hijo.transform.position, hijo.transform.forward);
        if (Physics.Raycast(rayo, out RaycastHit hit, distanciaRayo, layerNotas)) {
            Debug.DrawRay(rayo.origin, rayo.direction * distanciaRayo, Color.green);
            NotaInteractiva nota = hit.collider.GetComponent<NotaInteractiva>();
            Debug.Log("nota: "+ nota);
            if (nota != null)
            {
                nota.Iluminar();
                Debug.Log($"🎯 Nota iluminada: {nota.nombreNota}");
            }
            else Debug.Log("XD");
        }
        //Debug.DrawRay(rayo.origin, rayo.direction * 50, Color.yellow); // Visual
    }
}