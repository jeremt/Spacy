using UnityEngine;

[RequireComponent(typeof (PageTransition))]
public class HomePage : MonoBehaviour {

    void Update() {
        if (GetComponent<PageTransition>().IsTransitioning()) {
            return;
        }
        if (InputManager.Instance.GetKeyDown(InputAlias.Start)) {
           GetComponent<PageTransition>().GoNext();
        }
	}

}
