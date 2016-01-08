using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PageTransition))]
public class HomePage : MonoBehaviour {

    void Update() {
        if (GetComponent<PageTransition>().IsTransitioning()) {
            return;
        }
        if (InputManager.Instance.GetKeyUp(InputAlias.Start)) {
            GetComponent<PageTransition>().GoNext();
        }
	}

}
