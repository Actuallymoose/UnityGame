using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackPlayer : MonoBehaviour {

    public PlayerMove moveVariables;
    public PlayerHackResources points;
    public Text speedText, gravityText, jumpText, airControlText, sprintText;

    public float increment = 1f;

    private void Update()
    {
        UpdateText();
    }

    public void UpSpeed()
    {
        if (points.currentPoints > 0)
        {
            moveVariables.speed += increment;
            points.TakePoints();
        }
    }

    public void DownSpeed()
    {
        if (moveVariables.speed > 1f)
        {
            moveVariables.speed -= increment;
            points.AddPoints();
        }
    }

    public void UpGravity()
    {
        if(moveVariables.gravity < 25)
        {
            moveVariables.gravity += increment;
            points.AddPoints();
        }
    }

    public void DownGravity()
    {
        if(points.currentPoints > 0)
        {
            moveVariables.gravity -= increment;
            points.TakePoints();
        }
    }

    public void UpJump()
    {
        if(points.currentPoints > 0)
        {
            moveVariables.jumpForce += increment;
            points.TakePoints();
        }
    }

    public void DownJump()
    {
        if(moveVariables.jumpForce > 1f)
        {
            moveVariables.jumpForce -= increment;
            points.AddPoints();
        }  
    }

    public void UpAirMove()
    {
        if(points.currentPoints > 0)
        {
            moveVariables.airSpeed += increment;
            points.TakePoints();
        }
    }

    public void DownAirMove()
    {
        if(moveVariables.airSpeed > 1f)
        {
            moveVariables.airSpeed -= increment;
            points.AddPoints();
        }
    }

    public void UpSprint()
    {
        if(points.currentPoints > 0)
        {
            moveVariables.sprintSpeed += increment;
            points.TakePoints();
        }
    }

    public void DownSprint()
    {
        if(moveVariables.sprintSpeed > 1f)
        {
            moveVariables.sprintSpeed -= increment;
            points.AddPoints();
        }
    }

    void UpdateText()
    {
        speedText.text = "Player.speed = " + moveVariables.speed.ToString();
        gravityText.text = "Player.gravity = " + moveVariables.gravity.ToString();
        jumpText.text = "Player.jumpForce = " + moveVariables.jumpForce.ToString();
        airControlText.text = "Player.airControl = " + moveVariables.airSpeed.ToString();
        sprintText.text = "Player.sprintSpeed = " + moveVariables.sprintSpeed.ToString();
    }
}
