using UnityEngine;

public class PlayerTake2A : MonoBehaviour {

    [Header("Controles")]
    public bool coger = false;      // E pulsado
    public bool cogido = false;    // Ya tiene algo cogido
    public bool espera = false;
    private GameObject instrumentoActual;
    private float tiempoEspera = 2f;
    private float tiempoInicioEspera;

    void Update() {
        if (espera && !cogido)
        {
            if (Time.time - tiempoInicioEspera > tiempoEspera)
            {
                coger = false;
                espera = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
            coger = true;

        if (Input.GetKeyUp(KeyCode.E))
            coger = false;
    }
    public void añadirInstrumento(GameObject instrumento) {
        instrumentoActual = instrumento;
        cogido = true;
        espera = true;
        tiempoInicioEspera = Time.time;
    }

    public void eliminarInstrumento() {
        instrumentoActual = null;
        cogido = false;
        espera = false;
    }

    public GameObject getObjetoCogido() => instrumentoActual;
    public bool InstrumentoEntregado() => false;
    private void OnTriggerEnter(Collider other) {
        // Solo notas (C, C#, D, etc.)
        string[] notasValidas = {
        "C", "C#", "D", "D#", "E", "F", "F#",
        "G", "G#", "A", "A#", "B"
    };

        if (System.Array.IndexOf(notasValidas, other.tag) == -1)
            return;

        if (coger == false)
            return;

        if (cogido == true)
            return;

        Debug.Log("✅ AGARRANDO NOTA: " + other.tag);

        // Suena la nota
        NotaAgarrable nota = other.GetComponent<NotaAgarrable>();
        if (nota != null)
            nota.Activar();

        // Registra la nota en el tutorial
        AttackManagerTutorialObjetos2A tutorial = FindObjectOfType<AttackManagerTutorialObjetos2A>();
        if (tutorial != null)
            tutorial.RegistrarObjetoCogido(other.tag);

        // El Player ahora lleva el objeto
        añadirInstrumento(other.gameObject);
        // Opcional: quitar el objeto de la escena
        // Destroy(other.gameObject);
    }
}