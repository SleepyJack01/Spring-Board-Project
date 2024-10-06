using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottedState : EnemyState
{
    public SpottedState(EnemyController enemy) : base(enemy){}

    private float time;

    public override void OnStateEnter()
    {
        enemy.agent.speed = 0.0f;

        time = enemy.spottedPauseTime;
    }

    public override void OnStateUpdate()
    {
        if (enemy.seenByPlayer && enemy.playerSeenTimer <= 0)
        {
            enemy.ChangeState(new FleeState(enemy));
        }

        time -= Time.deltaTime;
        
        Vector3 direction = enemy.target+ - enemy.transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, LookRotation, Time.deltaTime * 5f);

        enemy.agent.SetDestination(enemy.target);

        if (!enemy.CanSeePlayer() && time <= 0.0f)
        {
            enemy.ChangeState(new AlertState(enemy));
        }
        else if (enemy.CanSeePlayer() && time <= 0.0f)
        {
            enemy.ChangeState(new ChaseState(enemy));
        }
    }

    public override void OnStateExit()
    {
        
    }
}
