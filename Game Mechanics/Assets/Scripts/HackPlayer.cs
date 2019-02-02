using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackPlayer : MonoBehaviour {

    string buttonChoice;
    public PlayerMove moveVariables;
    public PlayerHackResources points;
    public Text speedText, gravityText, jumpText, airControlText, sprintText;

    public float increment = 1f;

    private void Update()
    {
        switch (buttonChoice)
        {
            case "+speed":
                if(points.currentPoints > 0)
                {
                    AlterSpeed(increment);
                    points.AlterPoints(-1);
                }
                break;
            case "-speed":
                if(moveVariables.speed > 1f)
                {
                    AlterSpeed(-increment);
                    points.AlterPoints(1);
                }
                break;
            case "+gravity":
                if(points.currentPoints > 0)
                {
                    AlterGravity(increment);
                    points.AlterPoints(-1);
                }
                break;
            case "-gravity":
                if(moveVariables.gravity > 1f)
                {
                    AlterGravity(-increment);
                    points.AlterPoints(1);
                }
                break;
            case "+jump":
                if(points.currentPoints > 0)
                {
                    AlterJump(increment);
                    points.AlterPoints(-1);
                }
                break;
            case "-jump":
                if(moveVariables.jumpForce > 1f)
                {
                    AlterJump(-increment);
                    points.AlterPoints(1);
                }
                break;
            case "+airMove":
                if(points.currentPoints > 0)
                {
                    AlterAirMove(increment);
                    points.AlterPoints(-1);
                }
                break;
            case "-airMove":
                if(moveVariables.airSpeed > 1f)
                {
                    AlterAirMove(-increment);
                    points.AlterPoints(1);
                }
                break;
            case "+sprint":
                if(points.currentPoints > 0)
                {
                    AlterSprint(increment);
                    points.AlterPoints(-1);
                }
                break;
            case "-sprint":
                if(moveVariables.sprintSpeed > 1f)
                {
                    AlterSprint(-increment);
                    points.AlterPoints(1);
                }
                break;
        }

        UpdateText();
        buttonChoice = "";
    }

    public void ClickLog(string choice)
    {
        buttonChoice = choice;
    }

    void AlterSpeed(float amount)
    {
        moveVariables.speed += amount;
    }

    void AlterGravity(float amount)
    {
        moveVariables.gravity += amount;
    }

    void AlterJump(float amount)
    {
        moveVariables.jumpForce += amount;
    }

    void AlterAirMove(float amount)
    {
        moveVariables.airSpeed += amount;
    }

    void AlterSprint(float amount)
    {
        moveVariables.sprintSpeed += amount;
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
