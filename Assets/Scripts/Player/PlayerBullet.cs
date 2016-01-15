using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    // API
    public LayerMask LayerMask;
    public Explosion Explosion;
    public Explosion PlayerExplosion;
    public Vector2 Speed = Vector2.right * 0.2f;

    [HideInInspector] public int PlayerIndex = -1;

    // Components
    private PlayerSpawner _playerSpawner;

    public void Awake() {
        _playerSpawner = GameObject.Find("Level").GetComponent<PlayerSpawner>();
    }

    public void Update() {
        var hit = Physics2D.Raycast(transform.position, Speed, Speed.magnitude, LayerMask);
        if (hit) {
            OnHitRender(hit.collider);
        } else {
            transform.Translate(Speed.x, Speed.y, 0);
        }
    }

    public void OnHitRender(Collider2D collider) {
        if (collider.tag == "Player") {
            if (collider.GetComponent<Player>().Index != PlayerIndex) {
                if (collider.GetComponent<PlayerShield>().Active) {
                    collider.GetComponent<PlayerShield>().AwardDeflectBonus();
                    PlayerIndex = collider.GetComponent<Player>().Index;
                    GetComponent<SpriteRenderer>().color = GameManager.Instance.GetPlayer(PlayerIndex).SkinColor;
                    Speed *= -1f;
                } else {
                    var explosionInstance = Instantiate(Explosion, collider.transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Explosion;
                    explosionInstance.Color = GameManager.Instance.GetPlayer(collider.GetComponent<Player>().Index).SkinColor;
                    Destroy(collider.gameObject);
                    GameManager.Instance.GetPlayer(PlayerIndex).NumberOfKills += 1;
                    GameManager.Instance.GetPlayer(collider.GetComponent<Player>().Index).NumberOfDeaths += 1;
                    Explode();
                    _playerSpawner.RespawnPlayer(collider.GetComponent<Player>().Index);
                }
            } else {
                transform.Translate(Speed.x, Speed.y, 0);
            }
        } else if (collider.tag == "Solid") {
            Explode();
        }
    }

    private void Explode() {
        var explosionInstance = Instantiate(Explosion, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Explosion;
        explosionInstance.Color = GameManager.Instance.GetPlayer(PlayerIndex).SkinColor;
        Destroy(gameObject);
    }
}
