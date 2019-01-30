using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHackResources : MonoBehaviour {

    public int startingPoints = 10, currentPoints;

    public Text hackPoints;

    void Start()
    {
        currentPoints = startingPoints;
        UpdatePointsText();
    }

    public void AlterPoints(int amount)
    {
        currentPoints += amount;
        UpdatePointsText();
    }

    void UpdatePointsText()
    {
        hackPoints.text = "Points: " + currentPoints.ToString();
    }
}
