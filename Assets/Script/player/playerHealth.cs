using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Tiempo de invulnerabilidad despues de recibir daño
    private float invulnerableTime = 10.0f;
    private bool invulnerable = false;

    public int dañoKiko = 10;
    public int dañoCigala = 30;
    public int dañoFary = 60;
    public bool atacado = false;

    // Metodo para detectar colisiones con enemigos
    void OnTriggerEnter(Collider other)
    {
        if (invulnerable)
        {
            Debug.Log("Jugador es invulnerable, no recibe daño");
            return;
        }
        else if (other.gameObject.CompareTag("kiko"))
        {
            GameManager.instance.recibirDaño(dañoKiko);
            atacado = true;

        }
        else if (other.gameObject.CompareTag("cigala"))
        {
            GameManager.instance.recibirDaño(dañoCigala);
            atacado = true;

        }
        else if (other.gameObject.CompareTag("fary"))
        {
            GameManager.instance.recibirDaño(dañoFary);
            atacado = true;
        }
        if (atacado)
        {
            invulnerable = true;
            Debug.Log("Jugador ha recibido daño de: " + other.tag);
            StopCoroutine("delay");
            StartCoroutine(delay());
            atacado = false;
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