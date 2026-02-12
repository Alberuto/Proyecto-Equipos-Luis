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
        this.nota = obj;
        añadirNota(nota);
        foreach (var nota in notas)
        {
            nota.transform.SetParent(transform);
            int index = notas.IndexOf(nota);
            nota.transform.position = posicionesNotas[index].position;
            //transform.localEulerAngles = new Vector3(0f, 0f, 69.336f);
        }
    }
    public List<GameObject> getNotas()
    {
        return notas;
    }

}
