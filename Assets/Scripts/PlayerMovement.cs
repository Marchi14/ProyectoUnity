using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 1.9f;
    public float gravityScale = -50f;
    public int saltos = 0;
    public bool jump = true;

    Vector3 moveInput = Vector3.zero;
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        
        if (characterController.isGrounded)
        {
            jump = true;
            saltos = 0;
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);
            moveInput = transform.TransformDirection(moveInput) * walkSpeed;
        }

        
        if (Input.GetButtonDown("Jump") && jump)
        {
            moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            saltos++;
            if (saltos >= 2)
            {
                jump = false;
            }
        }
        
        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }
}