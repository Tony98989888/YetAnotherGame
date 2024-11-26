using System.Collections.Generic;
using UnityEngine;

public class BoxMergeSystem : MonoBehaviour
{
    public Box boxPrefab;

    public HashSet<Box> MergingBoxes = new HashSet<Box>();

    private void Start()
    {
    }

    public void MergeBoxes(Box boxA, Box boxB)
    {
        
        MergingBoxes.Add(boxA);
        MergingBoxes.Add(boxB);

        var spawnPosition = (boxA.transform.position + boxB.transform.position) / 2f;
        var newBox = Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
        newBox.Initialize(boxA.Number + boxB.Number);
        MergingBoxes.Remove(boxA);
        MergingBoxes.Remove(boxB);
        Destroy(boxA.gameObject);
        Destroy(boxB.gameObject);
    }
}