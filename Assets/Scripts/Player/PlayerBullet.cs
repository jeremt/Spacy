using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    // API
    public GameObject Explosion;

    [HideInInspector] public int PlayerIndex = -1;

    // Components
    private PlayerSpawner _playerSpawner;
    private Rigidbody2D _rigidbody;

    void Awake() {
        _playerSpawner = GameObject.Find("Level").GetComponent<PlayerSpawner>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player" && collider.GetComponent<Player>().Index != PlayerIndex) {
            if (collider.GetComponent<PlayerShield>().Activated) {
                _rigidbody.velocity = new Vector2(-1 * _rigidbody.velocity.x, _rigidbody.velocity.y);
                PlayerIndex = collider.GetComponent<Player>().Index;
                GetComponent<SpriteRenderer>().color = GameManager.Instance.GetPlayer(PlayerIndex).SkinColor;
            } else {
                Destroy(collider.gameObject);
                GameManager.Instance.GetPlayer(PlayerIndex).NumberOfKills += 1;
                GameManager.Instance.GetPlayer(collider.GetComponent<Player>().Index).NumberOfDeaths += 1;
                _explode();
                _playerSpawner.RespawnPlayer(collider.GetComponent<Player>().Index);
            }
        } else if (collider.tag == "Solid") {
            _explode();
        }
    }

    private void _explode() {
        var explosionInstance = Instantiate(Explosion, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
        explosionInstance.GetComponent<ParticleSystem>().startColor = GameManager.Instance.GetPlayer(PlayerIndex).SkinColor;
        Destroy(gameObject);

    }

}
