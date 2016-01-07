using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    [HideInInspector] public int PlayerIndex = -1;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player" && collider.GetComponent<Player>().Index != PlayerIndex) {
            Debug.Log(collider.GetComponent<Player>().Index + " -- " + PlayerIndex);
            // Add score to player
            // Kill player
            Destroy(gameObject);
        } else if (collider.tag == "Wall") {
            Debug.Log(collider.tag);
            Destroy(gameObject);
        }
    }

}
