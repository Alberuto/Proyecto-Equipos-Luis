using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class toroMovement : MonoBehaviour
{
    // player para la ubicacion del jugador
    [SerializeField] public PlayerMovement player;
    // target para la posicion del jugador
    private Vector3 target;
    // toro para la posicion del toro
    private Vector3 toro;
    [Header("spawn toro")]
    // posicion inicial del toro
    public Transform posicionInicial;
    // velocidad del toro
    public float speed = 3.0f;
    [Header("vida util toro")]
    // tiempo de vida del toro antes de entrar en rage
    public float tiempoVidaToro = 10.0f;
    // tiempo de rage del toro antes de desaparecer
    public float tiempoRageToro = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // se ubica en la posicion inicial el toro
        transform.position = posicionInicial.position;
        // invierte la rotacion del toro en el eje y por que si no te sigue de culo xd
        Vector3 rot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(rot.x, -rot.y, rot.z); // Invierte eje Y
        // inicia la corrutina de vida del toro
        StartCoroutine(vidaToro());
    }

    // Update is called once per frame
    void Update()
    {
        // mueve el toro hacia la posicion del jugador
        toro = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.position = Vector3.MoveTowards(toro, target, speed * Time.deltaTime);
        //transform.rotation = Quaternion.Inverse(transform.rotation);
        transform.LookAt(target);
        transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + 180, 0);

    }

    // Corrutina para manejar la vida del toro
    IEnumerator vidaToro()
    {
        // espera el tiempo de vida del toro
        yield return new WaitForSeconds(tiempoVidaToro);
        // aumenta la velocidad del toro y espera el tiempo de rage antes de destruir el toro
        speed = speed + 2;
        yield return new WaitForSeconds(tiempoRageToro);
        Destroy(gameObject);
    }
}
