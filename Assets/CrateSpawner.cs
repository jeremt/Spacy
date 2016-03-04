using UnityEngine;
using System.Collections;

public class CrateSpawner : MonoBehaviour {

    // API
    public float RespawnTime = 20f;

    // Components
    private Spawner _spawner;

    // Internals
    private float _currentTime;

    void Awake() {
        _spawner = GetComponent<Spawner>();
        _currentTime = RespawnTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (_currentTime <= 0f) {
            if (_spawner.HasSpawnAvailable()) {
                var result = _spawner.SpawnObject();
                result.Key.GetComponent<Crate>().Spawn = result.Value;
                _currentTime = RespawnTime;
            }
        }
        _currentTime -= Time.deltaTime;
	}
}
