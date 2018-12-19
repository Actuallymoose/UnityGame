using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100, currentHealth;
    public Slider healthBar;
    public Image damageImage;
    public float flashSpeed = 5f;// speed damage image fades at
    public Color flashColour = Color.red;

    public GameObject playerCam;
    PlayerMove movement;
    PlayerLook looking;

    bool isDead, damaged;

	// Use this for initialization
	void Awake () {
        currentHealth = startingHealth;
        movement = GetComponent<PlayerMove>();
        looking = playerCam.GetComponent<PlayerLook>();
	}
	
	// Update is called once per frame
	void Update () {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
	}

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount; ;

        healthBar.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;

        movement.enabled = false;
        looking.enabled = false;
    }
}
