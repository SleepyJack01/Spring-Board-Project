using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(EnemyController enemy) : base(enemy){}

    bool playerIsHit = false;

    public override void OnStateEnter()
    {
        RotateTowardsPlayer();
        FireRaycastAtPlayer();
    }

    public override void OnStateUpdate()
    {
        RotateTowardsPlayer();
        FireRaycastAtPlayer();
        if (playerIsHit)
        {
            Debug.Log("Player is hit");
            enemy.ChangeState(new FleeState(enemy));
        }
        else
        {
            Debug.Log("Player is not hit");
            enemy.ChangeState(new ChaseState(enemy));
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = enemy.target - enemy.transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, LookRotation, Time.deltaTime * 5f);
    }

    void FireRaycastAtPlayer()
    {
        RaycastHit hit;

        Vector3 playerDirection = new Vector3(enemy.target.x - enemy.transform.position.x, 0, enemy.target.z - enemy.transform.position.z).normalized;

        if (Physics.Raycast(enemy.transform.position + Vector3.up * 0.5f, playerDirection, out hit, enemy.attackRange, enemy.playerLayer))
        {
            Debug.DrawRay(enemy.transform.position + Vector3.up * 0.5f, playerDirection * hit.distance, Color.red);
            Debug.Log("Raycast hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Player"))
            {
                playerIsHit = true;
                enemy.playerRef.RobotHit(enemy.attackDamage);
            }
            else
            {
                playerIsHit = false;
            }
        }
        else
        {
            Debug.Log("Raycast missed.");
        }
    }

    public override void OnStateExit()
    {
        
    }
}
