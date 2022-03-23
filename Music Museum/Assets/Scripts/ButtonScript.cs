using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public UnityEvent buttonClicked;
    [ReadOnly] public AudioSource audioSource;
    private bool pressCheckBool = false;
    [ReadOnly] public bool instrumentPlayingCheckBool = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Hand")
        {
            GetComponent<Animation>().Play();
            if(!instrumentPlayingCheckBool)
            {
                audioSource.Play();
                buttonClicked.Invoke();
            }
        }
    }
}