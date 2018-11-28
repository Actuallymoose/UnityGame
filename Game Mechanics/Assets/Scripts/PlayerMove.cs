using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Range(0, 10)]
    public float speed, jumpMultiplier, sprintSpeed, airmodifier = 0.5f; // force variables
    public string horizontalInputName, verticalInputName, jumpInputName, sprintInputName; // variables to hold movement keys - see unity explorer

    // jumping variables
    public AnimationCurve jumpFallOff; // handles the curve of the jump
    bool isJumping;

    [Range(0,10)]
    public float slopeRayLengthMultiplyer, slopeDownwardForce;// slope variables

    CharacterController charControl; // character controller object - used to rotate the player instead of camera

    // Use this for initialization
    void Start ()
    {
        charControl = GetComponent<CharacterController>();
        isJumping = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

   void Movement()
    {
        float vertInput = Input.GetAxis(verticalInputName);
        float horizInput = Input.GetAxis(horizontalInputName); // left and right - ad
        
        Vector3 move = transform.forward * vertInput + transform.right * horizInput;
        move *= speed;

        // enables shift only in the forward direction
        if (Input.GetButton(sprintInputName) && Input.GetAxis(verticalInputName) > -0)
        {
            move *= sprintSpeed;
        }

        charControl.SimpleMove(move); // moves the player according to direction vectors - applies time.deltaTime

        // if moving and on slope - move player down so they can move down slopes without falling
        if((vertInput != 0 || horizInput != 0) && OnSlope())
        {
            charControl.Move(Vector3.down * charControl.height / 2 * slopeDownwardForce * Time.deltaTime);
        }
    }

    // decides when the player wants to jump
    void Jump()
    {
        if (Input.GetButton(jumpInputName) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    // performs the actual jump animation
    IEnumerator JumpEvent()
    {
        charControl.slopeLimit = 90.0f; // stops wierd clipping issues when jumping against obstacles
        float timeInAir = 0.0f;

        // loops until player hits roof or ground - causes the player to jump in an arc
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);

            float horizInput = Input.GetAxis(horizontalInputName) * speed * airmodifier; // left and right - ad
            Vector3 move = transform.right * horizInput + Vector3.up * jumpForce * jumpMultiplier;

            charControl.Move(move * Time.deltaTime);
            timeInAir += Time.deltaTime;

            yield return null;
        } while (!charControl.isGrounded && charControl.collisionFlags != CollisionFlags.Above);

        charControl.slopeLimit = 45.0f; // as above
        isJumping = false;
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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, charControl.height / 2 * slopeRayLengthMultiplyer)) // output parameter
        {
            if(hit.normal != Vector3.up)
            {
                return true;
            }
        }

        return false; // default return
    }
}
