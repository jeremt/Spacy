using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract class Gun { // TODO add ammo system for guns
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

public enum GunType {
    BasicGun,
    ShotGun,
    Rifle,
    Grenade
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
    private IDictionary<GunType, Gun> _guns = new Dictionary<GunType, Gun>();
    private int _gunIndex = 0;

    private Gun currentGun { get { return _guns[(GunType)_gunIndex]; } }

    public void Awake() {
        AddGun(0);
        _audioSource = GetComponent<AudioSource>();
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _currentShootingTime = currentGun.Interval();
        _shouldShoot = false;
        _isShooting = false;
    }

    public void Start() {
        _inputIndex = GameManager.Instance.GetPlayer(_player.Index).InputIndex;
    }
	
    public void Update() {
        if (InputManager.Instance.GetKeyDown(InputAlias.Shoot, _inputIndex) && !_isShooting) {
            _isShooting = true;
            _currentShootingTime = currentGun.Interval();
        }
        if (InputManager.Instance.GetKeyDown(InputAlias.Crouch, _inputIndex)) {
            NextGun();
        }
        if (_isShooting) {
            if (_currentShootingTime > currentGun.Interval()) {
                _currentShootingTime = 0f;
                if (!InputManager.Instance.GetKey(InputAlias.Shoot, _inputIndex)) {
                    _isShooting = false;
                } else {
                    currentGun.Shoot();
                }
            } else {
                _currentShootingTime += Time.deltaTime;
            }
        }
        currentGun.Update();
    }

    private void NextGun() { // TODO skip guns with no ammo
        _gunIndex++;
        if (_gunIndex == _guns.Count) {
            _gunIndex = 0;
        }
    }

    public void AddGun(GunType type) { // TODO if gun already there increase ammo
        if (type == GunType.BasicGun) {
            _guns[type] = new BasicGun();
        } else if (type == GunType.ShotGun) {
            _guns[type] = new ShotGun();
        } else {
            _guns[type] = new Rifle();
        }
        _guns[type].playerGun = this;
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
