using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float invulnerableTime = 5.0f;
    private bool invulnerable = false;
    public int dañoKiko = 10;
    public int dañoCigala = 30;
    public int dañoFary = 60;
    public bool atacado = false;
    private PlayerMovement move;

    void Start()
    {
        move = GetComponent<PlayerMovement>();
        invulnerable = true;
        StartCoroutine(InvulnerabilidadInicial(5.5f));
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"🔍 PlayerHealth hit: {other.tag}");

        if (invulnerable)
        {
            Debug.Log("Jugador es invulnerable");
            return;
        }

        // 🆕 DETERMINAR daño
        int daño = 0;
        if (other.CompareTag("kiko"))
        {
            daño = dañoKiko;
        }
        else if (other.CompareTag("cigala"))
        {
            daño = dañoCigala;
        }
        else if (other.CompareTag("fary"))
        {
            daño = dañoFary;
        }

        if (daño > 0)
        {
            Debug.Log($"💥 DAÑO {other.tag}: {daño}");

            // APLICO DAÑO
            GameManager.instance.RecibirDamage(daño);

            // INVULNERABILIDAD INMEDIATA
            atacado = true;
            invulnerable = true;
            if (move != null) move.recibiendoDaño = true;

            // 🆕 UNA SOLA CORUTINA
            StopAllCoroutines();
            StartCoroutine(Invulnerabilidad(invulnerableTime));
        }
    }

    IEnumerator InvulnerabilidadInicial(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        invulnerable = false;
        Debug.Log("✅ Jugador vulnerable (inicio)");
    }

    IEnumerator Invulnerabilidad(float tiempo)
    {
        Debug.Log($"🛡️ Invulnerable por {tiempo}s");
        yield return new WaitForSeconds(tiempo);

        invulnerable = false;
        atacado = false;
        if (move != null) move.recibiendoDaño = false;
        Debug.Log("✅ Jugador vulnerable otra vez");
    }
}
