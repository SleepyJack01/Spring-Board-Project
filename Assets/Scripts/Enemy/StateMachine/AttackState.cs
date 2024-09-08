using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(EnemyController enemy) : base(enemy){}

    bool playerIsHit = false;

    public override void OnStateEnter()
    {
        Vector3 direction = enemy.target+ - enemy.transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, LookRotation, Time.deltaTime * 5f);

        //fires a raycast at the player
        RaycastHit hit;
        Vector3 playerDirection = (enemy.target - enemy.transform.position).normalized;
        if (Physics.Raycast(enemy.transform.position, playerDirection, out hit, enemy.attackRange, enemy.playerLayer))
        {
            Debug.DrawRay(enemy.transform.position, direction * hit.distance, Color.red);
            Debug.Log("Raycast hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Player"))
            {
                playerIsHit = true;
            }
            else
            {
                playerIsHit = false;
            }
        }
    }

    public override void OnStateUpdate()
    {
        if (playerIsHit)
        {
            Debug.Log("Player is hit");
            enemy.ChangeState(new FleeState(enemy));
        }
        else
        {
            enemy.ChangeState(new ChaseState(enemy));
        }
    }

    public override void OnStateExit()
    {
        
    }
}
