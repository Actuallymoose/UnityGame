using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    enum State {PATROL, CHASE, RETURN} // starts in patrol state
    State state;
    
    public float lookRadius = 10f, slerpSmoothing = 5f;
    float distance;

    Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(target.position, transform.position); // distance between enemy and player

        switch(state)
        {
            case State.PATROL:
                Patrol();
                break;
            case State.CHASE:
                Chase();
                break;
            case State.RETURN:
                Return();
                break;
        }
        
	}

    void Chase()
    {
        agent.SetDestination(target.position);

        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
            Attack();
        }

        if(distance > lookRadius)
        {
            //state = State.RETURN;
            state = State.PATROL;
        }
    }

    void Attack()
    {
        // attack things
    }

    void Patrol()
    {
        if(distance <= lookRadius)
        {
            state = State.CHASE;
        }
    }

    void Return()
    {

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * slerpSmoothing);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
