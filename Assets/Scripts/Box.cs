using System;
using TMPro;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private TMP_Text m_displayNumber;
    BoxMergeSystem m_boxMergeSystem;
    [SerializeField]
    private int m_number;

    private bool m_isMergeable;
    public bool IsMergeable => m_isMergeable;

    public int Number => m_number;

    private void Start()
    {
        m_boxMergeSystem = FindObjectOfType<BoxMergeSystem>();
        m_displayNumber.text = Number.ToString();
        m_isMergeable = true;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        var otherBox = other.gameObject.GetComponent<Box>();
        if (otherBox != null && m_isMergeable && otherBox.IsMergeable)
        {
            Debug.Log("Merge Boxes");
            m_boxMergeSystem.MergeBoxes(this, otherBox);
            m_isMergeable = false;
        }
    }

    public void Initialize(int num)
    {
        m_number = num;
        m_displayNumber.text = Number.ToString();
    }
}