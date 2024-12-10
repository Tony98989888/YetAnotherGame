using UnityEngine;

[CreateAssetMenu(menuName = "Player Movement Data")]
public class PlayerMovementData : ScriptableObject
{
    [Header("Player Input Data")] public Vector2 moveInput;

    public float runAcceleration;
    public float runDeceleration;

    [Header("Movement Data")] public float runMaxSpeed;
    
    
    [Header("Layer Data")] public LayerMask groundLayer;
    
    [Header("Gravity Data")] public float gravityScale;

    public float fallSpeed;
    
    public float fallAcceleration;

    public float maxFallSpeed;

    public bool jumpBtnTriggered;

    public float jumpVelocity;
}