using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackPlayer : MonoBehaviour {

    string buttonChoice;
    public PlayerMove moveVariables;
    public PlayerHackResources points;
    public Text speedText;

    public float increment = 1f;

    private void Update()
    {
        switch (buttonChoice)
        {
            case "increaseSpeed":
                if(points.currentPoints > 0)
                {
                    AlterSpeed(increment);
                    points.AlterPoints(-1);
                }
                break;
            case "decreaseSpeed":
                if(moveVariables.speed > 1f)
                {
                    AlterSpeed(-increment);
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

    void UpdateText()
    {
        speedText.text = "Player.speed = " + moveVariables.speed.ToString();
    }
}
