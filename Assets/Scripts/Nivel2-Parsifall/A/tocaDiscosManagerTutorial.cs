using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tocaDiscosManagerTutorial : MonoBehaviour {

    [SerializeField] private List<Transform> posicionesNotas;
    [SerializeField] private AttackManagerTutorialObjetos2A attackManager; // ← referencia al manager del tutorial

    private List<GameObject> notas = new List<GameObject>();
    private float timeDelayNota = 1f;

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("mano"))
            return;

        // Aquí va tu lógica de “suenan las notas” si quieres
        if (notas.Count >= 1) {

            foreach (var nota in notas)  {
                int index = notas.IndexOf(nota);
                Debug.Log("Nota " + index + " : " + nota.tag);
                StopCoroutine("delay");
                StartCoroutine(delay());
            }
        }
    }
    // ESTE es el método clave: lo llamas cuando el jugador DEPOSITA un instrumento
    public void setNota(GameObject obj) {
        // Tag de la nota cogida
        string tagNota = obj.tag;

        // Avisamos SIEMPRE al AttackManager para que lleve vidas y progreso
        attackManager.RegistrarObjetoCogido(tagNota);

        // Solo si la nota es la correcta ocupamos slot y la dejamos en el tocadiscos
        if (tagNota == attackManager.notaObjetivoActual) {
            añadirNota(obj);
            ComprobarPosiciones();
        }
        else {
            // Nota incorrecta: NO ocupar hueco, destruir objeto o devolverlo
            Debug.Log($"❌ Nota incorrecta en tocadiscos: {tagNota}, no ocupa slot");
            Destroy(obj);
        }
    }
    private void añadirNota(GameObject nota) {
        notas.Add(nota);
    }
    public void eliminarNota(GameObject nota) {
        notas.Remove(nota);
    }
    public List<GameObject> getNotas() {
        return notas;
    }
    public void ComprobarPosiciones() {
        for (int i = 0; i < notas.Count; i++) {
            int cantidadObjetos = GameObject.Find("toca discos/pos_ (" + i + ")").transform.childCount;

            if (cantidadObjetos == 0) {
                notas[i].transform.SetParent(posicionesNotas[i]);
                notas[i].transform.position = posicionesNotas[i].position;
                colocaionTocadiscos(notas[i]);
            }
        }
    }
    private void colocaionTocadiscos(GameObject instrumento) {
        // Copia aquí tu switch de rotaciones/posiciones tal cual lo tienes
        switch (instrumento.tag) {
            case "C":
                instrumento.transform.localEulerAngles = new Vector3(270, 270, 0);
                instrumento.transform.localScale = new Vector3(0.000860747066f, 0.000641551218f, 0.00263507734f);
                break;
            case "C#":
                instrumento.transform.localEulerAngles = new Vector3(180, -90, 0);
                instrumento.transform.position = new Vector3(instrumento.transform.position.x, instrumento.transform.position.y - 0.4f, instrumento.transform.position.z - 1.4f);
                instrumento.transform.localScale = new Vector3(0.00182493869f, 0.0042732046f, 0.00130204926f);
                break;
            case "D":
                instrumento.transform.localEulerAngles = new Vector3(270, 90, 0);
                instrumento.transform.position = new Vector3(instrumento.transform.position.x, instrumento.transform.position.y - 1.2f, instrumento.transform.position.z);
                break;
            case "D#":
                instrumento.transform.localEulerAngles = new Vector3(270, 270, 0);
                instrumento.transform.position = new Vector3(instrumento.transform.position.x, instrumento.transform.position.y - 0.4f, instrumento.transform.position.z);
                break;
            case "E":
                instrumento.transform.localEulerAngles = new Vector3(0, 90, 0);
                instrumento.transform.position = new Vector3(instrumento.transform.position.x, instrumento.transform.position.y - 0.3f, instrumento.transform.position.z);
                instrumento.transform.localScale = new Vector3(0.0745163262f, 0.215892285f, 0.0772308931f);
                break;
            case "F":
                instrumento.transform.localEulerAngles = new Vector3(0, 0, 0);
                instrumento.transform.position = new Vector3(instrumento.transform.position.x, instrumento.transform.position.y + 0.3f, instrumento.transform.position.z);
                break;
            case "F#":
                instrumento.transform.localEulerAngles = new Vector3(270, 90, 180);
                instrumento.transform.position = new Vector3(instrumento.transform.position.x, instrumento.transform.position.y - 0.085f, instrumento.transform.position.z);
                break;
            case "G":
                instrumento.transform.localEulerAngles = new Vector3(0, 0, 0);
                instrumento.transform.position = new Vector3(instrumento.transform.position.x, instrumento.transform.position.y - 0.05f, instrumento.transform.position.z);

                break;
            case "G#":
                instrumento.transform.localEulerAngles = new Vector3(270, 180, 90);
                break;
            case "A":
                instrumento.transform.localEulerAngles = new Vector3(270, 180, 0);
                instrumento.transform.position = new Vector3(instrumento.transform.position.x, instrumento.transform.position.y + 0.3f, instrumento.transform.position.z);
                break;
            case "A#":
                instrumento.transform.localEulerAngles = new Vector3(270, 0, 0);
                instrumento.transform.position = new Vector3(instrumento.transform.position.x, instrumento.transform.position.y + 0.2f, instrumento.transform.position.z);
                break;
            case "B":
                instrumento.transform.localEulerAngles = new Vector3(270, 0, 0);
                instrumento.transform.localScale = new Vector3(9.99999975e-05f, 0.000227646044f, 0.000495513959f);
                break;
        }
    }
    IEnumerator delay() {
        yield return new WaitForSeconds(timeDelayNota);
    }
}