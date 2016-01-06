using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PlayerController))]
public class Player : MonoBehaviour {

    private PlayerController _controller;

    // Player API
    public int Index = 0;
    public int InputIndex = 0;

    // Player movement constants
    public float MoveSpeed = 6f;
    public float JumpHeight = .2f;
    public float JumpTimeApex = .4f;
    public float AccelerationAirborne = 0.1f;
    public float AccelerationGrounded = 0.1f;

    // Player movement internals
    private float _gravity = -20;
    private float _jumpVelocity = 8;
    private float _moveAcceleration;
    private Vector3 _velocity;

    public void Awake() { _controller = GetComponent<PlayerController>(); }

    public void Start() {
        _gravity = - (2 * JumpHeight) / Mathf.Pow(JumpTimeApex, 2);
        _jumpVelocity = Mathf.Abs(_gravity) * JumpTimeApex;
    }

    public void Update() {
        var input = new Vector2(Input.GetAxisRaw("0_Horizontal"), Input.GetAxisRaw("0_Vertical"));
        if (_controller.Collisions.above || _controller.Collisions.below) {
            _velocity.y = 0;
        }
        if (_controller.Collisions.below && Input.GetAxis("0_Jump") > 0) {
            _velocity.y = _jumpVelocity;
        }
        var targetVelocityX = input.x * MoveSpeed;
        _velocity.x = Mathf.SmoothDamp(_velocity.x, targetVelocityX, ref _moveAcceleration, _controller.Collisions.below ? AccelerationGrounded : AccelerationAirborne);
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

}
