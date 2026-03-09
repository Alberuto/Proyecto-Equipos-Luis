using GLTFast.Schema;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LamparaController : MonoBehaviour {

    [SerializeField] private GameObject lamparaApagada;
    [SerializeField] private GameObject lamparaEncendida;
    [SerializeField] private LayerMask layerNotas; // Layer "Notas"
    private float distanciaRayo = 5f;

    private bool estaEncendida = false;

    private bool detectado = false;
    void Update() {
        if (Input.GetKey(KeyCode.F)){ // Tu botón
            //ToggleLampara();
            lamparaEncendida.SetActive(true);
            lamparaApagada.SetActive(false);
            //Debug.Log(prueba);
            if (!detectado)
            {
                HacerRaycast();
            }
        }
        else
        {
            detectado = false;
            lamparaEncendida.SetActive(false);
            lamparaApagada.SetActive(true);
        }
        /*if (Input.GetKeyUp(KeyCode.F))
        {
            ToggleLampara();
        }*/
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
            string nota = hit.transform.tag;
            /*NotaInteractiva nota = hit.collider.GetComponent<NotaInteractiva>();
            Debug.Log("nota: "+ nota);
            if (nota != null)
            {
                nota.Iluminar();
                Debug.Log($"🎯 Nota iluminada: {nota}");
            }*/
            detectado = true;
            AttackManagerSecuencia4 attackMgr = FindObjectOfType<AttackManagerSecuencia4>();
            if (attackMgr != null)
            {
                attackMgr.RegistrarNotaJugador(nota);
                Debug.Log($"🎯 5 ataque: {nota} registrado");
            }
            AttackManagerSecuencia4a2 attackMgr4a2 = FindObjectOfType<AttackManagerSecuencia4a2>();
            if (attackMgr4a2 != null)
            {
                attackMgr4a2.RegistrarNotaJugador(nota);
                Debug.Log($"🎯 4a2 ataque: {nota} registrado");
            }
        }
        //Debug.DrawRay(rayo.origin, rayo.direction * 50, Color.yellow); // Visual
    }
    
}