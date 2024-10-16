using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
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
    [SerializeField] int numEvidenceToSpawn = 4;
    [SerializeField] int totalPointsToWin = 6;
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

        // Check the range to ensure we're not exceeding the available spawn points
        if (numEvidenceToSpawn > shuffledSpawnPoints.Count)
        {
            Debug.LogError("numEvidenceToSpawn exceeds shuffled spawn points. Adjusting automatically.");
            numEvidenceToSpawn = shuffledSpawnPoints.Count;
        }

        // Select only the number of spawn points required based on numObjectsToSpawn
        List<Transform> selectedSpawnPoints = shuffledSpawnPoints.GetRange(0, numEvidenceToSpawn);

        int totalSpawnedPoints = 0;
        bool hasNegativeOrZero = false;

        // Loop through selected spawn points, not shuffledSpawnPoints
        for (int i = 0; i < selectedSpawnPoints.Count; i++)
        {
            Transform spawnPoint = selectedSpawnPoints[i];
            EvidenceData randomEvidence = availableEvidence[Random.Range(0, availableEvidence.Count)];

            // Properly account for negative points
            totalSpawnedPoints += randomEvidence.evidencePoints;

            if (randomEvidence.evidencePoints <= 0)
            {
                hasNegativeOrZero = true;  // Mark that we've spawned a negative or zero-point item
            }

            // Instantiate the evidence at the current spawn point and track it
            GameObject evidenceInstance = InstantiateEvidence(randomEvidence, spawnPoint);
            spawnedEvidence.Add(evidenceInstance);
        }

        // Check if the total spawned points are within the desired range (min to max)
        Debug.Log($"Total Spawned Points: {totalSpawnedPoints}");
        if (totalSpawnedPoints >= totalPointsToSpawn && totalSpawnedPoints <= maxPointsToSpawn && hasNegativeOrZero)
        {
            return true;
        }
        else
        {
            Debug.LogWarning("Points are out of the desired range or no negative/zero evidence. Retrying...");
            return false;
        }
    }

    // Method to instantiate evidence at a given spawn point
    private GameObject InstantiateEvidence(EvidenceData evidence, Transform spawnPoint)
    {
        GameObject evidenceInstance = Instantiate(evidence.evidencePrefab, spawnPoint.position, spawnPoint.rotation);
        return evidenceInstance;
    }

    // Method to clean up previously spawned evidence
    private void CleanupSpawnedEvidence()
    {
        foreach (GameObject evidence in spawnedEvidence)
        {
            if (evidence != null)
            {
                Destroy(evidence);
            }
        }
        spawnedEvidence.Clear();
    }
}