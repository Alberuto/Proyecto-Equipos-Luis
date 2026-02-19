using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // Vida del jugador y enemigos
    public int vida = 100;
    public int vidaKiko = 30;
    public int vidaCigala = 60;
    public int vidaFary = 100;

    // listas para las notas
    public List<GameObject> notas;
    public List<GameObject> partitura;

    // variables para animacion
    public bool recibiendoDaño = false;
    public bool muerto = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        muerto = false;
    }

    public void recibirDaño(int daño)
    {
        
        if (vida <= daño)
        {
            muerto = true;
            Debug.Log("Jugador ha muerto");
            MySceneManager.instance.LoadScene("GameOver");
        }
        else
        {
            vida -= daño;
            Debug.Log("Jugador ha recibido " + daño + " de daño, vida restante: " + vida);
            recibiendoDaño = true;
            StopCoroutine("delay");
            StartCoroutine(delay());
        }
    }


    // Coroutine para manejar el tiempo de animacion para que no se mueva
    IEnumerator delay()
    {
        yield return new WaitForSeconds(2.5f);
        recibiendoDaño = false;
    }

    public void reiniciarJuego()
    {
        vida = 100;
        muerto = false;
        MySceneManager.instance.LoadScene("MainMenu");
    }
    public void setNotas(List<GameObject> lista)
    {
        notas = lista;
    }
    public void setPartitura(List<GameObject> lista)
    {
        partitura = lista;
    }

    public bool secuenciaCorrecta()
    {
        for (int i = 0; i < partitura.Count; i++)
        {
            if (notas[i].tag != partitura[i].tag)
            {
                Debug.Log("Secuencia incorrecta en la posición " + i);
                return false;
            }
        }
        Debug.Log("Secuencia correcta");
        return true;
    }
}
