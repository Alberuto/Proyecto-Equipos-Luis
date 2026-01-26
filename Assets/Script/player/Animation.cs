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

        animator.SetFloat("Z", Z);
        animator.SetFloat("X", X);
    }

}
