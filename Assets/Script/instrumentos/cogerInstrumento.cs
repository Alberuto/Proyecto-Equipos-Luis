using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


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
    // dupes del instrumento (se podria controllar en el gameManager)
    public int dupeMax = 3;


    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerTake>();
        tocaDiscosObject = GameObject.FindGameObjectWithTag("tocaDiscos");
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



                GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
                if (objs.Length < dupeMax)
                {
                    Instantiate(this);
                    Debug.Log(this.name + ": " + objs.Length);
                }
                transform.SetParent(other.transform);
                transform.localPosition = new Vector3(0.0130000003f, -0.0160000008f, -0.342999995f);
                transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                player.añadirInstrumento(gameObject);
            }
        }
    }
    // espalda
    // posicion Vector3(0.0130000003,-0.0160000008,-0.342999995)
    // rotacion Vector3(0,0,0)

    // posicion y rotacion con respecto a la mano para que el objeto quede bien cogido
    // position Vector3(-0.324000001,0.171000004,0.0810000002)  new Vector3(-0.324000001f, 0.171000004f, 0.0810000002f);
    // rotacion 0, 0, 69.336  new Vector3(0f, 0f, 69.336f);


    // funcion para dejar el objeto en el suelo
    public void soltar()
    {
        transform.SetParent(null);
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        player.eliminarInstrumento(); 
        Debug.Log("Soltar");

    }

    // funcion para saber si el objeto esta cogido que ytilizamos en el PlayerTake
    public bool objetoCogido()
    {
        return cogido;
    }

    // position Vector3(54.6739998,21.8920002,-7.12699986)
}

