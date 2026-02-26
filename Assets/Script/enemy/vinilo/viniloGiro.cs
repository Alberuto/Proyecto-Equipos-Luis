using System.Collections;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class viniloGiro : MonoBehaviour
{
    private Transform centro;
    private float radio;
    private float minRadio = 1.5f; // Radio mínimo para destruir el vinilo
    private float velocidad;
    private float velocidadMax = 300f;
    private float aumentoVel;
    public bool sentidoHorario = true;// se puede cambiar en el inspector para que empiece en sentido antihorario
    private bool cambioPosible = true;
    private float anguloActual;
    private float alturaVinilo;
    private float sentidoRadio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // velocidades iniciales aleatorias para cada vinilo para que no se muevan todos igual
        velocidad = Random.Range(50f, 100f);
        aumentoVel = Random.Range(10f, 30f);
        sentidoRadio = -1f;
        alturaVinilo = transform.position.y;
        centro = GameObject.FindGameObjectWithTag("centro").transform;
        //lejos();
        // calculo la distancia al centro
        radio = Vector3.Distance(transform.position, centro.transform.position);
        // calculo el angulo de cada vinilo respecto al centro para que empiece a girar desde su posición actual
        anguloActual = Mathf.Atan2(transform.position.z - centro.position.z, transform.position.x - centro.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0,100) == 2 && cambioPosible)
        {
            cambioPosible = false;
            sentidoHorario = !sentidoHorario; // Cambia el sentido de giro aleatoriamente
            if (Random.Range(0,10) == 2 && radio <= 15f)
            {
                if (sentidoRadio == -1f)
                {
                    sentidoRadio = 1f;
                }
                else
                {
                    sentidoRadio = -1f;
                }
                
                Debug.Log(this.name + " cambio: " + sentidoRadio + " con radio: " + radio);
            }
            StopCoroutine("DelaySentido");
            StartCoroutine(DelaySentido());
        }
        if (radio >= 20f)
        {
            sentidoRadio = -1f;
        }
        // prueba de furia
        /*if (Random.Range(0,50) < 10)
        {
            if (GameManager.instance.isFuria())
            {
                aumentoVel = Random.Range(20f, 50f);
            }
            else
            {
                aumentoVel = Random.Range(-20f, 40f);
            }
        }*/
        
        // calculo el nuevo angulo sumando la velocidad al angulo actual
        float deltaAngulo = velocidad * Mathf.Deg2Rad * Time.deltaTime * (sentidoHorario ? 1f : -1f);
        anguloActual += deltaAngulo;
        // calculo la nueva posición del vinilo usando el nuevo angulo y el radio
        Vector3 posicionObjetivo = new Vector3(
            math.cos(anguloActual) * radio,
            alturaVinilo,
            Mathf.Sin(anguloActual) * radio
        );
        transform.position = posicionObjetivo;
        radio += sentidoRadio * Time.deltaTime; // Disminuye el radio con el tiempo para acercarse al centro
        if (radio < minRadio)
        {
            Destroy(gameObject);
        }
        if (velocidad <= velocidadMax)
        {
            velocidad += aumentoVel * Time.deltaTime; // Aumenta la velocidad de giro con el tiempo hasta la velocidadMax
        }
        else
        {
            velocidad = velocidadMax;
        }
        
    }
    IEnumerator DelaySentido()
    {
        yield return new WaitForSeconds(Random.Range(4.0f, 12.0f));
        cambioPosible = true;
    }

    // Método para saber la distancia al centro prueba
    void lejos()
    {
        Debug.Log("lejos X: " + (transform.position.x - centro.transform.position.x));
        Debug.Log("lejos Z: " + (transform.position.z - centro.transform.position.z));
        Debug.Log("distancia al centro: " + Vector3.Distance(transform.position, centro.position));
    }
}
