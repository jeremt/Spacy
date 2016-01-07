using UnityEngine;
using System.Collections;

public class HomePage : MonoBehaviour {

    void Update() {
        if (!GetComponent<PageTransition>().IsTransitioning()) {
            if (InputManager.Instance.GetKeyUp(InputAlias.Start)) {
                GetComponent<PageTransition>().StartTransition();
            }
        }
	}

}
