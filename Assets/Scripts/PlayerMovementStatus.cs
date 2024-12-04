using UnityEngine;

[CreateAssetMenu(menuName = "Player Movement Status")]
public class PlayerMovementStatus : ScriptableObject
{
    [Header("Ground Status")] public bool isGrounded;
}