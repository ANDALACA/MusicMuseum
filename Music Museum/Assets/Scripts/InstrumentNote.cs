using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentNote : MonoBehaviour
{
    public HapticImpulse hapticImpulse;
    public AudioClip noteToPlay;
    private PlayInstrument playInstrument;
    private void Awake()
    {
        playInstrument = this.transform.GetComponentInParent<PlayInstrument>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Play sound:
        if (other.gameObject.tag == "Hand" || other.gameObject.tag == "GrabInteractable")
                playInstrument.PlaySound(noteToPlay);

        //If hand
        if (other.gameObject.tag == "Hand")
        {
            hapticImpulse.TriggerHaptics(other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, .7f, 0.07f);
        }

        //If grab interactable
        if (other.gameObject.tag == "GrabInteractable")
        {
            //Send impule to all interactors
            foreach (UnityEngine.XR.Interaction.Toolkit.IXRSelectInteractor interactor in other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>().interactorsSelecting)
            {
                hapticImpulse.TriggerHaptics(interactor.transform.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, .7f, 0.07f);
            }
        }
    }
}
