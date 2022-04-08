using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportButton : MonoBehaviour
{
    private GameObject player;
    private ToggleMenu menu;

    public HapticImpulse hapticImpulse;
    public UnityEvent buttonClicked;
    [ReadOnly] public AudioSource audioSource;
    public bool pressCheckBool = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        menu = player.GetComponent<ToggleMenu>();

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
                Invoke("CloseMenu", 0.2f);
                pressCheckBool = true;
            }
        }
    }

    private void CloseMenu()
    {
        if (menu.exhibitionInstrumentsMenu.activeSelf == true || menu.playableInstrumentsMenu.activeSelf == true)
        {
            menu.exhibitionInstrumentsMenu.SetActive(false);
            menu.playableInstrumentsMenu.SetActive(false);
            pressCheckBool = false;
        }
    }
}