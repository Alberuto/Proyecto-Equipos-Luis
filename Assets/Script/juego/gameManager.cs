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
            recibiendoDaño = true;
            StopCoroutine("delay");
            StartCoroutine(delay());
        }
    }


    // Coroutine para manejar el tiempo de invulnerabilidad
    IEnumerator delay()
    {
        yield return new WaitForSeconds(2.5f);
        recibiendoDaño = false;
    }
}
