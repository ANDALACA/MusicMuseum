using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumNotes : MonoBehaviour
{
    public HapticImpulse hapticImpulse;
    private PlayInstrument playInstrument;

    public AudioClip softHit;
    public AudioClip mediumSoftHit;
    public AudioClip mediumHit;
    public AudioClip mediumHardHit;
    public AudioClip hardHit;

    private void Awake()
    {
        playInstrument = this.transform.GetComponentInParent<PlayInstrument>();
    }
    private void OnTriggerEnter(Collider other)
    {
        float hitSpeed = other.gameObject.GetComponent<ObjectSpeed>().speed;
        
        //If there is vertical motion
        if (other.gameObject.GetComponent<ObjectSpeed>().velocityY < 0)
        {
            //If hand
            if (other.gameObject.tag == "Hand")
            {
                if (hitSpeed > .01f)
                {
                    playInstrument.PlaySound(softHit);
                    hapticImpulse.TriggerHaptics(other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, .1f, 0.06f);
                }
                if (hitSpeed > 1)
                {
                    playInstrument.PlaySound(mediumSoftHit);
                    hapticImpulse.TriggerHaptics(other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, .2f, 0.07f);
                }
                if (hitSpeed > 2)
                {
                    playInstrument.PlaySound(mediumHit);
                    hapticImpulse.TriggerHaptics(other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, .4f, 0.08f);
                }
                if (hitSpeed > 3)
                {
                    playInstrument.PlaySound(mediumHardHit);
                    hapticImpulse.TriggerHaptics(other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, .7f, 0.09f);
                }
                if (hitSpeed > 5)
                {
                    playInstrument.PlaySound(hardHit);
                    hapticImpulse.TriggerHaptics(other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, 1f, 0.1f);
                }
            }

            //If grab interactable
            if (other.gameObject.tag == "GrabInteractable")
            {
                //Send impule to all interactors
                foreach (UnityEngine.XR.Interaction.Toolkit.IXRSelectInteractor interactor in other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>().interactorsSelecting)
                {
                    if (hitSpeed > .01f)
                    {
                        playInstrument.PlaySound(softHit);
                        hapticImpulse.TriggerHaptics(interactor.transform.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, .1f, 0.06f);
                    }

                    if (hitSpeed > 1)
                    {
                        playInstrument.PlaySound(mediumSoftHit);
                        hapticImpulse.TriggerHaptics(interactor.transform.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, .2f, 0.07f);
                    }

                    if (hitSpeed > 2)
                    {
                        playInstrument.PlaySound(mediumHit);
                        hapticImpulse.TriggerHaptics(interactor.transform.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, .4f, 0.08f);
                    }

                    if (hitSpeed > 3)
                    {
                        playInstrument.PlaySound(mediumHardHit);
                        hapticImpulse.TriggerHaptics(interactor.transform.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, .7f, 0.09f);
                    }

                    if (hitSpeed > 5)
                    {
                        playInstrument.PlaySound(hardHit);
                        hapticImpulse.TriggerHaptics(interactor.transform.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRController>().inputDevice, 1f, 0.1f);
                    }
                }
            }
        }
    }
}
