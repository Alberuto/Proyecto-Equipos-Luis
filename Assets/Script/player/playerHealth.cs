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

    private PlayerMovement move;

    public void Start()
    {
        move = GetComponent<PlayerMovement>();
        invulnerable = true;
        StopCoroutine("delay");
        StartCoroutine(delay(7.0f));
    }

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
            GameManager.instance.RecibirDamage(dañoKiko);
            atacado = true;

        }
        else if (other.gameObject.CompareTag("cigala"))
        {
            GameManager.instance.RecibirDamage(dañoCigala);
            atacado = true;

        }
        else if (other.gameObject.CompareTag("fary"))
        {
            GameManager.instance.RecibirDamage(dañoFary);
            atacado = true;
        }
        if (atacado)
        {
            invulnerable = true;
            move.recibiendoDaño = true;
            StopCoroutine("delay");
            StartCoroutine(delay(2.5f));
            Debug.Log("Jugador ha recibido daño de: " + other.tag);
            StopCoroutine("delay");
            StartCoroutine(delay(invulnerableTime));
            atacado = false;
            invulnerable = false;
        }
        
    }
    // Coroutine para manejar el tiempo de invulnerabilidad
    IEnumerator delay(float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("Jugador ya no es invulnerable");
    }
}