using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    void OnDrawGizmos() {
//        Gizmos.color = ;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
