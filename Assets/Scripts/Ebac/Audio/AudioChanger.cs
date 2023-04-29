using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChanger : MonoBehaviour
{
    public AudioMixer group;
    public string floatParam = "MasterVolume";

    [NaughtyAttributes.Button]
    public void DisableVolume()
    {
        group.SetFloat(floatParam, -80);
    }

    [NaughtyAttributes.Button]
    public void EnableVolume()
    {
        group.SetFloat(floatParam, 0);
    }
}
