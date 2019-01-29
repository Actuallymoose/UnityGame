using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackPlayer : MonoBehaviour {

    string buttonChoice;
    public PlayerMove moveVariables;

    public float increment = 1f;

    private void Update()
    {
        switch (buttonChoice)
        {
            case "increaseSpeed":
                moveVariables.speed += increment;
                buttonChoice = "";
                break;
            case "decreaseSpeed":
                if(moveVariables.speed > 1f)
                {
                    moveVariables.speed -= increment;
                }
                buttonChoice = "";
                break;
        }
    }

    public void ClickLog(string choice)
    {
        buttonChoice = choice;
    }

}
