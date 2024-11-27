using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxMergeSystem : MonoBehaviour
{
    [SerializeField] private Box boxPrefab;
    
    private HashSet<Box> mergingBoxes = new HashSet<Box>();

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
        Box newBox = CreateMergeBox(spawnPosition, boxA.Number + boxB.Number);
        
        CleanUpBoxes(boxA, boxB);
    }

    private Vector3 CalculateSpawnPosition(Box boxA, Box boxB)
    {
        return (boxA.transform.position + boxB.transform.position) / 2f;
    }

    private Box CreateMergeBox(Vector3 spawnPosition, int newNumber)
    {
        Box newBox = Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
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