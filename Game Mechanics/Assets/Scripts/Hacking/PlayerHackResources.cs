using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHackResources : MonoBehaviour {

    public int startingPoints = 10, currentPoints, amount = 1;

    public Text hackPoints;

    void Start()
    {
        currentPoints = startingPoints;
        UpdatePointsText();
    }
    
    public void AddPoints()
    {
        currentPoints += amount;
        UpdatePointsText();
    }

    public void TakePoints()
    {
        currentPoints -= amount;
        UpdatePointsText();
    }

    void UpdatePointsText()
    {
        hackPoints.text = "Points: " + currentPoints.ToString();
    }
}
