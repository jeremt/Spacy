using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSelector : MonoBehaviour {

    public int Index = 0;
        

    [HideInInspector] public int InputIndex = -1;

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
        if (InputIndex != -1) {
            if (Input.GetAxis(InputIndex + "_Shoot")) { // Cancel
                _deselectInput();
            }
        } else {
            for (int inputIndex = 0; inputIndex < 5; ++inputIndex) {
                if (Input.GetAxis(inputIndex + "_Jump")) { // Submit
                    _selectInput(inputIndex);
                }
            }
        }
	}

    private void _selectInput(int index) {
        Debug.Log("Select Input " + index);
    }

    private void _deselectInput() {
        Debug.Log("Deselect input " + InputIndex);
    }

}
