using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2A : MonoBehaviour {

    public static GameManager2A instance;

    public float vidaPlayer;
    private int dificil = 12;

    public bool recibiendoDaño = false;
    public bool muerto = false;

    private void Awake() {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else  {
            Destroy(gameObject);
        }
    }
    private void Start() {   
        vidaPlayer = 100;
        muerto = false;
    }
    // Método para recibir daño del jugador, se llama desde el script de colisiones del jugador playerHealth
    IEnumerator delay() {
        yield return new WaitForSeconds(2.5f);
        recibiendoDaño = false;
    }
    public void setDificultad(int cantidad) {
        dificil = cantidad;
    }
    public int getDificultad() {  return dificil; }
}