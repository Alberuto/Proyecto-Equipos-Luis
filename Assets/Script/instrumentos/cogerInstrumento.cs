using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


// Script para coger y soltar instrumentos
public class cogerInstrumento : MonoBehaviour
{
    // se asignan el toca discos y el player
    private GameObject playerObject;
    private PlayerTake player;
    private GameObject tocaDiscosObject;
    private tocaDiscosManager tocaDiscos;
    // bolleano para saber si el objeto esta cogido
    private bool cogido = false;


    void Start()
    {
        playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        player = playerObject.GetComponent<PlayerTake>();
        tocaDiscosObject = GameObject.FindGameObjectsWithTag("tocaDiscos")[0];
        tocaDiscos = tocaDiscosObject.GetComponent<tocaDiscosManager>();
    }
    private void Update()
    {
        // si ha pasado x tiempo (ajustable desde PlayerTake) desde que se ha pulsado el boton de coger y el objeto no esta cogido, se desactiva la opcion de coger
        if (player.espera == true && player.cogido == false)
        {
            player.coger = false;
        }
        // si el objeto esta cogido y el jugador le da al boton de coger, se suelta el objeto
        if (player.coger == false && cogido == true)
        {
            soltar();
            cogido = false;
            player.cogido = false;
        }
    }

    // funcion para detectar la colision con la mano
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("mano"))
        {
            //Debug.Log("Colision con la mano");
            
            // esta el player.cogido para que no pueda coger varios objetos a la vez
            if (player.coger == true && player.cogido == false)
            {
                if (tocaDiscos.getNotas().Contains(gameObject))
                {
                    tocaDiscos.eliminarNota(gameObject);
                }
                cogido = true;
                transform.SetParent(other.transform);
                transform.localPosition = new Vector3(-0.324000001f, 0.171000004f, 0.0810000002f);
                transform.localEulerAngles = new Vector3(0f, 0f, 69.336f);
                player.añadirInstrumento(gameObject);
            }
        }
    }

    // position Vector3(-0.324000001,0.171000004,0.0810000002)
    // rotacion 0, 0, 69.336


    // funcion para dejar el objeto en el suelo
    public void soltar()
    {
        transform.SetParent(null);
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        player.eliminarInstrumento(gameObject); 
        Debug.Log("Soltar palo");

    }

    // funcion para saber si el objeto esta cogido que ytilizamos en el PlayerTake
    public bool objetoCogido()
    {
        return cogido;
    }

    // position Vector3(54.6739998,21.8920002,-7.12699986)
}

