using System;
using UnityEngine;

public struct PlayerStateInfos {

    public bool Grounded, Running, Jumping, Falling, Crouching;

    public void Reset() { Grounded = Running = Jumping = Falling = Crouching = false; }

    public override string ToString() {
        return String.Format(
            "Grounded: {0}, Running: {1}, Jumping: {2}, Falling: {3}, Crouching: {4}",
            Grounded, Running, Jumping, Falling, Crouching);
    }
}

[RequireComponent(typeof (PlayerController), typeof(BoxCollider2D), typeof(Animator))]
public class Player : MonoBehaviour {

#if UNITY_EDITOR
    public bool CreateDebug = true;
#endif

    // Player API
    public int Index = 0;

    // Player movement constants
    public float MoveSpeed = 1f;
    public float AccelerationAirborne = 0.1f;
    public float AccelerationGrounded = 0.1f;
    public bool FacingRight = true;

    // Player jump constants
    public int NumberOfAirJumps = 1;
    public float JumpHeight = .35f;
    public float JumpTimeApex = .32f;
    public float MaxFallSpeed = -3f;

    // Player collider size and offset
    public Vector2 StandingSize = new Vector2(0.07f, 0.14f);
    public Vector2 CrouchingSize = new Vector2(0.07f, 0.06f);
    public Vector2 StandingOffset = new Vector2(0.015f, -0.03f);
    public Vector2 CrouchingOffset = new Vector2(0.015f, -0.07f);

    [HideInInspector]
    public PlayerStateInfos PlayerState;

    // Components
    private PlayerController _controller;
    private BoxCollider2D _collider;
    private Animator _animator;

    // Player
    private PlayerData _playerData;
    private int _inputIndex;

    // Player movement internals
    private int _jumps;
    private float _gravity;
    private float _jumpVelocity;
    private float _moveAcceleration;
    private Vector3 _velocity;

    public void Awake() {
        _controller = GetComponent<PlayerController>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
#if UNITY_EDITOR
        if (CreateDebug && GameManager.Instance.GetPlayer(Index) == null) {
            GameManager.Instance.SetPlayer(Index, Index);
        }
#endif
    }

    public void Start() {
        _playerData = GameManager.Instance.GetPlayer(Index);
        _inputIndex = _playerData.InputIndex;
        _gravity = - (2 * JumpHeight) / Mathf.Pow(JumpTimeApex, 2);
        _jumpVelocity = Mathf.Abs(_gravity) * JumpTimeApex;
        GetComponent<SpriteRenderer>().color = _playerData.SkinColor;
    }

    public void Update() {
        // Horizontal and Vertical input
        var input = new Vector2(
            InputManager.Instance.GetAxis(InputAlias.Horizontal, _inputIndex),
            InputManager.Instance.GetAxis(InputAlias.Vertical, _inputIndex)
        );
        // Gravity and max fall speed
        if (_controller.Collisions.Above || _controller.Collisions.Below) {
            _velocity.y = 0;
        }
        _velocity.y += _gravity * Time.deltaTime;
        _velocity.y = Math.Max(_velocity.y, MaxFallSpeed);
        // Direction
        if ((FacingRight && _velocity.x < 0) || (!FacingRight && _velocity.x > 0)) {
            FlipDirection();
        }
        // Jump
        if (InputManager.Instance.GetKeyDown(InputAlias.Jump, _inputIndex) && _controller.Collisions.Below) {
            _velocity.y = _jumpVelocity;
        }
        // Air Jump
        if (InputManager.Instance.GetKeyDown(InputAlias.Jump, _inputIndex) && !_controller.Collisions.Below && _jumps < NumberOfAirJumps) {
            _velocity.y = _jumpVelocity;
            _jumps += 1;
        }
        if (_controller.Collisions.Below) {
            _jumps = 0;
        }
        // Crouching
        if (_controller.Collisions.Below && InputManager.Instance.GetKey(InputAlias.Crouch, _inputIndex)) {
            input.x = 0;
            PlayerState.Crouching = true;
        }
        _collider.size = !PlayerState.Crouching ? StandingSize : CrouchingSize;
        _collider.offset = !PlayerState.Crouching ? StandingOffset : CrouchingOffset;
        // Jumpthru
        if (_controller.Collisions.BelowPlatform && InputManager.Instance.GetKey(InputAlias.Crouch, _inputIndex) && InputManager.Instance.GetKeyDown(InputAlias.Jump, _inputIndex)) {
            // TODO: Jumpthru
            _velocity.y = 0;
        }
        // Run / Acceleration / Deceleration
        _velocity.x = Mathf.SmoothDamp(_velocity.x, input.x * MoveSpeed, ref _moveAcceleration, _controller.Collisions.Below ? AccelerationGrounded : AccelerationAirborne);
        // Collisions
        _controller.Move(_velocity * Time.deltaTime);
        // Player state and animations
        UpdateState();
    }

    private void UpdateState() {
        PlayerState.Reset();
        // Player state
        PlayerState.Grounded = _controller.Collisions.Below;
        PlayerState.Running = Mathf.Abs(_velocity.x) > 0.1;
        PlayerState.Jumping = !_controller.Collisions.Below && Math.Sign(_velocity.y) > 0;
        PlayerState.Falling = !_controller.Collisions.Below && Math.Sign(_velocity.y) < 0;
        // Animations
        _animator.SetBool("Running", PlayerState.Running);
        _animator.SetInteger("Jumping", PlayerState.Grounded ? 0 : (PlayerState.Jumping ? 1 : -1));
        _animator.SetBool("Crouching", PlayerState.Crouching);
    }

    private void FlipDirection() {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        FacingRight = !FacingRight;
    }
}