using UnityEngine;
using System.Collections;

interface Gun {
    float Interval();
    void Shoot(PlayerBullet Bullet);
};

class BasicGun : Gun {
    public float Interval() {
        return 0.5f;
    }

    public void Shoot(PlayerBullet Bullet) {
    }
};

public class PlayerGun : MonoBehaviour {

    // PlayerGun API
    public float ShootInterval = 0.5f;
    public float BulletSpeed = 0.2f;
    public PlayerBullet Bullet;
    public AudioClip ShootAudio;

    // Components
    private Player _player;
    private Animator _animator;

    // Input internals
    private int _inputIndex;
    private AudioSource _audioSource;

    // PlayerGun internals
    private bool _isShooting;
    private bool _shouldShoot;
    private float _currentShootingTime;
    private Gun _gun;

    public void Awake() {
        _gun = new BasicGun();
        _audioSource = GetComponent<AudioSource>();
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _currentShootingTime = _gun.Interval();
        _shouldShoot = false;
        _isShooting = false;
    }

    public void Start() {
        _inputIndex = GameManager.Instance.GetPlayer(_player.Index).InputIndex;
    }
	
    public void Update() {
        if (InputManager.Instance.GetKeyDown(InputAlias.Shoot, _inputIndex) && !_isShooting) {
            _isShooting = true;
            _currentShootingTime = _gun.Interval();
        }
        if (_isShooting) {
            if (_currentShootingTime > _gun.Interval()) {
                _currentShootingTime = 0f;
                if (!InputManager.Instance.GetKey(InputAlias.Shoot, _inputIndex)) {
                    _isShooting = false;
                } else {
                    ShootBullet();
                }
            } else {
                _currentShootingTime += Time.deltaTime;
            }
        }

    }

    private void ShootBullet() {
        _audioSource.PlayOneShot(ShootAudio, 0.2f);
        var bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as PlayerBullet;
        if (bulletInstance != null) {
            float direction = InputManager.Instance.GetAxis(InputAlias.Vertical, _inputIndex);
            if (direction < -0.2f) {
                bulletInstance.transform.Rotate(new Vector3(0, 0, _player.FacingRight ? 45 : -45));
            } else if (direction > 0.2f) {
                bulletInstance.transform.Rotate(new Vector3(0, 0, _player.FacingRight ? -45 : 45));
            }
            bulletInstance.Speed = new Vector2(_player.FacingRight ? BulletSpeed : -BulletSpeed, 0);
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
