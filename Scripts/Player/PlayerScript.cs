using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The script to handle player movement and jumping
public class PlayerScript : MonoBehaviour
{
    [Header ("Movement Variables")]
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpheight = 3f;

    [Header("Ground Variables")]
    public Transform isGrounded;
    public float groundDistance = 0.4f;
    public LayerMask groundedMask;
    bool grounded;

    [Header("Vector 3 Variables")]
    private Vector3 currentPos;
    Vector3 velocity;

    /*check the players current position and declare grounded, if the player is grounded and hasnt jumped, allow the jump
     Handles the WASD Movement and audio using Horizontal & Vertical*/ 
    void Update()
    {
        CurrentPosition();

        grounded = Physics.CheckSphere(isGrounded.position, groundDistance, groundedMask);

        if(grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
            FindObjectOfType<AudioManager>().Play("Jump");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (Input.GetButtonDown("Horizontal")) {
            FindObjectOfType<AudioManager>().Play("Footsteps");
        }

        if(Input.GetButtonDown("Vertical")) {
            FindObjectOfType<AudioManager>().Play("Footsteps");
        }
    }
    //hold the value for the current position of the player
    public void CurrentPosition()
    {
        currentPos = gameObject.transform.position;
    }
}
