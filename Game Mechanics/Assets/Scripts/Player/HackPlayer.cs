using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackPlayer : MonoBehaviour {

    string buttonChoice;
    PlayerMove moveVariables;

    public int increment = 1;

    private void Start()
    {
        moveVariables.GetComponent<PlayerMove>();
    }

    private void Update()
    {
        

        switch (buttonChoice)
        {
            case "increaseSpeed":
                moveVariables.speed += increment;
                break;
            case "decreaseSpeed":
                moveVariables.speed -= increment;
                break;
        }
    }

    public void GetButtonChoice(string choice)
    {
        buttonChoice = choice;
    }

}
