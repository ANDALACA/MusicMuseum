using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = new Vector4(0, 0, 255, 10);
        Gizmos.DrawWireCube(this.transform.position, new Vector3(.1f, .001f, .1f));
    }
}
