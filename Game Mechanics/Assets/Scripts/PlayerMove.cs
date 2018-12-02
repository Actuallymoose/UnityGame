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
        float timer = 0;

        float horizInput = Input.GetAxis(horizontalInputName);
        float vertInput = Input.GetAxis(verticalInputName);
        Vector3 startVelocity = transform.right * horizInput + transform.forward * vertInput;
        startVelocity *= speed;

        Vector3 airTargetVector = new Vector3();

        if (controller.isGrounded)
        {
            airTargetVector = startVelocity;

            // enables shift only in the forward direction
            if (Input.GetButton(sprintInputName) && Input.GetAxis(verticalInputName) > -0)
            {
                startVelocity *= sprintSpeed;
            }

            // jumps
            if (Input.GetButton(jumpInputName))
            {
                verticalVelocity = jumpForce;
                timer = 0;
            }

            airTargetVector = new Vector3();
        }
        else
        {
            airTargetVector = startVelocity;
            airTargetVector += transform.right * Input.GetAxis(horizontalInputName) * airModifier + transform.forward * Input.GetAxis(verticalInputName) * airModifier;
        }

        timer += Time.deltaTime * 0.5f;
        Debug.Log(timer);

        verticalVelocity -= gravity * Time.deltaTime; // applies gravity

        startVelocity += Vector3.up * verticalVelocity;

        Vector3 finalMove = Vector3.Lerp(startVelocity, airTargetVector, timer);

        controller.Move(finalMove * Time.deltaTime);

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // if moving and on slope - move player down so they can move down slopes without falling
        if ((vertInput != 0 || horizInput != 0) && OnSlope())
        {
            controller.Move(Vector3.down * controller.height / 2 * slopeDownwardForce * Time.deltaTime);
        }
    }

    // returns true if player is on a slope
    bool OnSlope()
    {
        if(!controller.isGrounded)
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
