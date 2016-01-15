using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof (PageTransition))]
public class LevelsPage : MonoBehaviour {

    public void Update() {
        if (GetComponent<PageTransition>().IsTransitioning()) {
            return;
        }
        if (InputManager.Instance.GetKeyUp(InputAlias.Cancel)) {
            GetComponent<PageTransition>().GoPrevious();
        }
        if (InputManager.Instance.GetKeyUp(InputAlias.Submit)) {
            SceneManager.LoadScene("Runaway");
        }
    }

}
