using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    public PatrolState(EnemyController enemy) : base(enemy){}

    private float time;

    private int randomDirectionIndex = 0;
    private int randomMax = Random.Range(1, 6);

    public override void OnStateEnter()
    {
        enemy.agent.speed = enemy.walkSpeed;
    }

    public override void OnStateUpdate()
    {
        // Change the waypoint to a random one after a certain random number of waypoints
        if (randomDirectionIndex >= randomMax)
        {
            enemy.GetRandomWaypoint();
            randomDirectionIndex = 0;
            randomMax = Random.Range(1, 6);
        }

        // Check if the enemy has reached the waypoint
        if (enemy.agent.remainingDistance <= 0.1f)
        {
            // Wait at the waypoint for the specified time
            if (time <= 0)
            {
                time = enemy.waypoints[enemy.waypointIndex].waitTime;
            }
            else
            {
                time -= Time.deltaTime;
            }

            // If the time has elapsed, move to the next waypoint
            if (time <= 0)
            {
                enemy.waypointIndex = (enemy.waypointIndex + 1) % enemy.waypoints.Length;
                enemy.agent.SetDestination(enemy.waypoints[enemy.waypointIndex].waypointTransform.position);

                // Increment the random direction index
                randomDirectionIndex += 1;
            }
        }

        if (enemy.CanSeePlayer())
        {
            // If the player is spotted, change to the spotted state
            enemy.ChangeState(new SpottedState(enemy));
        }
    }

    public override void OnStateExit()
    {

    }
    
}
