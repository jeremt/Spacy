using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {

    // PlayerGun API
    public int InputIndex = 0;
    public float ShootInterval = 0.2f;
    public float BulletSpeed = 5f;
    public Rigidbody2D Bullet;

    // Components
    private Player _player;
    private Animator _animator;

    // PlayerGun internals
    private bool _isShooting;
    private float _currentShootingTime;

    public void Awake() {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }
	
    public void Update() {
        _isShooting = InputManager.Instance.GetKey(InputAlias.Shoot, InputIndex);
        if (InputManager.Instance.GetKeyDown(InputAlias.Shoot, InputIndex)) {
            _currentShootingTime = 0f;
            _isShooting = true;
//            _animator.Play(_animator.GetBool("Running") ? "PlayRunShoot" : "PlayerIdleShoot");
            _shootBullet();
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

    private void _shootBullet() {
        var bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
        if (bulletInstance != null) {
            bulletInstance.velocity = new Vector2(_player.FacingRight ? BulletSpeed : -BulletSpeed, 0);
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
