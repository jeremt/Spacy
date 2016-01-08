using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModeSelector : MonoBehaviour {

    // API
    public int CurrentOption = 0;
    public string[] Options;
    public Text Description;

    // Components
    private Image _image;

    // Internals
    private bool _isSelected;

    void Awake() {
        _image = GetComponent<Image>();
    }

    void Start() {
        Description.text = Options[CurrentOption];
    }

    public void Select() {
        _isSelected = true;
        transform.localScale = new Vector3(1, 1, 1);
        _image.color = new Color(0, 0, 0, 70f/255f);
    }

    public void Deselect() {
        _isSelected = false;
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        _image.color = new Color(0f, 0f, 0f, 0f);
    }

    public void ChangeOption() {
        CurrentOption = CurrentOption + 1 == Options.Length ? 0 : CurrentOption + 1;
        Description.text = Options[CurrentOption];
    }

}
