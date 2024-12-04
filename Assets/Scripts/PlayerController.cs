using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovementData playerMovementData;
    [SerializeField] private PlayerMovementStatus playerMovementStatus;

    private Rigidbody2D _rb2d;


    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        playerMovementData.moveInput.x = Input.GetAxisRaw("Horizontal");
        playerMovementData.moveInput.y = Input.GetAxisRaw("Vertical");
        Debug.Log(playerMovementData.moveInput);
    }

    private void FixedUpdate()
    {
        Run(1);
    }

    private void Run(float runSpeedLerpFactor)
    {
        var desiredVelocity = playerMovementData.runMaxSpeed * playerMovementData.moveInput.x;
        desiredVelocity = Mathf.Lerp(_rb2d.velocity.x, desiredVelocity, runSpeedLerpFactor);

        var speedGap = desiredVelocity - _rb2d.velocity.x;

        var accelerate = playerMovementData.runAcceleration;
        
        float movement = accelerate * speedGap;
        _rb2d.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }
}