using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour {

    public Transform[] Spawns;
    public GameObject Player;

	void Start() {
        var spawns = new List<Transform>();
        spawns.AddRange(Spawns);
        for (int playerIndex = 0; playerIndex < GameManager.Instance.GetNextPlayerIndex(); ++playerIndex) {
            Transform spawnTransform = spawns[Random.Range(0, spawns.Count)];
            spawns.Remove(spawnTransform);
            var player = Instantiate(Player, spawnTransform.position, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
            player.GetComponent<SpriteRenderer>().color = GameManager.Instance.GetPlayer(playerIndex).SkinColor;
            var inputIndex = GameManager.Instance.GetPlayer(playerIndex).InputIndex;
            player.GetComponent<PlayerGun>().InputIndex = inputIndex;
            player.GetComponent<Player>().InputIndex = inputIndex;
            player.GetComponent<Player>().Index = playerIndex;
        }
	}

}
