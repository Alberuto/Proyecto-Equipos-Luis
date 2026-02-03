using System.Collections;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    // Vida del jugador
    public int vida = 100;
    // Tiempo de invulnerabilidad despues de recibir daño
    private float invulnerableTime = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Metodo para detectar colisiones con enemigos
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            vida -= 10;
            Debug.Log("Vida del jugador: " + vida);
            StartCoroutine(delay());
        }
    }

    // Coroutine para manejar el tiempo de invulnerabilidad
    IEnumerator delay()
    {
        yield return new WaitForSeconds(invulnerableTime);
    }
}
