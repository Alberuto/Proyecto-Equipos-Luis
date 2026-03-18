using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class tocaDiscosManager : MonoBehaviour
{
    [SerializeField] private List<Transform> posicionesNotas;
    private List<GameObject> notas = new List<GameObject>();
    //private GameObject nota;
    private float timeDelayNota = 1f;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("mano"))
        {
            /* poner las notas en tocadiscos mediante collision
            //Debug.Log("Colision con el player");
            PlayerTake player = other.GetComponent<PlayerTake>();
            if (player.InstrumentoEntregado())
            {
                nota = player.getObjetoCogido();
                añadirNota(nota);
                foreach (var nota in notas)
                {
                    nota.transform.SetParent(transform);
                    int index = notas.IndexOf(nota);
                    nota.transform.position = posicionesNotas[index].position;
                    //transform.localEulerAngles = new Vector3(0f, 0f, 69.336f);
                }
                
                player.eliminarInstrumento(nota);
                player.SetCoger(false);
                player.SetInstrumentoEntregado(false);
                Debug.Log("nota entregada: " + nota.tag);
            }
    */
            if (notas.Count >= 1)
            {
                //suenan las notas
                foreach (var nota in notas)
                {
                    int index = notas.IndexOf(nota);
                    Debug.Log("Nota " + index + " : " + nota.tag);
                    //nota.GetComponent<AudioSource>().Play();
                    StopCoroutine("delay");
                    StartCoroutine(delay());
                }
            }
        }
    }
    private void añadirNota(GameObject nota)
    {
        notas.Add(nota);
    }
    public void eliminarNota(GameObject nota)
    {
        notas.Remove(nota);
    }
    public void setNota(GameObject obj)
    {
        añadirNota(obj);
        int index = notas.IndexOf(obj);
        ComprobarPosiciones();
        /*nota.transform.SetParent(posicionesNotas[index]);

        Debug.Log("Nota " + index + " colocada: " + nota.tag);
        nota.transform.position = posicionesNotas[index].position;*/
        //nota.transform.localEulerAngles = new Vector3(0f, 0f, 69.336f);
        /*switch (nota.tag)
            {
                case "C":
                    nota.transform.localEulerAngles = new Vector3(270, 180, 0);
                    break;
                case "C#":
                    nota.transform.localEulerAngles = new Vector3(0, 90, 180);
                    nota.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case "D":
                    nota.transform.localEulerAngles = new Vector3(274.30249f, 18.1386051f, 198.894836f);
                    break;
                case "D#":
                    nota.transform.localEulerAngles = new Vector3(278.107239f, 176.020538f, 76.3567886f);
                    break;
                case "E":
                    nota.transform.localEulerAngles = new Vector3(2.54443765f, 168.32901f, 354.313293f);
                    break;
                case "F":
                    nota.transform.localEulerAngles = new Vector3(356.406097f, 359.665283f, 359.020294f);
                    break;
                case "F#":
                    nota.transform.localEulerAngles = new Vector3(270, 180, 0);
                    break;
                case "G":
                    nota.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                    nota.transform.eulerAngles = new Vector3(270, 0, 0);
                break;
                case "G#":
                    nota.transform.localEulerAngles = new Vector3(270, 180, 0);
                    break;
                case "A":
                    nota.transform.localEulerAngles = new Vector3(284.555054f, 165.033829f, 13.669013f);
                    break;
                case "A#":
                    nota.transform.localEulerAngles = new Vector3(310.916504f, 180.148575f, 74.464859f);
                    break;
                case "B":
                    nota.transform.localEulerAngles = new Vector3(270, 180, 0);
                    break;

            }*/
        /*Debug.Log("posicion instrumento: " + nota.transform.parent);
        Debug.Log("angulo: " + nota.transform.eulerAngles);
        Debug.Log("angulo local: " + nota.transform.localEulerAngles);*/
        
    }

    public List<GameObject> getNotas()
    {
        return notas;
    }

    public void entregarNotas()
    {
        //GameManager.instance.setNotas(notas);
    }
    public void ComprobarPosiciones()
    {
        int posicion = 0;
        for (int i = 0; i < notas.Count; i++)
        {
            //Debug.Log("entro al for");
            posicion = i;
            int cantidadObjetos = GameObject.Find("toca discos/pos_ (" + i + ")").transform.childCount;
            //Debug.Log("cantidad objetos en posicion " + i + ": " + cantidadObjetos);
            if (cantidadObjetos == 0)
            {
                notas[i].transform.SetParent(posicionesNotas[i]);
                notas[i].transform.position = posicionesNotas[i].position;
                //Debug.Log("nueva posicion: " + i + "para: " + notas[i].tag);
                colocaionTocadiscos(notas[i]);
            }

        }
    }

    private void colocaionTocadiscos(GameObject instrumento)
    {
        switch (instrumento.tag)// - 5.960464e-08 => -1,4f =       - 0.149
        {
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
/*
 1º -0.5, 0.5                                                               2º 0.5, 0.5
    -0.3, 0.25          -0.1, 0.25           0.1, 0.25           0.3, 0.25
 
    -0.3, 0             -0.1, 0              0.1, 0              0.3, 0
         
    -0.3,-0.25          -0.1, -0.25          0.1, -0.25          0.3, -0.25
                                                                            4º 0.5, -0.5
 3º -0.5, -0.5
 */