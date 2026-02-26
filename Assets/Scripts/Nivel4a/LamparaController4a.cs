using UnityEngine;

public class LamparaControllerLampara4a : MonoBehaviour {

    [SerializeField] private GameObject lamparaApagada;
    [SerializeField] private GameObject lamparaEncendida;
    [SerializeField] private LayerMask layerNotas = -1;

    private bool estaEncendida = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
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

        // 🆕 Activar FASE LÁMPARA primera vez
        if (estaEncendida) {
            AttackManagerTutorial4a tutorial = FindObjectOfType<AttackManagerTutorial4a>();
            if (tutorial != null) {
                tutorial.ActivarFaseLampara();
            }
        }
        Debug.Log(estaEncendida ? "🔦 LÁMPARA FASE 1 ON" : "💡 LÁMPARA OFF");
    }
    void HacerRaycast() {
        Ray rayo = new Ray(lamparaEncendida.transform.position, lamparaEncendida.transform.forward);
        if (Physics.Raycast(rayo, out RaycastHit hit, layerNotas))     {
            NotaInteractivaLampara4a nota = hit.collider.GetComponent<NotaInteractivaLampara4a>();
            if (nota != null) {
                nota.Iluminar();
            }
        }
        Debug.DrawRay(rayo.origin, rayo.direction * 50, Color.yellow);
    }
}