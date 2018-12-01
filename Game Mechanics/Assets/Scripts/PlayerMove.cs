using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Range(0, 10)]
    public float speed = 6f, sprintSpeed = 2f, airModifier = 0.25f; // force variables
    public string horizontalInputName, verticalInputName, jumpInputName, sprintInputName; // variables to hold movement keys - see unity explorer

    public float gravity = 14f, jumpForce = 10f, verticalVelocity;

    [Range(0,10)]
    public float slopeRayLengthMultiplyer, slopeDownwardForce;// slope variables

    bool isJumping;

    CharacterController controller; // character controller object - used to rotate the player instead of camera

    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
    void Update()
    {
        Movement();
    }

   void Movement()
    {
        float vertInput = Input.GetAxis(verticalInputName) * speed;
        float horizInput = Input.GetAxis(horizontalInputName) * speed;

        if (controller.isGrounded)
        {
            // enables shift only in the forward direction
            if (Input.GetButton(sprintInputName) && Input.GetAxis(verticalInputName) > -0)
            {
                vertInput *= sprintSpeed;
            }

            // jumps
            if (Input.GetButton(jumpInputName))
            {
                verticalVelocity = jumpForce;
            }
        }

        verticalVelocity -= gravity * Time.deltaTime; // applies gravity

        Vector3 move = transform.forward * vertInput + Vector3.up * verticalVelocity + transform.right * horizInput;
        controller.Move(move * Time.deltaTime);

        //// if moving and on slope - move player down so they can move down slopes without falling
        //if ((vertInput != 0 || horizInput != 0) && OnSlope())
        //{
        //    controller.Move(Vector3.down * controller.height / 2 * slopeDownwardForce * Time.deltaTime);
        //}
    }

    // returns true if player is on a slope
    bool OnSlope()
    {
        if(isJumping)
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
