using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThereminVolume : MonoBehaviour
{
    public PlayTheremin playTheremin;
    public Transform volumeMax;
    public Transform volumeMin;

    private void OnTriggerStay(Collider other)
    {
        playTheremin.volumeActive = true;
        playTheremin.volumeVal = (other.gameObject.transform.position.y - volumeMin.position.y) / (volumeMax.position.y - volumeMin.position.y);
    }
    private void OnTriggerExit(Collider other)
    {
        playTheremin.volumeVal = .01f;
        playTheremin.volumeActive = false;
    }
}
