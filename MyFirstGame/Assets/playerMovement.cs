using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float jumpHeight;
    public float speed;
    public float gravity;
    public Transform groundCheck;
    public float groundDistance = 0.8f; // Radius for GroundCheck physics sphere
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the the Player is touching ground every update
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // Make sure the player is on the ground
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f; // forces player on ground
        }
        
        // Gets Input for x and z Directions
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Make Input into a vector
        Vector3 move = transform.right * x + transform.forward *z;

        // Moves Contoller using Move Vector multiplied by speed (Time is added to make independent of framerate)
        controller.Move(move * speed * Time.deltaTime);
        
        // If Jump button is pressed then calulate vecolity needed to reach Jump Height
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity );
        }

        // Moves Controller in Jump Direction (+y) accord to Velocity
        controller.Move(velocity * Time.deltaTime);

        // Applies "Gravity" to the y Velocity
        velocity.y += gravity * Time.deltaTime;
    }
}
