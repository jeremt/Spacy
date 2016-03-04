using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    // API
    public Spawn[] Spawns;
    public GameObject Prefab;

    // Checks if there is at least one spawn available.
    public bool HasSpawnAvailable() {
        foreach (var spawn in Spawns) {
            if (spawn.IsAvailable) {
                return true;
            }
        }
        return false;
    }

    // Spawns a game model on an available spawn.
    public KeyValuePair<GameObject, Spawn> SpawnObject() {
        
        var spawn = GetRandomAvailableSpawn();
        var spawnedObject = Instantiate(Prefab, spawn.transform.position, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
        spawnedObject.transform.parent = transform;
        spawn.IsAvailable = false;
        return new KeyValuePair<GameObject, Spawn>(spawnedObject, spawn);
    }

    // Tries to find a spawn that is available. Starts searching from a random index and checks every spawn until
    // finding one.
    private Spawn GetRandomAvailableSpawn() {
        var index = Random.Range(0, Spawns.Length - 1);
        var currentIndex = index;
        while (Spawns[currentIndex].IsAvailable == false && currentIndex < Spawns.Length) {
            currentIndex++;
        }
        while (Spawns[currentIndex].IsAvailable == false && currentIndex >= 0) {
            currentIndex--;
        }
        if (Spawns[currentIndex].IsAvailable == false) {
            return null;
        }
        return Spawns[currentIndex];
    }

}
