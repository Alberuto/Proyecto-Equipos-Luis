using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


// Script para el movimiento del jugador
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("movimiento jugador")]
    // atributos publicos para configurar la velocidad de movimiento y la gravedad
    public float moveSpeed = 8f;
    public float gravity = -9.81f;
    public float verticalVelocity;
    [Header("parametros salto")]
    // atributo publico para gestionar el salto
    public bool salto;
    public float jumpHeight = 3f;

    // parametro para cuando esta inactivo el jugador poner una animacion diferente
    private bool inactivo = false;
    private float timeDelay = 20f;


    private CharacterController characterController;
    [SerializeField] public PlayerHealth playerManager; 
    [SerializeField] public Vector2 moveInput;

    [SerializeField] private AudioSource audioSourceSalto;
    [SerializeField] private AudioSource audioSourcePasos;
    [SerializeField] private int minSpeedSound = 1;


    void Start()
    {
        salto = false;
        // Obtener el componente CharacterController
        characterController = GetComponent<CharacterController>();
        //Debug.Log("movimiento= " + characterController);
    }

    // Metodo para recibir la entrada de movimiento del jugador
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        StopCoroutine("delay");
        inactivo = false;
    }


    void Update()
    {
        if (characterController == null)
            return;
        if (GameManager.instance.recibiendoDaño || GameManager.instance.muerto)
        {
            //Debug.Log("Jugador recibiendo daño o muerto, no se puede mover");
        }
        else
        {
            ControlMovimiento();
            SonidoPasos();
        }

        //Debug.Log("Grounded= " + characterController.isGrounded);

        //StartCoroutine("delay");

    }

    // Metodo para reproducir el sonido de los pasos
    private void SonidoPasos()
    {
        if (audioSourcePasos == null)
            return;
        Vector3 v = characterController.velocity;
        v.y = 0;
        bool andando = characterController.isGrounded && v.magnitude > minSpeedSound;
        if (andando)
        {
            if (!audioSourcePasos.isPlaying)
                audioSourcePasos.Play();
        }
        else if (audioSourcePasos.isPlaying)
            audioSourcePasos.Stop();
    }
    private void OnSalto(InputValue value)
    {
        //Debug.Log("Pulsado boton salto");
        if (value.isPressed)
            if (!salto && characterController.isGrounded)
                salto = true;
        StopCoroutine("delay");
        inactivo = false;
    }

    // Metodo para controlar el movimiento del jugador
    private void ControlMovimiento()
    {
        bool isGrounded = characterController.isGrounded;
        //Reset vertical al tocar suelo
        if (isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        //Movimiento local XZ
        //Debug.Log("moveInput X= " + moveInput.x);
        //Debug.Log("moveInput Y= " + moveInput.y);
        Vector3 localMove = new Vector3(moveInput.x, 0, moveInput.y);

        //convertir de local a mundo
        Vector3 worldMove = transform.TransformDirection(localMove);
        //Debug.Log("worldMove= " + worldMove);

        if (worldMove.sqrMagnitude > 1f)
            worldMove.Normalize();

        Vector3 horizontalVelocity = worldMove * moveSpeed;

        if (isGrounded && salto)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //Debug.Log("verticalVelocity= " + verticalVelocity);
        }
        if (isGrounded)
        {
            salto = false;
        }

        verticalVelocity += gravity * Time.deltaTime;
        horizontalVelocity.y = verticalVelocity;
        //Debug.Log("horizontalVelocity= " + horizontalVelocity);
        characterController.Move(horizontalVelocity * Time.deltaTime);

    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(timeDelay);
        //Debug.LogError("Espera " + timeDelay + " segundos");
        inactivo = true;
    }
}