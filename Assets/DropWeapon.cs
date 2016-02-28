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
            if (Random.Range(0, 2) == 0) {
                collider.GetComponent<PlayerGun>().AddGun(GunType.ShotGun);
            } else {
                collider.GetComponent<PlayerGun>().AddGun(GunType.Rifle);
            }
            Destroy(gameObject);
        }
    }
}
