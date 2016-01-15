using UnityEngine;
using System.Collections;

public class AutoDestroyParticles : MonoBehaviour {

    public void Start() {
        Destroy(gameObject, GetComponent<ParticleSystem>().duration);
	}

}
