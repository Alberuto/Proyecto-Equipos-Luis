using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Tiempo de invulnerabilidad despues de recibir daño
    private float invulnerableTime = 5.0f;
    private bool invulnerable = false;

    public int dañoKiko = 10;
    public int dañoCigala = 15;
    public int dañoFary = 25;
    public bool atacado = false;

    private PlayerMovement move;

    [SerializeField]
    private DefensaManager5 dm;
    public void Start()
    {
        move = GetComponent<PlayerMovement>();
        dm = GetComponent<DefensaManager5>();
        invulnerable = true;
        StopCoroutine("delay");
        StartCoroutine(delay(7.0f));
    }

    // Metodo para detectar colisiones con enemigos
    void OnTriggerEnter(Collider other)
    {
        if (invulnerable)
        {
            Debug.Log("Jugador es invulnerable, no recibe daño de "+other.name);
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
            dm.ActualizarUI();
        }
        if (atacado)
        {
            invulnerable = true;
            move.recibiendoDaño = true;
            StopCoroutine("delayAnimation");
            StartCoroutine(delayAnimation());
            Debug.Log("Jugador ha recibido daño de: " + other.tag);
            StopCoroutine("delay");
            StartCoroutine(delay(invulnerableTime));
            atacado = false;
        }
        
    }
    // Coroutine para manejar el tiempo de invulnerabilidad
    IEnumerator delay(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Jugador ya no es invulnerable");
        invulnerable = false;
    }
    IEnumerator delayAnimation()
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Jugador ya no es invulnerable");
    }
}