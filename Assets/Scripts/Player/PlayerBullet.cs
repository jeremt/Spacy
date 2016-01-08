using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    // API
    public GameObject Explosion;

    [HideInInspector] public int PlayerIndex = -1;

    // Components
    private PlayerSpawner _playerSpawner;

    void Awake() {
        _playerSpawner = GameObject.Find("Level").GetComponent<PlayerSpawner>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player" && collider.GetComponent<Player>().Index != PlayerIndex) {
            Destroy(collider.gameObject);
            GameManager.Instance.GetPlayer(PlayerIndex).NumberOfKills += 1;
            GameManager.Instance.GetPlayer(collider.GetComponent<Player>().Index).NumberOfDeaths += 1;
            _explode();
            _playerSpawner.RespawnPlayer(collider.GetComponent<Player>().Index);
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
