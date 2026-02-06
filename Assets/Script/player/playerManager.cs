using System.Collections;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    // Vida del jugador
    public int vida = 100;
    // Tiempo de invulnerabilidad despues de recibir daño
    private float invulnerableTime = 10.0f;
    private bool invulnerable = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {
            Debug.Log("Jugador ha muerto");
        }
    }

    // Metodo para detectar colisiones con enemigos
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            if (invulnerable)
            {
                Debug.Log("Jugador es invulnerable, no recibe daño");
                return;
            }
            else
            {
                invulnerable = true;
                vida -= 10;
                Debug.Log("Vida del jugador: " + vida);
                Debug.Log("Jugador ha recibido daño de: " + other.name);
                StopCoroutine("delay");
                StartCoroutine(delay());
            }
                
        }
    }

    // Coroutine para manejar el tiempo de invulnerabilidad
    IEnumerator delay()
    {
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
        Debug.Log("Jugador ya no es invulnerable");
    }
}
