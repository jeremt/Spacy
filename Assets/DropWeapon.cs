using UnityEngine;
using System.Collections;

public class DropWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            collider.GetComponent<PlayerGun>().AddGun((GunType)Random.Range(1, 3));
            Destroy(gameObject);
        } else if (collider.tag == "Bullet") {
            // TODO
        }
    }
}
