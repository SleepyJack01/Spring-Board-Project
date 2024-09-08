using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    // Fisher-Yates Shuffle for randomizing the list in place
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        System.Random rng = new System.Random();
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public class EvidenceSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] List<EvidenceData> availableEvidence;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] int totalPointsToWin = 10;
    [SerializeField] float extraPointFactor = 1.2f;
    [SerializeField] float maxPointFactor = 1.6f;
    [SerializeField] private int maxSpawnAttempts = 40;

    private int totalPointsToSpawn;
    private int maxPointsToSpawn;
    private List<GameObject> spawnedEvidence = new List<GameObject>();

    private void Start()
    {
        if (availableEvidence == null || availableEvidence.Count == 0)
        {
            Debug.LogError("No evidence data available for spawning!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points available.");
            return;
        }

        // Calculate target points and max points to spawn
        totalPointsToSpawn = Mathf.CeilToInt(totalPointsToWin * extraPointFactor);
        maxPointsToSpawn = Mathf.CeilToInt(totalPointsToWin * maxPointFactor);

        Debug.Log($"Total Points to Spawn: {totalPointsToSpawn}, Max Points: {maxPointsToSpawn}");

        // Try to spawn evidence until we meet the required points and within the max limit
        bool success = false;
        for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
        {
            // Clean up any previously spawned evidence before trying again
            CleanupSpawnedEvidence();

            success = TrySpawnEvidence();

            if (success)
            {
                Debug.Log($"Successfully spawned evidence after {attempt + 1} attempt(s).");
                break;
            }
        }

        if (!success)
        {
            Debug.LogError($"Failed to spawn evidence within the point range after {maxSpawnAttempts} attempts.");
        }
    }

    private bool TrySpawnEvidence()
    {
        // Shuffle the spawn points to randomize placement
        List<Transform> shuffledSpawnPoints = new List<Transform>(spawnPoints);
        shuffledSpawnPoints.Shuffle();

        int totalSpawnedPoints = 0;
        bool hasNegativeOrZero = false;  // Track if we've spawned any negative or zero-point evidence

        // Randomly select evidence for each spawn point (use exactly one spawn point for each piece of evidence)
        for (int i = 0; i < shuffledSpawnPoints.Count; i++)
        {
            Transform spawnPoint = shuffledSpawnPoints[i];  // Get the next available spawn point
            EvidenceData randomEvidence = availableEvidence[Random.Range(0, availableEvidence.Count)];

            // Properly account for negative points
            totalSpawnedPoints += randomEvidence.evidencePoints;

            if (randomEvidence.evidencePoints <= 0)
            {
                hasNegativeOrZero = true;  // Mark that we've spawned a negative or zero-point item
            }

            // Instantiate the evidence at the current spawn point and track it
            GameObject evidenceInstance = InstantiateEvidence(randomEvidence, spawnPoint);
            spawnedEvidence.Add(evidenceInstance);  // Track the instantiated evidence for cleanup
        }

        // Check if the total spawned points are within the desired range (min to max)
        Debug.Log($"Total Spawned Points: {totalSpawnedPoints}");
        if (totalSpawnedPoints >= totalPointsToSpawn && totalSpawnedPoints <= maxPointsToSpawn && hasNegativeOrZero)
        {
            return true;  // Success: Points are within range, and we have at least one negative or zero-point evidence
        }
        else
        {
            Debug.LogWarning("Points are out of the desired range or no negative/zero evidence. Retrying...");
            return false;  // Retry: points out of range or no negative/zero-point evidence
        }
    }

    // Method to instantiate evidence at a given spawn point
    private GameObject InstantiateEvidence(EvidenceData evidence, Transform spawnPoint)
    {
        GameObject evidenceInstance = Instantiate(evidence.evidencePrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log($"Spawned {evidence.evidenceName} at {spawnPoint.position} with {evidence.evidencePoints} points.");
        return evidenceInstance;  // Return the instantiated object for tracking
    }

    // Method to clean up previously spawned evidence
    private void CleanupSpawnedEvidence()
    {
        Debug.Log("Cleaning up previously spawned evidence...");
        foreach (GameObject evidence in spawnedEvidence)
        {
            if (evidence != null)
            {
                Destroy(evidence);  // Destroy each previously spawned evidence object
            }
        }
        spawnedEvidence.Clear();  // Clear the list after cleaning up
    }
}