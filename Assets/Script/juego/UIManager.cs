using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // referencias a los textos de la UI para actualizar el tiempo y la vida
    [SerializeField] private TextMeshProUGUI tiempo;
    [SerializeField] private TextMeshProUGUI vida;
    [SerializeField] private TextMeshProUGUI vidaEnemy;
    // variables para gestionar el estado de la partida
    private bool partida = true;
    private bool finPartida = false;
    public int tiempoPartida = 120; // kiko como 30 segundos estan bien
    public int tiempoActual;

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
    void Start()
    {
        tiempoActual = tiempoPartida;
        // Inicializar el tiempo en la UI y la courutine del temporizador
        tiempo.text = tiempoActual.ToString();
        StopCoroutine("Temporizador");
        StartCoroutine(Temporizador());
        // Inicializar la vida del jugador en la UI
        vida.text = "Vida: " + GameManager.instance.vida.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        // Comprobar si la partida ha terminado por tiempo
        if (finPartida)
        {
            partida = false;
        }
    }
    // Metodo para actualizar la vida del jugador en la UI
    public void ActualizarVida()
    {
        vida.text = "Vida: " + GameManager.instance.vida.ToString();
    }

    // Metodo para actualizar la vida del enemigo en la UI dependiendo de cual sea el enemigo actual que se actualiza segun la escena
    public void ActualizarVidaEnemy(string enemy)
    {
        if(enemy == "kiko")
        {
            vidaEnemy.text = "Vida Kiko: " + GameManager.instance.vidaKiko.ToString();
        }
        if(enemy == "cigala")
        {
            vidaEnemy.text = "Vida Cigala: " + GameManager.instance.vidaCigala.ToString();
        }
        if(enemy == "fary")
        {
            vidaEnemy.text = "Vida Fary: " + GameManager.instance.vidaFary.ToString();
        }
    }

    IEnumerator Temporizador()
    {
        while (partida)
        {
            yield return new WaitForSeconds(1);
            tiempoActual = int.Parse(tiempo.text);
            tiempoActual--;
            tiempo.text = tiempoActual.ToString();
            if (tiempoActual <= 0)
            {
                finPartida = true;
            }
        }
    }
}
