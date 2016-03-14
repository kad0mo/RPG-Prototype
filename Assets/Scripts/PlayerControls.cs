using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    // Variables
    public float turnSmoothing = 15f; // A smoothing value for turning the player.
    public float speedDampTime = 0.1f; // The damping for the speed parameter
    public float moveSpeed = 10.0f;
    private Animator anim; // Reference to the animator component.

    // Functions
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Cache the inputs.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        MovementManagement(h, v);
    }

    /////////////////////////////////////////////CHARACTER MOVEMENT/////////////////////////////////////////

    void MovementManagement(float horizontal, float vertical)
    {
        // If there is some axis input...
        if (horizontal != 0f || vertical != 0f)
        {

            Vector3 movement =  new Vector3(horizontal, 0.0f, vertical);
            movement = Camera.main.transform.TransformDirection(movement);
            movement.y = 0.0f;

            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
            // ... set the players rotation and set the speed parameter to 5.3f.
            Rotating(horizontal, vertical);
            anim.SetFloat("Speed", 5.3f, speedDampTime, Time.deltaTime);
        }
        else
            // Otherwise set the speed parameter to 0.
            anim.SetFloat("Speed", 0);
    }

    void Rotating(float horizontal, float vertical)
    {
        // Create a new vector of the horizontal and vertical inputs.

        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);

        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
        GetComponent<Rigidbody>().MoveRotation(newRotation);
    }

}