using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    /*
    // daño jugador
    private int metal = 10;
    private int wagner = 30;
    private int dodecafonico = 60;
    private int ataque;

    // Vida del jugador y enemigos
    private int vidaMax = 100;*/
    public float vidaPlayer;
    private int dificil = 12;
    /*private bool furia = false;
    public int vidaKiko = 30;
    public int vidaCigala = 60;
    public int vidaFary = 100;

    // listas para las notas
    public List<GameObject> notas;
    public List<GameObject> partitura;
    */
    private GameObject playerObject;
    private PlayerMovement player;


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
        vidaPlayer = 100;
        //vidaPlayer = PlayerPrefs.GetFloat("Nivel1VidaJugador", 100f);
        /*
        // Inicializar la vida del enemigo en la UI
        UIManager.instance.ActualizarVidaEnemy(MySceneManager.instance.getEnemyActual());*/
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
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
        else
        {
            vidaPlayer -= daño;
            //UIManager.instance.ActualizarVida();
            Debug.Log("Jugador ha recibido " + daño + " de daño, vida restante: " + vidaPlayer);
        }
    }

    private IEnumerator GameOverConPausa()
    {
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


    public void setDificultad(int cantidad)
    {
        dificil = cantidad;
    }
    public int getDificultad() {  return dificil; }
    public void reiniciarJuego()
    {
        vidaPlayer = PlayerPrefs.GetFloat("Nivel1VidaJugador");
        //MySceneManager.instance.LoadScene("MainMenu");
    }
    /*
    // Métodos para gestionar las notas y la partitura, se llaman desde el script de entrega de notas para actualizar las listas y desde el script de ataque para comprobar si la secuencia es correcta
    public void setNotas(List<GameObject> lista)
    {
        notas = lista;
    }
    public void setPartitura(List<GameObject> lista)
    {
        partitura = lista;
    }
    public void setAtaque(int daño)
    {
        ataque = daño;
    }

    // Método para comprobar si la secuencia de notas entregada por el jugador es correcta,
    // se llama desde el script de entrega de notas cuando se han entregado suficientes notas para activar la secuencia de ataque
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
        inflingirDaño(MySceneManager.instance.getEnemyActual(), ataque);
        return true;
    }

    // Método para inflingir daño al enemigo actual, se llama desde el método secuenciaCorrecta si la secuencia es correcta
    private void inflingirDaño(string enemy, int daño)
    {
        if (enemy == "kiko")
        {
            vidaKiko -= daño;
        }
        if (enemy == "cigala")
        {
            vidaCigala -= daño;
        }
        if (enemy == "fary")
        {
            vidaFary -= daño;
        }
        UIManager.instance.ActualizarVidaEnemy(enemy);
    }
    public bool isFuria()
    {
        return furia;
    }
    public void ModoFuria(bool estado)
    {
        furia = estado;
        //Debug.Log("Modo furia " + estado);
    }*/
}
