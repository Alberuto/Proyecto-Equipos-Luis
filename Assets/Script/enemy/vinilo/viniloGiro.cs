using Unity.Mathematics;
using UnityEngine;

public class viniloGiro : MonoBehaviour
{
    public Transform centro;
    public float radio;
    private float minRadio = 1f; // Radio mínimo para destruir el vinilo
    public float velocidad = 40f;
    private float aumentoVel = 10f;
    public bool sentidoHorario = true;
    private float anguloActual;
    public float alturaVinilo = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //lejos();
        // calculo la distancia al centro
        radio = Vector3.Distance(transform.position, centro.transform.position);
        // calculo el angulo de cada vinilo respecto al centro para que empiece a girar desde su posición actual
        anguloActual = Mathf.Atan2(transform.position.z - centro.position.z, transform.position.x - centro.position.x);
    }

    // Update is called once per frame
    void Update()
    {
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
        radio -= 1f * Time.deltaTime; // Disminuye el radio con el tiempo para acercarse al centro
        if (radio < minRadio)
        {
            Destroy(gameObject);
        }
        velocidad += aumentoVel * Time.deltaTime; // Aumenta la velocidad con el tiempo para un giro más frenético
    }
    // Método para saber la distancia al centro
    void lejos()
    {
        Debug.Log("lejos X: " + (transform.position.x - centro.transform.position.x));
        Debug.Log("lejos Z: " + (transform.position.z - centro.transform.position.z));
        Debug.Log("distancia al centro: " + Vector3.Distance(transform.position, centro.position));
    }
}
