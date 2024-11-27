using System.Collections.Generic;
using UnityEngine;

public class BoxMergeSystem : MonoBehaviour
{
    [SerializeField] private Box boxPrefab;

    private readonly HashSet<Box> mergingBoxes = new();

    public void MergeBoxes(Box boxA, Box boxB)
    {
        if (boxA == null || boxB == null)
        {
            Debug.Log("Can not merge boxes because boxes is null");
            return;
        }

        if (!mergingBoxes.Add(boxA) || !mergingBoxes.Add(boxB))
        {
            Debug.LogWarning("One or more boxes are already in the merging queue.");
            return;
        }

        var spawnPosition = CalculateSpawnPosition(boxA, boxB);
        var newBox = CreateMergeBox(spawnPosition, boxA.Number + boxB.Number);

        CleanUpBoxes(boxA, boxB);
    }

    private Vector3 CalculateSpawnPosition(Box boxA, Box boxB)
    {
        return (boxA.transform.position + boxB.transform.position) / 2f;
    }

    private Box CreateMergeBox(Vector3 spawnPosition, int newNumber)
    {
        var newBox = Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
        newBox.Initialize(newNumber);
        return newBox;
    }

    private void CleanUpBoxes(Box boxA, Box boxB)
    {
        mergingBoxes.Remove(boxA);
        mergingBoxes.Remove(boxB);

        Destroy(boxA.gameObject);
        Destroy(boxB.gameObject);
    }
}