using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    public GameObject Explosion;

    [HideInInspector] public int PlayerIndex = -1;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player" && collider.GetComponent<Player>().Index != PlayerIndex) {
            Debug.Log(collider.GetComponent<Player>().Index + " -- " + PlayerIndex);
            // Add score to player
            // Kill player
            _explode();
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
