using UnityEngine;
using System.Collections;

public class HomePage : MonoBehaviour {

    void Update() {
        if (!GetComponent<PageTransition>().IsTransitioning()) {
            for (int i = 0; i < 5; ++i) {
                if (InputManager.Instance.GetKeyUp(InputAlias.Submit, i)) {
                    InputManager.Instance.MasterIndex = i;
                    GetComponent<PageTransition>().StartTransition();
                }
            }
        }
	}

}
