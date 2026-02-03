using UnityEngine;

public class viniloGiro : MonoBehaviour
{
    public Transform centro;
    public float radio = 2f;
    public float velocidad = 40f;
    public bool sentidoHorario = true;
    public float velocidadCentro = 1f;
    public float distanciaCentro = 5f;
    public float velocidadGiro = 720f;
    private float anguloActual;
    private Vector3 eulerInicial;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lejos();
    }

    // Update is called once per frame
    void Update()
    {
        eulerInicial = transform.eulerAngles;
        float desplazamientoCentro = Mathf.Sin(Time.time * velocidadCentro) * (distanciaCentro * 0.5f);
        Vector3 centroDinamico = centro.position + Vector3.right * desplazamientoCentro;
        eulerInicial.y += velocidadGiro * Time.deltaTime;
        float deltaAngulo = velocidad * Mathf.Deg2Rad * Time.deltaTime * (sentidoHorario ? 1f : -1f);
        anguloActual += deltaAngulo;
        Vector3 posicionObjetivo = centroDinamico + new Vector3(
            Mathf.Cos(anguloActual) * radio,
            1,
            Mathf.Sin(anguloActual) * radio
        );
        transform.position = Vector3.Slerp(transform.position, posicionObjetivo, Time.deltaTime * 8f);
    }
    void lejos()
    {
        Debug.Log("lejos X: " + (transform.position.x - centro.transform.position.x));
        Debug.Log("lejos Z: " + (transform.position.z - centro.transform.position.z));
        Debug.Log("distancia al centro: " + Vector3.Distance(transform.position, centro.position));
    }
}
