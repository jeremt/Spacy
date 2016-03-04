using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public bool IsAvailable = true;

    public void OnDrawGizmos() {
        Gizmos.color = IsAvailable ? Color.green : Color.red;
         Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
