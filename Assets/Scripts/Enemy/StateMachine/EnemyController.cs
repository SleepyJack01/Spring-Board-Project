using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct Waypoint
{
    public Transform waypointTransform;
    public float waitTime;
}

public class EnemyController : MonoBehaviour
{
    [Header("State Controls")]
    public EnemyState currentState;
    public Animator animator;

    [Header("Movement Varibles")]
    public float walkSpeed = 3.5f;
    public float alertSpeed = 4.5f;
    public float chaseSpeed = 6.5f;

    [Header("Navigation")]
    public NavMeshAgent agent;
    public Waypoint[] waypoints;
    [HideInInspector]
    public int waypointIndex = 0;
    public float spottedPauseTime = 1.5f;
    public float alertSearchTime = 10f;
    public float updateTargetDelay = 0.5f;
    private float time;

    [Header("Player Detection")]
    [HideInInspector]
    public Transform player;
    private EnemyVision enemyVision;
    [HideInInspector]
    public Vector3 target;
    [HideInInspector]
    public Vector3 lastKnownPosition;
    public bool seenByPlayer;
    public float playerSeenTimer; 

    [Header("Attack Varibles")]
    public float attackRange = 1.5f;
    public float attackDamage = 10.0f; 
    public LayerMask playerLayer;
    public RoboManager playerRef;

    [Header("Animation Varibles")]
    private float xMovement;
    private float zMovement;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyVision = GetComponent<EnemyVision>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        ChangeState(new PatrolState(this));
    }

    
    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = newState;
        currentState.OnStateEnter();
        Debug.Log($"Current state: {currentState.ToString()}");
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }
        UpdateTarget();
        SetAnimatorSpeed();
        UpdateSeenByPlayer(); 
        
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }

    public bool CanSeePlayer()
    {
        if (enemyVision.visibleTargets.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateTarget()
    {
        if (CanSeePlayer())
        {
            time = updateTargetDelay;
            target = player.position;
            lastKnownPosition = player.position;
        }
        else
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                target = lastKnownPosition;
            }
            else
            {
                lastKnownPosition = player.position;
            }
        }
    }

    private void UpdateSeenByPlayer()
    {
        seenByPlayer = playerRef.CanSeeEnemy();
        if (seenByPlayer)
        {
            playerSeenTimer = playerSeenTimer - 4.0f * Time.deltaTime;
        }
        else
            playerSeenTimer = 3.0f;
    }
    public void GetNearestWaypoint()
    {
        float distance = Mathf.Infinity;
        int index = 0;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float newDistance = Vector3.Distance(transform.position, waypoints[i].waypointTransform.position);

            if (newDistance < distance)
            {
                distance = newDistance;
                index = i;
            }
        }

        waypointIndex = index;
    }

    public void GetFurthestWaypoint()
    {
        float distance = 0;
        int index = 0;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float newDistance = Vector3.Distance(transform.position, waypoints[i].waypointTransform.position);

            if (newDistance > distance)
            {
                distance = newDistance;
                index = i;
            }
        }

        waypointIndex = index;
    }

    public void GetRandomWaypoint()
    {
        waypointIndex = Random.Range(0, waypoints.Length);
    }

    private void SetAnimatorSpeed()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        xMovement = localVelocity.x;
        zMovement = localVelocity.z;

        animator.SetFloat("xMovement", xMovement);
        animator.SetFloat("zMovement", zMovement);
    }

    public void EnterAlertState()
    {
        ChangeState(new AlertState(this));
    }

}
