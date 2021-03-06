﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyController : MonoBehaviour {

    enum State {PATROL, CHASE, ATTACK, RUN} // RUN state // starts in patrol state
    State state;

    public float lookRadius = 10f, slerpSmoothing = 5f, minDistance = 1f, attackDistance = 3f,
                attackDamage = 20, searchTime = 5f, timeBetweenAttacks = 1.5f, playerTooClose = 3f;

    float distanceToPlayer, searchTimer = 0f, attackTimer = 0f;
    public int currentWaypoint = 0; 

    public Transform[] waypoints;
    Transform target;
    NavMeshAgent agent;

    EnemyHealth enemyHealth;
    PlayerHealth playerHealth;


    // Use this for initialization
    void Awake ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        playerHealth = target.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        distanceToPlayer = Vector3.Distance(target.position, transform.position); // distance between enemy and player

        switch (state)
        {
            case State.PATROL:
                Patrol();
                break;
            case State.CHASE:
                Chase();
                break;
            case State.ATTACK:
                Attack();
                break;
            case State.RUN:
                Run();
                break;
        }
	}

    void Run()
    {
        agent.stoppingDistance = 0;

        if (distanceToPlayer <= playerTooClose)
        {
            Vector3 dirToTarget = transform.position - target.position;
            Vector3 newPos = transform.position + dirToTarget;
            agent.SetDestination(newPos);
        }
        else if (distanceToPlayer <= lookRadius)
        {
            state = State.CHASE;
        }
        else if (distanceToPlayer <= attackDistance)
        {
            state = State.ATTACK;
        }
        else if (distanceToPlayer > lookRadius)
        {
            state = State.PATROL;
        }
    }

    void Chase()
    {
        if(distanceToPlayer <= lookRadius)
        {
            agent.stoppingDistance = attackDistance; // sets stopping distance to default for chasing
            agent.SetDestination(target.position);

            if (distanceToPlayer <= agent.stoppingDistance)
            {
                state = State.ATTACK;
            }
        }
       else if(distanceToPlayer > lookRadius) // when the agent loses the player it will wait for 5 seconds before return to its patrol
        {
            if(searchTimer > searchTime)
            {
                state = State.PATROL;
                searchTimer = 0f;
            }
            else
            {
                searchTimer += Time.deltaTime;
            } 
        }
    }

    void Attack()
    {
        FaceTarget(); // always looks at the player when attacking

        attackTimer += Time.deltaTime; // only attacks if timebetweenattacks has occured

        if(attackTimer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
        {
            attackTimer = 0;

            if(playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        if (distanceToPlayer > attackDistance)
        {
            state = State.CHASE;
        }
        else if (distanceToPlayer > lookRadius)
        {
            state = State.PATROL;
        }
        else if(distanceToPlayer <= playerTooClose)
        {
            state = State.RUN;
        }
    }

    void Patrol()
    {
        agent.stoppingDistance = 0; // so agent walks right up to waypoint

        if(waypoints.Length > 0)
        {
            float distanceToWaypoint = Vector3.Distance(waypoints[currentWaypoint].position, transform.position);

            if (distanceToPlayer <= lookRadius)
            {
                state = State.CHASE;
            }

            if (distanceToWaypoint > minDistance)
            {
                agent.SetDestination(waypoints[currentWaypoint].position);
            }
            else if (currentWaypoint + 1 == waypoints.Length)
            {
                currentWaypoint = 0;
            }
            else
            {
                currentWaypoint++;
            }
        }
        
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * slerpSmoothing);
    }

    // used to draw agent field of view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerTooClose);

        Gizmos.color = Color.red;

        if(waypoints.Length > 0)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.DrawSphere(waypoints[i].position, 0.2f);
            }
        }
    }
}
