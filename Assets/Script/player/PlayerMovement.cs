using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


// Script para el movimiento del jugador
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    // atributos publicos para configurar la velocidad de movimiento y la gravedad
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    private float verticalVelocity;
    

    private CharacterController characterController;

    [SerializeField] public Vector2 moveInput;

    [SerializeField] private AudioSource audioSourceSalto;
    [SerializeField] private AudioSource audioSourcePasos;
    [SerializeField] private int minSpeedSound = 1;


    void Start()
    {
        // Obtener el componente CharacterController
        characterController = GetComponent<CharacterController>();
        Debug.Log("movimiento= " + characterController);
    }

    // Metodo para recibir la entrada de movimiento del jugador
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }


    void Update()
    {
        if (characterController == null)
            return;
        ControlMovimiento();
        SonidoPasos();


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

        verticalVelocity += gravity * Time.deltaTime;
        horizontalVelocity.y = verticalVelocity;

        characterController.Move(horizontalVelocity * Time.deltaTime);

    }
    
}