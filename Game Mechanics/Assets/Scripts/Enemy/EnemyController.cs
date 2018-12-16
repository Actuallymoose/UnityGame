using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    /// <todo>
    /// implement search so that when ai loses sight of target it stays and looks around for a few seconds
    /// </todo>

    enum State {PATROL, CHASE, RETURN} // starts in patrol state
    State state;

    public float lookRadius = 10f, slerpSmoothing = 5f, minDistance = 1f, stoppingDistance = 3f;
    float distanceToPlayer;
    public int currentPoint = 0;

    public Transform[] waypoints;
    public Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        distanceToPlayer = Vector3.Distance(target.position, transform.position); // distance between enemy and player

        switch (state)
        {
            case State.PATROL:
                Patrol();
                break;
            case State.CHASE:
                Chase();
                break;
            case State.RETURN:
                //Return();
                break;
        }
        
	}

    void Chase()
    {
        agent.stoppingDistance = stoppingDistance; // sets stopping distance to default for chasing
        agent.SetDestination(target.position);

        if (distanceToPlayer <= agent.stoppingDistance)
        {
            FaceTarget();
            //Attack();
        }

        if(distanceToPlayer > lookRadius)
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
        agent.stoppingDistance = 0; // so agent walks right up to waypoint

        float distanceToWaypoint = Vector3.Distance(waypoints[currentPoint].position, transform.position);

        if (distanceToPlayer <= lookRadius)
        {
            state = State.CHASE;
        }

        if(distanceToWaypoint > minDistance)
        {
            agent.SetDestination(waypoints[currentPoint].position);
        }
        else if(currentPoint + 1 == waypoints.Length)
        {
            currentPoint = 0;
        }
        else
        {
            currentPoint++;
        }
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
