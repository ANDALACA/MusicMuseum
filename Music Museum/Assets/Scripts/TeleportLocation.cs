using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLocation : MonoBehaviour
{
    public Mesh arrowMesh;

    private void OnDrawGizmos()
    {
        Gizmos.DrawMesh(arrowMesh, transform.position, transform.rotation * Quaternion.AngleAxis(180, transform.up));
    }
}
