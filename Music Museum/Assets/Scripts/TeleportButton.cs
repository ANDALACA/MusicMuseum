using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportButton : MonoBehaviour
{
    public HapticImpulse hapticImpulse;
    public UnityEvent buttonClicked;
    [ReadOnly] public AudioSource audioSource;
    public bool pressCheckBool = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Hand")
        {
            hapticImpulse.TriggerHaptics(col.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, 1f, 0.07f);
            GetComponent<Animation>().Play();
            if(!pressCheckBool)
            {
                audioSource.Play();
                buttonClicked.Invoke();
                pressCheckBool = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Hand")
        {
            if(pressCheckBool)
                pressCheckBool = false;
        }
    }
}