using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInstrumentAudio : MonoBehaviour
{
    public GameObject instrumentObject;

    public void PlayInstrumentSound(GameObject buttonObject)
    {
        StartCoroutine(PlayInstrumentSoundEffect(buttonObject));
    }

    public IEnumerator PlayInstrumentSoundEffect(GameObject buttonObject)
    {
        instrumentObject.GetComponent<ParticleSystem>().Play();
        instrumentObject.GetComponent<AudioSource>().Play();
        buttonObject.GetComponent<ButtonScript>().instrumentPlayingCheckBool = true;
        yield return new WaitForSeconds(instrumentObject.GetComponent<AudioSource>().clip.length);
        instrumentObject.GetComponent<ParticleSystem>().Stop();
        instrumentObject.GetComponent<AudioSource>().Stop();
        buttonObject.GetComponent<ButtonScript>().instrumentPlayingCheckBool = false;
    }
}
