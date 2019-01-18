using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Range(0, 10)]
    public float speed = 6f, sprintSpeed = 2f, airSpeed = 3f; // force variables

    public string horizontalInputName, verticalInputName, jumpInputName, sprintInputName; // variables to hold movement keys - see unity explorer

    public float gravity = 14f, jumpForce = 10f; // jump variables
    public float slopeRayLengthMultiplyer, slopeDownwardForce;// slope variables

    private Vector3 jumpVelocity = new Vector3(); // for jumping movement

    CharacterController controller; // character controller object - used to rotate the player instead of camera
    PlayerStamina stamina;

    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
        stamina = GetComponent<PlayerStamina>();
	}
	
	// Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis(horizontalInputName), 0, Input.GetAxis(verticalInputName));
        move = transform.TransformDirection(move);

        if (controller.isGrounded)
        {
            move *= speed;

            // enables shift only in the forward direction
            if (Input.GetButton(sprintInputName) && Input.GetAxis(verticalInputName) > -0 && stamina.hasStam)
            {
                stamina.isSprinting = true;
                move *= sprintSpeed;
            }
            else
            {
                stamina.isSprinting = false;
            }

            // jumps
            if (Input.GetButton(jumpInputName))
            {
                jumpVelocity = move;
                jumpVelocity.y = jumpForce;
            }
            else
            {
                jumpVelocity = new Vector3();
            }
        }
        else
        {
            move *= airSpeed;
        }

        jumpVelocity.y -= gravity * Time.deltaTime; // applies gravity

        controller.Move((move + jumpVelocity) * Time.deltaTime);
       
        // if moving and on slope - move player down so they can move down slopes without falling
        if ((Input.GetAxis(verticalInputName) != 0 || Input.GetAxis(horizontalInputName) != 0) && OnSlope() && !Input.GetButton(jumpInputName))
        {
            controller.Move(Vector3.down * controller.height / 2 * slopeDownwardForce * Time.deltaTime);
        }

    }

    // returns true if player is on a slope
    bool OnSlope()
    {
        if(controller.isGrounded)
        {
            return false;
        }

        RaycastHit hit;

        // charControl.height / 2 - distance from middle of player body to the ground
        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeRayLengthMultiplyer)) // output parameter
        {
            if(hit.normal != Vector3.up)
            {
                return true;
            }
        }

        return false; // default return
    }
}
