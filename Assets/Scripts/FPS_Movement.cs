using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Movement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 3.0f;
    Vector3 velocity;
    bool isGrounded;

    bool canMove;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask groundMask;


    private void Start()
    {
        canMove = false;
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);


        // change velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        // get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (canMove)
        {
            // move the player controller
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            
        }

        



        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jumped");
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
        
        // apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    
    }
}
