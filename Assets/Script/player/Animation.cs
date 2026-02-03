using System;
using UnityEngine;


// Script para gestionar las animaciones del jugador
[RequireComponent(typeof(CharacterController))]
public class Animation : MonoBehaviour
{
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;

    /*[Tooltip("velocidad maxima utilizada para normalizar el movimiento")]
    private float velocidadMax = 1f;

    private Vector3 movimientoLocal;*/


    void Start()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarMovimiento();
    }

    // Metodo para actualizar los parametros de movimiento en el Animator
    private void ActualizarMovimiento()
    {
        float Z = PlayerMovement.moveInput.y;
        float X = PlayerMovement.moveInput.x;
        float Y = PlayerMovement.verticalVelocity;

        animator.SetFloat("Z", Z);
        animator.SetFloat("X", X);
        animator.SetFloat("Y", Y);
        animator.SetBool("salto", PlayerMovement.salto);
        animator.SetBool("suelo", characterController.isGrounded);
        //animator.SetBool("tiempo", PlayerMovement.inactivo);
    }

}
