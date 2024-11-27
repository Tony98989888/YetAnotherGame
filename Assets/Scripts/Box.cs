using TMPro;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private TMP_Text displayName;
    [SerializeField] private int number;


    [SerializeField] private bool isMergeable = true;


    private BoxMergeSystem boxMergeSystem;
    public bool IsMergeable => isMergeable;


    public int Number => number;

    private void Awake()
    {
        boxMergeSystem = FindObjectOfType<BoxMergeSystem>();
        if (boxMergeSystem == null) Debug.LogError("Box Merge System Not Found");
    }

    private void Start()
    {
        UpdateDisplay();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isMergeable) return;

        var otherBox = other.gameObject.GetComponent<Box>();
        if (otherBox != null && otherBox.isMergeable)
        {
            boxMergeSystem.MergeBoxes(this, otherBox);
            isMergeable = false;
        }
    }

    private void UpdateDisplay()
    {
        displayName.text = Number.ToString();
    }

    public void Initialize(int num)
    {
        number = num;
        displayName.text = Number.ToString();
    }
}