using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {

    // PlayerGun API
    public float ShootInterval = 0.2f;
    public float BulletSpeed = 0.2f;
    public PlayerBullet Bullet;

    // Components
    private Player _player;
    private Animator _animator;

    // Input internals
    private int _inputIndex;

    // PlayerGun internals
    private bool _isShooting;
    private float _currentShootingTime;

    public void Awake() {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    public void Start() {
        _inputIndex = GameManager.Instance.GetPlayer(_player.Index).InputIndex;
    }
	
    public void Update() {
        _isShooting = InputManager.Instance.GetKey(InputAlias.Shoot, _inputIndex);
        if (InputManager.Instance.GetKeyDown(InputAlias.Shoot, _inputIndex)) {
            _currentShootingTime = 0f;
            _isShooting = true;
//            _animator.Play(_animator.GetBool("Running") ? "PlayRunShoot" : "PlayerIdleShoot");
            ShootBullet();
        }// else if (InputManager.Instance.GetKeyUp(InputAlias.Shoot, InputIndex)) {
//            _isShooting = false;
        //}
//        if (_isShooting) {
//            if (_currentShootingTime > ShootInterval) {
//                _currentShootingTime = 0f;
//                _shootBullet();
//            } else {
//                _currentShootingTime += Time.deltaTime;
//            }
//        }

    }

    private void ShootBullet() {
        var bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as PlayerBullet;
        if (bulletInstance != null) {
            float direction = InputManager.Instance.GetAxis(InputAlias.Vertical, _inputIndex);
            if (Mathf.Abs(direction) < 0.2f) {
                bulletInstance.Speed = new Vector2(_player.FacingRight ? BulletSpeed : -BulletSpeed, 0);
            } else if (direction < 0f) {
                bulletInstance.Speed = new Vector2(_player.FacingRight ? BulletSpeed : -BulletSpeed, BulletSpeed);
            } else if (direction > 0f) {
                bulletInstance.Speed = new Vector2(_player.FacingRight ? BulletSpeed : -BulletSpeed, -BulletSpeed);
            }
            bulletInstance.transform.Translate((_player.FacingRight ? 0.1f : -0.1f), -0.03f, 0f);
            if (!_player.FacingRight) {
                var scale = bulletInstance.transform.localScale;
                scale.x *= -1;
                bulletInstance.transform.localScale = scale;
            }
            bulletInstance.GetComponent<SpriteRenderer>().color = GameManager.Instance.GetPlayer(_player.Index).SkinColor;
            bulletInstance.GetComponent<PlayerBullet>().PlayerIndex = _player.Index;
        }
    }
}
