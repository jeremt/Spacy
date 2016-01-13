using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour {

    public float ShieldDuration = 0.15f;
    public float ShieldDurationBonus = 0.5f;
    public float ShieldLagBonus = 0.75f;
    public float ShieldLag = 1f;
    public GameObject Shield;

    [HideInInspector] public bool Active = false;

    private int _inputIndex;
    private float _shieldDuration;
    private float _shieldLag;
    private bool _shieldLagging;
    private GameObject _shieldInstance;

    public void Start() {
        _inputIndex = GameManager.Instance.GetPlayer(GetComponent<Player>().Index).InputIndex;
    }

    public void Update() {
        if (!Active && !_shieldLagging && InputManager.Instance.GetKeyDown(InputAlias.Shield, _inputIndex)) {
            _shieldInstance = Instantiate(Shield, transform.position + Vector3.down * 0.05f, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            _shieldDuration = ShieldDuration;
            _shieldLag = ShieldLag;
            _shieldLagging = true;
            Active = true;
        }
        if (Active) {
            _shieldDuration -= Time.deltaTime;
            if (_shieldDuration <= 0) {
                _shieldDuration = 0;
                Active = false;
                Destroy(_shieldInstance);
            }
            else if (_shieldInstance != null) {
                _shieldInstance.transform.position = transform.position + Vector3.down * 0.05f;
            }
        }
        if (!Active && _shieldLagging) {
            _shieldLag -= Time.deltaTime;
            if (_shieldLag <= 0) {
                _shieldLag = 0;
                _shieldLagging = false;
            }
        }
    }

    public void AwardDeflectBonus() {
        _shieldDuration = Mathf.Max(_shieldDuration, ShieldDurationBonus);
        _shieldLag -= ShieldLagBonus;
    }
}
