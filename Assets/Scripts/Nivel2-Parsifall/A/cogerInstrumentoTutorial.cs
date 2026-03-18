using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para coger y soltar instrumentos - VERSIÓN TUTORIAL 2A
public class cogerInstrumentoTutorial : MonoBehaviour {

    private GameObject playerObject;
    private PlayerTakeTutorial player;  // ← Cambio clave: PlayerTakeTutorial
    private GameObject tocaDiscosObject;
    private tocaDiscosManagerTutorial tocaDiscos;  // ← Cambio clave: tocadiscos tutorial

    private bool cogido = false;
    public int dupeMax = 4;
    private GameObject parentObject;
    private string nombrePadre = "";
    private Transform padre;

    void Start() {

        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerTakeTutorial>();  // ← PlayerTakeTutorial

        tocaDiscosObject = GameObject.FindGameObjectWithTag("tocaDiscos");
        tocaDiscos = tocaDiscosObject.GetComponent<tocaDiscosManagerTutorial>();  // ← Tutorial tocadiscos

        dupeMax = 14;
    }

    private void Update() {
        if (player.espera == true && player.cogido == false) {
            player.coger = false;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("mano")) {
            if (player.coger == true && player.cogido == false) {
                // Si estaba en tocadiscos, eliminarlo (igual que original)
                if (tocaDiscos.getNotas().Contains(this.gameObject)) {
                    tocaDiscos.eliminarNota(this.gameObject);
                }
                cogido = true;
                player.InstrumentoCogido(true);
                GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
                if (objs.Length < dupeMax)  {
                    switch (tag) {
                        case "C": padre = GameObject.Find("ObjetosConSonido/C").transform; break;
                        case "C#": padre = GameObject.Find("ObjetosConSonido/C#").transform; break;
                        case "D": padre = GameObject.Find("ObjetosConSonido/D").transform; break;
                        case "D#": padre = GameObject.Find("ObjetosConSonido/D#").transform; break;
                        case "E": padre = GameObject.Find("ObjetosConSonido/E").transform; break;
                        case "F": padre = GameObject.Find("ObjetosConSonido/F").transform; break;
                        case "F#": padre = GameObject.Find("ObjetosConSonido/F#").transform; break;
                        case "G": padre = GameObject.Find("ObjetosConSonido/G").transform; break;
                        case "G#": padre = GameObject.Find("ObjetosConSonido/G#").transform; break;
                        case "A": padre = GameObject.Find("ObjetosConSonido/A").transform; break;
                        case "A#": padre = GameObject.Find("ObjetosConSonido/A#").transform; break;
                        case "B": padre = GameObject.Find("ObjetosConSonido/B").transform; break;
                    }
                    Instantiate(this, padre);
                    Debug.Log(this.name + ": " + objs.Length);
                }
                // Posiciones en mano (igual que original)
                transform.SetParent(other.transform);
                switch (tag) {
                    case "C":
                        transform.localPosition = new Vector3(0.0149999997f, -0.0209999997f, -0.479000002f);
                        transform.localEulerAngles = new Vector3(270, 180, 0);
                        break;
                    case "C#":
                        transform.localPosition = new Vector3(1.37385118f, -0.59594214f, -0.84602809f);
                        transform.localEulerAngles = new Vector3(0, 0, 180);
                        break;
                    case "D":
                        transform.localPosition = new Vector3(0.0350000001f, -3.16000009f, -0.768000007f);
                        transform.localEulerAngles = new Vector3(274.30249f, 18.1386051f, 198.894836f);
                        break;
                    case "D#":
                        transform.localPosition = new Vector3(0.101999998f, -0.861999989f, -0.31400001f);
                        transform.localEulerAngles = new Vector3(278.107239f, 176.020538f, 76.3567886f);
                        break;
                    case "E":
                        transform.localPosition = new Vector3(0.0209615529f, -0.807926178f, -0.263866484f);
                        transform.localEulerAngles = new Vector3(2.54443765f, 168.32901f, 354.313293f);
                        break;
                    case "F":
                        transform.localPosition = new Vector3(-0.00693426514f, 0.413905412f, -0.429204136f);
                        transform.localEulerAngles = new Vector3(356.406097f, 359.665283f, 359.020294f);
                        break;
                    case "F#":
                        transform.localPosition = new Vector3(0.0529999994f, 0.00999999978f, -0.486999989f);
                        transform.localEulerAngles = new Vector3(270, 180, 0);
                        break;
                    case "G":
                        transform.localPosition = new Vector3(0.0280000009f, -0.0160000008f, -1.63900006f);
                        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        break;
                    case "G#":
                        transform.localPosition = new Vector3(0.0149999997f, 0.0579999983f, -0.375f);
                        transform.localEulerAngles = new Vector3(270, 180, 0);
                        break;
                    case "A":
                        transform.localPosition = new Vector3(0.100800291f, -0.0834593475f, -0.662549973f);
                        transform.localEulerAngles = new Vector3(284.555054f, 165.033829f, 13.669013f);
                        break;
                    case "A#":
                        transform.localPosition = new Vector3(-0.270999998f, 0.175999999f, -0.268000007f);
                        transform.localEulerAngles = new Vector3(310.916504f, 180.148575f, 74.464859f);
                        break;
                    case "B":
                        transform.localPosition = new Vector3(-0.968999982f, -0.591000021f, -0.989000022f);
                        transform.localEulerAngles = new Vector3(270, 180, 0);
                        break;
                }
                player.añadirInstrumento(this.gameObject);
            }
        }
    }
    public void soltar() {
        transform.SetParent(null);
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        player.eliminarInstrumento();
        Debug.Log("Soltar");
    }
    public bool objetoCogido() {
        return cogido;
    }
}