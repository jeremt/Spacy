using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int Index = 0;
    public int InputIndex = 0;
    public float MaxSpeed = 1f;
    public float JumpStrength = 2f;
    public float Acceleration = 10f;
    public int NumberOfJumps = 2;
    public BoxCollider2D FootCollider;

    private string AxisHorizontal { get { return InputIndex + "_Horizontal"; } }
    private string AxisJump { get { return InputIndex + "_Jump"; } }

    private Animator _animator;
    private Rigidbody2D _rigidBody;

    public void Awake() {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() {
        var grounded = IsGrounded();
        var moving = IsMoving();
        _animator.SetBool("Grounded", grounded);
        _animator.SetBool("Walking", grounded && moving);

        // Move
        if (Math.Abs(Input.GetAxis(AxisHorizontal)) > float.Epsilon) {
            _rigidBody.AddForce(Vector2.right * Input.GetAxis(AxisHorizontal) * Acceleration, ForceMode2D.Force);
        }

        // Jump
        if (grounded && Input.GetAxis(AxisJump) > float.Epsilon) {
            _rigidBody.AddForce(Vector2.up * JumpStrength, ForceMode2D.Impulse);
        }

        // Cap speed to max speed
        if (_rigidBody.velocity.x > MaxSpeed) {
            _rigidBody.velocity = new Vector2(MaxSpeed, _rigidBody.velocity.y);
        }
        if (_rigidBody.velocity.x < -MaxSpeed)
        {
            _rigidBody.velocity = new Vector2(-MaxSpeed, _rigidBody.velocity.y);
        }
    }

    public bool IsMoving() {
        return Math.Abs(_rigidBody.velocity.x) > float.Epsilon;
    }

    public bool IsGrounded() {
        return Physics2D.OverlapArea(FootCollider.bounds.min, FootCollider.bounds.max);
    }
}