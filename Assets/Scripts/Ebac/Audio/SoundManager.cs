using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Ebac.Core.Singleton;

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}
public enum SFXType
{
    NONE,
    TYPE_01,
    TYPE_02,
    TYPE_03
}

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;
    public AudioSource musicSource;

    public AudioMixer group;
    public string floatParam = "MasterVolume";

    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return  musicSetups.Find(i => i.musicType == musicType);
    }

    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);
    }

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

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audioClip;
}

