using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PageTransition))]
public class LevelsPage : MonoBehaviour {

    void Update() {
        if (GetComponent<PageTransition>().IsTransitioning()) {
            return;
        }
        if (InputManager.Instance.GetKeyUp(InputAlias.Cancel)) {
            GetComponent<PageTransition>().GoPrevious();
        }
        if (InputManager.Instance.GetKeyUp(InputAlias.Submit)) {
            Application.LoadLevel("Runaway");
        }
    }

}
