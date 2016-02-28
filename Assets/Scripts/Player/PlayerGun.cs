using UnityEngine;
using System.Collections;

abstract class Gun {
    public PlayerGun playerGun;
    abstract public float Interval();
    abstract public void Shoot();
    abstract public void Update();
};

class BasicGun : Gun {
    public override float Interval() {
        return 0.4f;
    }

    public override void Shoot() {
        playerGun.ShootBullet();
    }

    public override void Update() {
    }
};

class Rifle : Gun {
    float _currentTime = 0f;
    int _numberOfShots = 0;

    public override float Interval() {
        return 0.6f;
    }

    public override void Shoot() {
        _currentTime = 0f;
        _numberOfShots = 3;
    }

    public override void Update() {
        if (_numberOfShots == 0) {
            return;
        }
        _currentTime += Time.deltaTime;
        if (_currentTime > 0.1f) {
            _currentTime = 0f;
            _numberOfShots--;
            playerGun.ShootBullet();
        }
    }
};

class ShotGun : Gun {
    public override float Interval() {
        return 0.6f;
    }

    public override void Shoot() {
        playerGun.ShootBullet(4f);
        playerGun.ShootBullet();
        playerGun.ShootBullet(-4f);
    }

    public override void Update() {
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
        int n = Random.Range(0, 3);
        if (n == 0) {
            _gun = new Rifle();
        } else if (n == 1) {
            _gun = new ShotGun();
        } else {
            _gun = new BasicGun();
        }
        _gun.playerGun = this;
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
                    _gun.Shoot();
                }
            } else {
                _currentShootingTime += Time.deltaTime;
            }
        }
        _gun.Update();
    }

    public void ShootBullet(float angleOffset = 0f) {
        _audioSource.PlayOneShot(ShootAudio, 0.2f);
        var bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as PlayerBullet;
        if (bulletInstance != null) {
            float direction = InputManager.Instance.GetAxis(InputAlias.Vertical, _inputIndex);
            if (direction < -0.2f) {
                bulletInstance.transform.Rotate(new Vector3(0, 0, _player.FacingRight ? 45 : -45));
            } else if (direction > 0.2f) {
                bulletInstance.transform.Rotate(new Vector3(0, 0, _player.FacingRight ? -45 : 45));
            }
            bulletInstance.transform.Rotate(new Vector3(0, 0, angleOffset));
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
