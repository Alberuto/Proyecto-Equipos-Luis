using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tocaDiscosManager : MonoBehaviour
{
    [SerializeField] private List<Transform> posicionesNotas;
    private List<GameObject> notas = new List<GameObject>();
    private GameObject nota;



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
        nota = obj;
        añadirNota(nota);
        int index = notas.IndexOf(nota);
        /*for(int i = 0; i < notas.Count ;i++)
        {
            index = i;
            if (notas[i] == null)
                notas[i].transform.SetParent(posicionesNotas[i]);
        }*/
        nota.transform.SetParent(posicionesNotas[index]);
        Debug.Log("Nota " + index + " colocada: " + nota.tag);
        nota.transform.position = posicionesNotas[index].position;
        //nota.transform.localEulerAngles = new Vector3(0f, 0f, 69.336f);
        switch (nota.tag)
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

            }
        Debug.Log("posicion instrumento: " + nota.transform.parent);
        Debug.Log("angulo: " + nota.transform.eulerAngles);
        Debug.Log("angulo local: " + nota.transform.localEulerAngles);

    }

    public List<GameObject> getNotas()
    {
        return notas;
    }

}


/*
 1º -0.5, 0.5
                                                                               2º 0.5, 0.5
 
 
 
    -0.3, 0.25          -0.1, 0.25           0.1, 0.25           0.3, 0.25
 
    -0.3, 0             -0.1, 0              0.1, 0              0.3, 0
         
    -0.3,-0.25          -0.1, -0.25          0.1, -0.25          0.3, -0.25
                                                                            4º 0.5, -0.5
 3º -0.5, -0.5
 */