using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public UnityEvent buttonClicked;
    private Vector3 pisseposition;
    [ReadOnly] public AudioSource audioSource;
    private bool pressCheckBool = false;
    [ReadOnly] public bool instrumentPlayingCheckBool = false;
    void Start()
    {
        pisseposition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (transform.position.y < pisseposition.y - transform.lossyScale.y)
        {
            transform.position = new Vector3(transform.position.x, pisseposition.y - transform.lossyScale.y, transform.position.z);
            
            if(!pressCheckBool && !instrumentPlayingCheckBool)
            {
                audioSource.Play();
                pressCheckBool = true;
                buttonClicked.Invoke();
            }
        }

        if (transform.position.y == pisseposition.y)
        {
            pressCheckBool = false;
        }

        if (transform.position.y > pisseposition.y)
            transform.position = pisseposition;
    }
}