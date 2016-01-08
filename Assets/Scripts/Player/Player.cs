using UnityEngine;

[RequireComponent(typeof (PlayerController))]
public class Player : MonoBehaviour {

    // Player API
    public int Index = 0;
    public int InputIndex = 0;

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

    // Player movement internals
    private int _jumps;
    private float _gravity;
    private float _jumpVelocity;
    private float _moveAcceleration;
    private Vector3 _velocity;

    public void Awake() {
        _controller = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    public void Start() {
        _gravity = - (2 * JumpHeight) / Mathf.Pow(JumpTimeApex, 2);
        _jumpVelocity = Mathf.Abs(_gravity) * JumpTimeApex;
    }

    public void Update() {
        var input = new Vector2(
            InputManager.Instance.GetAxis(InputAlias.Horizontal, InputIndex),
            InputManager.Instance.GetAxis(InputAlias.Vertical, InputIndex)
        );
        if (_controller.Collisions.Above || _controller.Collisions.Below) {
            _velocity.y = 0;
        }
        if (InputManager.Instance.GetKeyDown(InputAlias.Jump, InputIndex) && _controller.Collisions.Below) {
            _velocity.y = _jumpVelocity;
        }
        if (InputManager.Instance.GetKeyDown(InputAlias.Jump, InputIndex) && !_controller.Collisions.Below && _jumps < NumberOfAirJumps) {
            _velocity.y = _jumpVelocity;
            _jumps += 1;
        }
        if (_controller.Collisions.Below) {
            _jumps = 0;
        }
        _velocity.x = Mathf.SmoothDamp(_velocity.x, input.x * MoveSpeed, ref _moveAcceleration, _controller.Collisions.Below ? AccelerationGrounded : AccelerationAirborne);
        _velocity.y += _gravity * Time.deltaTime;

        if ((FacingRight && _velocity.x < 0) || (!FacingRight && _velocity.x > 0)) {
            _flipDirection();
        }

        _animator.SetFloat("PlayerSpeed", Mathf.Abs(_velocity.x));
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void _flipDirection() {
        FacingRight = !FacingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
