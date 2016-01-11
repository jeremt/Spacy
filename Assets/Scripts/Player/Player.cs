using System;
using UnityEngine;

[RequireComponent(typeof (PlayerController))]
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

    // Components
    private PlayerController _controller;
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
        var input = new Vector2(
            InputManager.Instance.GetAxis(InputAlias.Horizontal, _inputIndex),
            InputManager.Instance.GetAxis(InputAlias.Vertical, _inputIndex)
        );
        if (_controller.Collisions.Above || _controller.Collisions.Below) {
            _velocity.y = 0;
        }
        if (InputManager.Instance.GetKeyDown(InputAlias.Jump, _inputIndex) && _controller.Collisions.Below) {
            _velocity.y = _jumpVelocity;
        }
        if (InputManager.Instance.GetKeyDown(InputAlias.Jump, _inputIndex) && !_controller.Collisions.Below && _jumps < NumberOfAirJumps) {
            _velocity.y = _jumpVelocity;
            _jumps += 1;
        }
        if (_controller.Collisions.Below) {
            _jumps = 0;
        }
        _velocity.x = Mathf.SmoothDamp(_velocity.x, input.x * MoveSpeed, ref _moveAcceleration, _controller.Collisions.Below ? AccelerationGrounded : AccelerationAirborne);
        _velocity.y += _gravity * Time.deltaTime;
        if ((FacingRight && _velocity.x < 0) || (!FacingRight && _velocity.x > 0)) {
            FlipDirection();
        }
        _controller.Move(_velocity * Time.deltaTime);
        UpdateAnimations();
    }

    private void UpdateAnimations() {
        _animator.SetBool("Running", Mathf.Abs(_velocity.x) > 0.1);
        _animator.SetInteger("Jumping", _controller.Collisions.Below ? 0 : Math.Sign(_velocity.y));
    }

    private void FlipDirection() {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        FacingRight = !FacingRight;
    }

}
