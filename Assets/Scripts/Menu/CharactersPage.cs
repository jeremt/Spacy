using UnityEngine;
using System.Collections;

public class CharactersPage : MonoBehaviour {

	void Update() {
        for (int inputIndex = 0; inputIndex < InputManager.NumberOfInputs; ++inputIndex) {
            if (InputManager.Instance.GetKeyUp(InputAlias.Cancel, inputIndex) && GameManager.Instance.IsInputAvailable(inputIndex)) {
                Debug.Log("GO HOME");
            }
        }
	}
}
