using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public int attackableMask;
    public float range = 3f, damage = 50; // same as stopping distance of ai

    public string attackButton;

    Ray ray = new Ray();
    RaycastHit hit;

	// Use this for initialization
	void Awake () {
        attackableMask = LayerMask.GetMask("Attackable");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(attackButton))
        {
            Attack();
        }
	}

    void Attack()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        if(Physics.Raycast(ray, out hit, range, attackableMask))
        {
            EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();

            if(health != null)
            {
                health.TakeDamage(damage);
                Debug.Log("hit enemy");
            }
        }
        else
        {
            Debug.Log("missed enemy");
        }
        
    }
}
