using UnityEngine;
using System.Collections;

public class AutoDestroyParticles : MonoBehaviour {

	void Start() {
        Destroy(gameObject, GetComponent<ParticleSystem>().duration);
	}

}
