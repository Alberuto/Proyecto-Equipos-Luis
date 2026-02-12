using System.Collections;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    // Vida del jugador
    public int vida = 100;
    // variables para animacion
    public bool recibiendoDaño = false;
    public bool muerto = false;

    // Tiempo de invulnerabilidad despues de recibir daño
    public int daño = 10;
    private float invulnerableTime = 10.0f;
    private bool invulnerable = false;

    private void Start()
    {
        muerto = false;
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
                if (vida <= daño)
                {
                    muerto = true;
                    Debug.Log("Jugador ya ha muerto, no recibe más daño");
                }
                else
                {
                    invulnerable = true;
                    recibiendoDaño = true;
                    vida -= daño;
                    Debug.Log("Vida del jugador: " + vida);
                    Debug.Log("Jugador ha recibido daño de: " + other.name);
                    StopCoroutine("delay");
                    StartCoroutine(delay());
                }
                
            }
                
        }
    }

    // Coroutine para manejar el tiempo de invulnerabilidad
    IEnumerator delay()
    {
        yield return new WaitForSeconds(2.5f);
        recibiendoDaño = false;
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
        Debug.Log("Jugador ya no es invulnerable");
    }
}
