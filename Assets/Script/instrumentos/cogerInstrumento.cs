using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.LowLevel;
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
    // dupes del instrumento (se podria controllar en el gameManager) Las teclas cuentan como objeto dupe pero es para que suenen sus respecivos sonidos con el tag
    public int dupeMax = 4;
    private Transform padre;


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
        /*if (player.coger == false && cogido == true)
        {
            soltar();
            cogido = false;
            player.cogido = false;
        }*/
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
                if (tocaDiscos.getNotas().Contains(this.gameObject))
                {
                    tocaDiscos.eliminarNota(this.gameObject);
                }
                cogido = true;



                GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
                if (objs.Length < dupeMax)
                {
                    // Posicion para que se muestren donde estaban
                    switch (tag)
                    {
                        case "C":
                            padre = GameObject.Find("Objetos escena/C").transform;
                            break;
                        case "C#":
                            padre = GameObject.Find("Objetos escena/C#").transform;
                            break;
                        case "D":
                            padre = GameObject.Find("Objetos escena/D").transform;
                            break;
                        case "D#":
                             padre = GameObject.Find("Objetos escena/D#").transform;
                            break;
                        case "E":
                            padre = GameObject.Find("Objetos escena/E").transform;
                            break;
                        case "F":
                            padre = GameObject.Find("Objetos escena/F").transform;
                            break;
                        case "F#":
                            padre = GameObject.Find("Objetos escena/F#").transform;
                            break;
                        case "G":
                            padre = GameObject.Find("Objetos escena/G").transform;
                            break;
                        case "G#":
                            padre = GameObject.Find("Objetos escena/G#").transform;
                            break;
                        case "A":
                            padre = GameObject.Find("Objetos escena/A").transform;
                            break;
                        case "A#":
                            padre = GameObject.Find("Objetos escena/A#").transform;
                            break;
                        case "B":
                            padre = GameObject.Find("Objetos escena/B").transform;
                            break;

                    }
                    Instantiate(this, padre);
                    Debug.Log(this.name + ": " + objs.Length);
                }
                transform.SetParent(other.transform);
                //posiciones de los instrumentos al cogerlos
                switch (tag)
                {
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

