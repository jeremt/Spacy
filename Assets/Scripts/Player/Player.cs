using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PlayerController))]
public class Player : MonoBehaviour {

    // Player inputs
    private string InputHorizontal { get { return InputIndex + "_Horizontal"; } }
    private string InputVertical { get { return InputIndex + "_Vertical"; } }
    private string InputJump { get { return InputIndex + "_Jump"; } }

    // Player controller
    private PlayerController _controller;

    // Player API
    public int Index = 0;
    public int InputIndex = 0;

    // Player movement constants
    public float MoveSpeed = 1.2f;
    public float AccelerationAirborne = 0.1f;
    public float AccelerationGrounded = 0.1f;

    // Player jump constants
    public int NumberOfAirJumps = 1;
    public float JumpHeight = .2f;
    public float JumpTimeApex = .4f;

    // Player movement internals
    private int _jumps;
    private float _gravity;
    private float _jumpVelocity;
    private float _moveAcceleration;
    private Vector3 _velocity;

    public void Awake() { _controller = GetComponent<PlayerController>(); }

    public void Start() {
        _gravity = - (2 * JumpHeight) / Mathf.Pow(JumpTimeApex, 2);
        _jumpVelocity = Mathf.Abs(_gravity) * JumpTimeApex;
    }

    public void Update() {
        var input = new Vector2(Input.GetAxisRaw(InputHorizontal), Input.GetAxisRaw(InputVertical));
        if (_controller.Collisions.above || _controller.Collisions.below) {
            _velocity.y = 0;
        }
        if (Input.GetButtonDown(InputJump) && _controller.Collisions.below) {
            _velocity.y = _jumpVelocity;
        }
        if (Input.GetButtonDown(InputJump) && !_controller.Collisions.below && _jumps < NumberOfAirJumps) {
            _velocity.y = _jumpVelocity;
            _jumps += 1;
        }
        if (_controller.Collisions.below) {
            _jumps = 0;
        }
        _velocity.x = Mathf.SmoothDamp(_velocity.x, input.x * MoveSpeed, ref _moveAcceleration, _controller.Collisions.below ? AccelerationGrounded : AccelerationAirborne);
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

}
