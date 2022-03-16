using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThereminPitch : MonoBehaviour
{
    public PlayTheremin playTheremin;
    public Renderer pitchRangeRenderer;
    private float pitchRange;
    private float distX;
    private float distZ;
    private float distXNorm;
    private float distZNorm;
    
    void Start()
    {
        pitchRange = pitchRangeRenderer.bounds.extents.x;
    }
    private void OnTriggerStay(Collider other)
    {
        distX = other.transform.position.x - this.transform.position.x;
        distZ = other.transform.position.z - this.transform.position.z;
        if (Mathf.Pow(distX, 2) + Mathf.Pow(distZ, 2) < Mathf.Pow(pitchRange,2))
        {
            distXNorm = Mathf.Abs((other.transform.position.x - this.transform.position.x) / (this.transform.position.x+pitchRange - this.transform.position.x));
            distZNorm = Mathf.Abs((other.transform.position.z - this.transform.position.z) / (this.transform.position.z+pitchRange - this.transform.position.z));
            playTheremin.pitchVal = 1 - Mathf.Sqrt(Mathf.Pow(distXNorm, 2) + Mathf.Pow(distZNorm, 2));
        }
        playTheremin.pitchActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playTheremin.pitchActive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Vector4(0, 0, 255, 10);
        pitchRange = pitchRangeRenderer.bounds.extents.x;
        Gizmos.DrawWireCube(pitchRangeRenderer.bounds.center, new Vector3(pitchRange, pitchRangeRenderer.bounds.extents.y, pitchRange)*2);
    }
}
