using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTheremin : MonoBehaviour
{
    [ReadOnly] public bool volumeActive;
    [ReadOnly] public bool pitchActive;

    [ReadOnly] public float volumeVal;
    [ReadOnly] public float pitchVal;

    //Synth
    [ReadOnly] public double frequency;
    private double increment;
    private double phase;
    private double sampling_frequency = 48000.0;

    [ReadOnly]public float gain;

    private void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency;

        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            if(gain*Mathf.Sin((float)phase)>=0*gain)
            {
                data[i] = (float)gain * .6f;
            }
            else
            {
                data[i] = (-(float)gain) * .6f;
            }
        
            if(channels == 2)
            {
                data[i + 1] = data[i];
            }

            if(phase > (Mathf.PI * 2))
            {
                phase = 0.0;
            }
        }
    }

    void Update()
    {
        if (volumeActive && pitchActive)
        {
            gain = (((volumeVal - .00001f) * (.5f - .0001f)) / (1 - .0001f)) + .0001f;
            Debug.Log("Volume = " + volumeVal);
            frequency = (((pitchVal - .00001f) * (440f - 50f)) / (1 - .00001f)) + 50f;
            Debug.Log("Pitch = " + pitchVal);
        }
        else gain = 0;


    }
}
