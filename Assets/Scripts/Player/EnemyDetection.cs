using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float heightTolerance = 1f;
    [SerializeField] private LayerMask enemyLayer;
    private Transform player;

    [SerializeField] private TMPro.TextMeshProUGUI warningText;
    [SerializeField] private AudioClip[] enemyWarningSounds;
    private AudioSource audioSource;

    private bool detectionWarning = false;
    private float warningInterval = 0.5f;
    private float warningTimer = 0f;

    private void Start() 
    {
        player = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        DetectEnemies();    
    }
    
    private void DetectEnemies()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        bool enemyDetected = false;

        foreach (Collider enemy in enemiesInRange)
        {
            Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
            Vector3 enemyPos = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
            float horizontalDistance = Vector3.Distance(playerPos, enemyPos);

            float heightDifference = Mathf.Abs(player.position.y - enemy.transform.position.y);

            if (horizontalDistance <= detectionRadius && heightDifference <= heightTolerance)
            {
                enemyDetected = true;
                
                if (!detectionWarning)
                {
                    audioSource.PlayOneShot(enemyWarningSounds[Random.Range(0, enemyWarningSounds.Length)]);
                    detectionWarning = true;
                }
                FlashWarning();
                break;
            }  
        }

        if(!enemyDetected && detectionWarning)
        {
            warningText.enabled = false;
            detectionWarning = false;
        }
    }

    private void FlashWarning()
    {
        if (Time.time >= warningTimer)
        {
            warningText.enabled = !warningText.enabled;
            warningTimer = Time.time + warningInterval;
        }
    }
}
