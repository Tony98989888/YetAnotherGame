using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovementData playerMovementData;
    [SerializeField] private PlayerMovementStatus playerMovementStatus;

    private Rigidbody2D _rb2d;
    private CapsuleCollider2D _capsuleCollider;



    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
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
        
        playerMovementData.jumpBtnTriggered = Input.GetKeyDown(KeyCode.Space);
        Debug.Log(playerMovementData.moveInput);
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        
        Jump();
        Move();
        UpdateGravity();
    }

    private void Jump()
    {
        if (playerMovementStatus.isGrounded && playerMovementData.jumpBtnTriggered)
        {
            var jumpVelocity = playerMovementData.jumpVelocity;
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpVelocity);
        }
    }

    private void Move()
    {
        // 计算目标速度
        float targetSpeedX = playerMovementData.moveInput.x * playerMovementData.runMaxSpeed;

        var acceleration = playerMovementData.moveInput.x == 0
            ? playerMovementData.runDeceleration
            : playerMovementData.runAcceleration;

        // 使用插值平滑过渡到目标速度，避免突变
        float smoothedSpeedX = Mathf.Lerp(
            _rb2d.velocity.x,
            targetSpeedX,
            acceleration * Time.fixedDeltaTime
        );

        // 设置新的速度
        _rb2d.velocity = new Vector2(smoothedSpeedX, _rb2d.velocity.y);

        // 如果速度很小，直接设为0避免抖动
        if (Mathf.Abs(_rb2d.velocity.x) < 0.01f)
        {
            _rb2d.velocity = new Vector2(0, _rb2d.velocity.y);
        }
    }

    private void UpdateGravity()
    {
        if (playerMovementStatus.isGrounded && _rb2d.velocity.y < 0)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, playerMovementData.fallSpeed);
        }
        else
        {
            // Jumping
            var fallAcceleration = playerMovementData.fallAcceleration;
            var velocityY = Mathf.Lerp(_rb2d.velocity.y, playerMovementData.maxFallSpeed, fallAcceleration * Time.fixedDeltaTime);
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, velocityY);
        }

    }

    private void CheckGrounded()
    {
        Bounds colliderBounds = _capsuleCollider.bounds;
        float colliderRadius = _capsuleCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        playerMovementStatus.isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != _capsuleCollider)
                {
                    playerMovementStatus.isGrounded = true;
                    break;
                }
            }
        }

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), playerMovementStatus.isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), playerMovementStatus.isGrounded ? Color.green : Color.red);
    }
}