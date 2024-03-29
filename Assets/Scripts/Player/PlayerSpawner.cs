﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour {

    public Transform[] Spawns;
    public GameObject Player;

    public void RespawnPlayer(int playerIndex) {
        Transform spawnTransform = Spawns[Random.Range(0, Spawns.Length)];
        SpawnPlayer(spawnTransform, playerIndex);
    }

    public void Start() {
        var spawns = new List<Transform>();
        spawns.AddRange(Spawns);
        for (int playerIndex = 0; playerIndex < GameManager.Instance.GetNextPlayerIndex(); ++playerIndex) {
            Transform spawnTransform = spawns[Random.Range(0, spawns.Count)];
            spawns.Remove(spawnTransform);
            SpawnPlayer(spawnTransform, playerIndex);
        }
    }

    private void SpawnPlayer(Transform spawnTransform, int playerIndex) {
        var player = Instantiate(Player, spawnTransform.position, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
        player.transform.parent = transform;
        player.GetComponent<Player>().Index = playerIndex;
    }
}
