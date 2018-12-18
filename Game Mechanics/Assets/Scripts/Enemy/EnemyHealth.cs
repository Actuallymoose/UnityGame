using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100, currentHealth;
    public float pushBackAmount = 1f;

    bool isDead;
    new CapsuleCollider collider;
  
	// Use this for initialization
	void Awake () {
        currentHealth = startingHealth;
        collider = GetComponent<CapsuleCollider>();
	}

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;

        transform.position += -transform.forward * pushBackAmount; // minecraft style pushback on hit

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        // turns collider into a trigger so objects can pass through it
        collider.isTrigger = true;

        // destroys enemy after 2 seconds of being dead
        Destroy(gameObject, 2f);
    }
}
