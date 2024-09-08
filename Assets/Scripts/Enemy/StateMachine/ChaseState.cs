using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(EnemyController enemy) : base(enemy){}

    public override void OnStateEnter()
    {
        enemy.agent.speed = enemy.chaseSpeed;
    }

    public override void OnStateUpdate()
    {
        enemy.agent.SetDestination(enemy.target);


        if (!enemy.CanSeePlayer())
        {
            enemy.agent.SetDestination(enemy.lastKnownPosition);

            if (enemy.agent.remainingDistance <= 0.6f)
            {
                enemy.ChangeState(new AlertState(enemy));
            }
        }
        else if (enemy.agent.remainingDistance <= 1.2f)
        {
            enemy.ChangeState(new AttackState(enemy));
        }
    }

    public override void OnStateExit()
    {
        
    }
}
