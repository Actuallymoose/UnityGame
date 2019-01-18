using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour {

    public float startingStam = 100f, currentStam, timeInterval = 0.5f, stamLoss = 1f, stamRegen = 1f;

    public Slider stamBar;

    public bool hasStam = true, isSprinting = false;

    float timer = 0f;

	// Use this for initialization
	void Awake ()
    {
        currentStam = startingStam;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer > timeInterval)
        {
            // use stamina when sprinting
            if (currentStam > 0 && isSprinting)
            {
                currentStam -= stamLoss;
                stamBar.value = currentStam;
            }
            // regen stamina when not sprinting
            else if (currentStam != startingStam && !isSprinting)
            {
                currentStam += stamRegen;
                stamBar.value = currentStam;
            }

            timer = 0f;
        }
        
        // stops sprinting if out of stamina
        if(currentStam <= 0)
        {
            hasStam = false;
        }
        else
        {
            hasStam = true;
        }

	}
}
