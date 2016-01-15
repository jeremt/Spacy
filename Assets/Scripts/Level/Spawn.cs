using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
