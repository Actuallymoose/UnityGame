using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100, currentHealth, damage = 0, damageCount = 0, count = 0;
    public float flashSpeed = 5f, damageInterval = 0.01f;// speed damage image fades at
    public Color flashColour = Color.red;

    float timer = 0f;

    public Slider healthBar;
    public Image damageImage;

    // used to disable movement on death
    public GameObject playerCam;
    PlayerMove movement;
    PlayerLook looking;

    bool isDead, damaged;

	// Use this for initialization
	void Awake ()
    {
        currentHealth = startingHealth;
        movement = GetComponent<PlayerMove>();
        looking = playerCam.GetComponent<PlayerLook>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;

        InflictDamageOnPlayer();
    }

    // allows for taken damage to go down over time instead of in one block
    void InflictDamageOnPlayer()
    {
        timer += Time.deltaTime;

        if (timer > damageInterval)
        {
            if (count < damageCount)
            {
                currentHealth -= damage;
                healthBar.value = currentHealth;

                count++;
            }

            timer = 0f;
        }

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    // amount of damage taken from an enemy
    public void TakeDamage(int amount)
    {
        damaged = true;
        damage = amount/amount;
        damageCount = amount;
        count = 0;
    }

    void Death()
    {
        isDead = true;

        movement.enabled = false;
        looking.enabled = false;
    }
}
