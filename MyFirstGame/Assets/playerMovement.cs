using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController controller; // For selecting the controller body in Unity
    public float jumpHeight; // Max jump height (used in jump calculations)
    public float speed; // Movement Multiplier (Literally used to Multiply move vector for movement)
    public float jumpGravity; // "Gravity" used for the up of the jump
    public float fallGravity; // "Gravity" used for the down of th jump
    public Transform groundCheck; // Coordinates for the groundCheck Sphere
    public float groundDistance = 0.2f; // Radius for GroundCheck physics sphere
    public LayerMask groundMask; // Layer that checks for "Ground" for Jump
    private Vector3 velocity;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        velocity.y = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the the Player is touching ground every update
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // Make sure the player is on the ground
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f; // changes y velocity to zero
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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * jumpGravity );
        }

        // Moves Controller in Jump Direction (+y) accord to Velocity
        controller.Move(velocity * Time.deltaTime);

        // Applies Jump Gravity to the y Velocity
        if(!isGrounded && (velocity.y > 0))
        {
            velocity.y += jumpGravity * Time.deltaTime;
        }

        // Changing the Jump gravity to Fall gravity
        if(!isGrounded && (velocity.y <= 0) )
        {
            velocity.y += fallGravity * Time.deltaTime;
        }
    }
}
