//======= Copyright (c) ANDALACA Corporation, All rights reserved. ===============
//
// Purpose: Creates foodstep sound based on the players movement.  
//
//=============================================================================

using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    //Audio
    private AudioSource audioSource;
    private AudioClip previousClip;
    private List<AudioClip[]> footstepsSoundTypes = new List<AudioClip[]>();

    //Player distance
    private Vector3 lastPosition;
    private float distanceCovered;
    private float modifier = 0.5f;
    private float currentSpeed;

    //Raycast
    private RaycastHit hit;

    private void Awake()
    {
        //Load Arrays of sounds for footsteps
        footstepsSoundTypes.Insert(0, Resources.LoadAll<AudioClip>("Resource Sounds/Walk Sounds/Grass"));
        footstepsSoundTypes.Insert(1, Resources.LoadAll<AudioClip>("Resource Sounds/Walk Sounds/Floor"));
        footstepsSoundTypes.Insert(2, Resources.LoadAll<AudioClip>("Resource Sounds/Walk Sounds/Sand"));

        //Get audio source
        audioSource = transform.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        CheckForPlayerMovement();
    }

    /// <summary>
    /// Cast a ray downwards and put result in hit. Checks how far the player has moved, if the distance is above a certain amount trigger next sound clip. 
    /// </summary>
    private void CheckForPlayerMovement()
    {
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y+1, transform.position.z), Vector3.down, out hit, 5);
        currentSpeed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;

        if (currentSpeed > 0)
        {
            distanceCovered += (currentSpeed * Time.deltaTime) * modifier;
            if (distanceCovered > 0.008f)
            {
                TriggerNextClip();
                distanceCovered = 0;
            }
        }
    }

    /// <summary>
    /// Get a random clip from array and ensure that no sound effect is playe twice in a row. 
    /// </summary>
    /// <param name="clipArray">The array of clips to choose from depending on the surface that the player is standing on.</param>
    /// <returns></returns>
    private AudioClip GetClipFromArray(AudioClip[] clipArray)
    {
        int attempts = 3;
        AudioClip selectedClip = clipArray[Random.Range(0, clipArray.Length - 1)];

        while (selectedClip == previousClip && attempts > 0)
        {
            selectedClip = clipArray[Random.Range(0, clipArray.Length - 1)];
            attempts--;
        }
        previousClip = selectedClip;
        return selectedClip;
    }

    /// <summary>
    /// Triggers a sound effect based on the tag of the gameobject below them. 
    /// </summary>
    private void TriggerNextClip()
    {
        audioSource.volume = Random.Range(0.1f, 0.4f);
        audioSource.pitch = Random.Range(0.8f, 1.5f);

        //If we are hitting something.
        if (hit.collider != null)
        {
            //What are we hitting.
            switch (hit.collider.tag)
            {
                case "Ground"://This is wrong
                    audioSource.PlayOneShot(GetClipFromArray(footstepsSoundTypes[0]), 1);
                    break;

                case "Grass":
                    audioSource.PlayOneShot(GetClipFromArray(footstepsSoundTypes[0]), 1);
                    break;

                case "Floor":
                    audioSource.PlayOneShot(GetClipFromArray(footstepsSoundTypes[1]), 1);
                    break;

                case "Sand":
                    audioSource.PlayOneShot(GetClipFromArray(footstepsSoundTypes[2]), 1);
                    break;
            }
        }

    }

}
