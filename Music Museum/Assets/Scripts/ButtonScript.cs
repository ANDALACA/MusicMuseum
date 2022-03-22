using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public UnityEvent buttonClicked;
    private Vector3 position;
    [ReadOnly] public AudioSource audioSource;
    private bool pressCheckBool = false;
    [ReadOnly] public bool instrumentPlayingCheckBool = false;
    void Start()
    {
        position = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (transform.position.y < position.y - transform.lossyScale.y)
        {
            transform.position = new Vector3(transform.position.x, position.y - transform.lossyScale.y, transform.position.z);
            
            if(!pressCheckBool && !instrumentPlayingCheckBool)
            {
                audioSource.Play();
                pressCheckBool = true;
                buttonClicked.Invoke();
            }
        }

        if (transform.position.y == position.y)
        {
            pressCheckBool = false;
        }

        if (transform.position.y > position.y)
            transform.position = position;
    }
}