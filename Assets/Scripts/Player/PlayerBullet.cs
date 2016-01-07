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
        } else if (collider.tag == "Wall") {
            _explode();
        }
    }

    private void _explode() {
        var bulletInstance = Instantiate(Explosion, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
        Destroy(gameObject);

    }

}
