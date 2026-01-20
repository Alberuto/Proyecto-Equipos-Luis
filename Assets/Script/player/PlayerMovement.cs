using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController characterController;

    [SerializeField] private Vector2 moveInput;
    private float verticalVelocity;

    [SerializeField] private AudioSource audioSourceSalto;
    [SerializeField] private AudioSource audioSourcePasos;
    [SerializeField] private int minSpeedSound = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Debug.Log("movimiento= " + characterController);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController == null)
            return;
        ControlMovimiento();
        SonidoPasos();
    }

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


    private void ControlMovimiento()
    {
        bool isGrounded = characterController.isGrounded;
        //Reset vertical al tocar suelo
        if (isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        //Movimiento local XZ
        Debug.Log("moveInput X= " + moveInput.x);
        Debug.Log("moveInput Y= " + moveInput.y);
        Vector3 localMove = new Vector3(moveInput.x, 0, moveInput.y);

        //convertir de local a mundo
        Vector3 worldMove = transform.TransformDirection(localMove);
        Debug.Log("worldMove= " + worldMove);

        if (worldMove.sqrMagnitude > 1f)
            worldMove.Normalize();

        Vector3 horizontalVelocity = worldMove * moveSpeed;
        
    }
}