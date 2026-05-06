using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager3 : MonoBehaviour {

    public static GameManager3 instance;

    public float vidaPlayer;
    private int dificil = 12;

    private GameObject playerObject;
    private PlayerMovement player;

    private void Awake() {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        //vidaPlayer = PlayerPrefs.GetFloat("VidaJugador", 100f);
        vidaPlayer = 100;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
        //PlayerPrefs.Save();
    }
    // Método para recibir daño del jugador, se llama desde el script de colisiones del jugador playerHealth
    public void RecibirDamage(int daño) {
        
        if (vidaPlayer <= daño) {
            vidaPlayer = 0;
            player.muerto = true;
            //UIManager.instance.ActualizarVida();
            //Debug.Log("Jugador ha muerto");
            //MySceneManager.instance.LoadScene("GameOver");
            StartCoroutine(GameOverConPausa());
        }
        else {
            vidaPlayer -= daño;
            //UIManager.instance.ActualizarVida();
            Debug.Log("Jugador ha recibido " + daño + " de daño, vida restante: " + vidaPlayer);
        }
        PlayerPrefs.SetFloat("VidaJugador", vidaPlayer);
        PlayerPrefs.Save();
    }
    private IEnumerator GameOverConPausa() {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3f);
        PlayerPrefs.SetInt("Fallo", 1);
        PlayerPrefs.Save();
        string escenaBase = GetBaseLevelSceneName(); // "Nivel1" desde "Nivel1-Defensa"/"Nivel1-Furia"
        Debug.Log($"💀 GAME OVER → Fallo=1 → Cargar {escenaBase}");
        Time.timeScale = 1f; // opcional: devolver el tiempo a normal antes de cambiar
        SceneManager.LoadScene(escenaBase);
    }
    private string GetBaseLevelSceneName() {
        string current = SceneManager.GetActiveScene().name;
        var m = Regex.Match(current, @"^Nivel\d+");
        return m.Success ? m.Value : current;
    }
    public void setDificultad(int cantidad) {
        dificil = cantidad;
    }
    public int getDificultad() {  return dificil; }
    public void reiniciarJuego() {
        vidaPlayer = PlayerPrefs.GetFloat("VidaJugador");
        //MySceneManager.instance.LoadScene("MainMenu");
    }
}
