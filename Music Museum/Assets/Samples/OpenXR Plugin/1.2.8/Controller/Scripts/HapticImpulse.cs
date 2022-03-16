using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HapticImpulse : MonoBehaviour
{    
    public void TriggerHaptics(InputDevice inputDevice, float amplitude, float duration)
    {
        inputDevice.SendHapticImpulse(1, amplitude, duration);
    }
}