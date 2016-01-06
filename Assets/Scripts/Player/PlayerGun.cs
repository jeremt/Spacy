using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {

    public int InputIndex = 0;
    public float ShootInterval = 0.2f;
    public float BulletSpeed = 5f;
    public Rigidbody2D Bullet;

    private bool _isShooting = false;
    private float _currentShootingTime = 0f;

    private string AxisShoot { get { return InputIndex + "_Shoot"; } }
	
    public void Update() {
        _isShooting = Input.GetAxis(AxisShoot) > float.Epsilon;
        if (Input.GetButtonDown(AxisShoot)) {
            _currentShootingTime = 0f;
            _isShooting = true;
            _shootBullet();
        } else if (Input.GetButtonUp(AxisShoot)) {
            _isShooting = false;
        }
        if (_isShooting) {
            if (_currentShootingTime > ShootInterval) {
                _currentShootingTime = 0f;
                _shootBullet();
            } else {
                _currentShootingTime += Time.deltaTime;
            }
        }

    }

    private void _shootBullet() {
        Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
        bulletInstance.velocity = new Vector2(BulletSpeed, 0);
    }

}
