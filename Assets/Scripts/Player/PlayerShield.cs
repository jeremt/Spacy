using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour {

    public float RespawnDuration = 1f;
    public float ShieldDuration = 0.1f;
    public GameObject Shield;

    [HideInInspector] public bool Activated = false;

    private GameObject _shieldInstance;
    private int _inputIndex;
    private float _currentTime = 0f;

    void Start() {
        _inputIndex = GameManager.Instance.GetPlayer(GetComponent<Player>().Index).InputIndex;
    }

	void Update() {
        if (_currentTime > 0f) {
            _currentTime -= Time.deltaTime;
        } else if (InputManager.Instance.GetKeyDown(InputAlias.Shield, _inputIndex)) {
            Activated = true;
            _currentTime = RespawnDuration;
            _shieldInstance = Instantiate(Shield, transform.position + Vector3.down * 0.05f, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            StartCoroutine(DesactivateShield());
        }
        if (_shieldInstance != null) {
            _shieldInstance.transform.position = transform.position + Vector3.down * 0.05f;
        }
	}

    public void ResetRespawn() {
        _currentTime = 0f;
    }

    IEnumerator DesactivateShield() {
        yield return new WaitForSeconds(ShieldDuration);
        Destroy(_shieldInstance);
        _shieldInstance = null;
        Activated = false;
    }

}
