using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : EnemyState
{
    public FleeState(EnemyController enemy) : base(enemy){}

    public override void OnStateEnter()
    {
        enemy.agent.speed = enemy.chaseSpeed;

        enemy.GetFurthestWaypoint();
    }

    public override void OnStateUpdate()
    {
        enemy.agent.SetDestination(enemy.waypoints[enemy.waypointIndex].waypointTransform.position);

        if (enemy.agent.remainingDistance <= 0.1f)
        {
            enemy.ChangeState(new PatrolState(enemy));
        }
    }

    public override void OnStateExit()
    {
        
    }
}
