using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class GlobalMusic : MonoBehaviour
{
    public MusicProperties[] properties;

    private void Awake()
    {
       if(GameObject.FindGameObjectsWithTag("Music").Length <= 1)
            DontDestroyOnLoad(this);
        else
            Destroy(this.gameObject);


        foreach (MusicProperties MusicClip in properties)
        {
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.clip = MusicClip.clip;
            audioSource.pitch = MusicClip.stablePitch;
            audioSource.volume = MusicClip.stableVolume;
            MusicClip.source = audioSource;
        }
    }


    public void CustomeudioPlayAudio(string name)
    {
        MusicProperties s = Array.Find(properties, properties => properties.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name+" not exist.");
            return;
        }
        if(s.name == "Calm")
        {
            s.source.loop = true;
        }
        
        s.source.Play();
        
        
    }
    public void CustomaudioChangeData(string name, string pitchorvolume, float number)
    {

        foreach (MusicProperties MusicClip in properties)
        {
            if (MusicClip.name == name)
            {
                switch (pitchorvolume)
                {
                    case "pitch":
                        MusicClip.stablePitch = number;
                        break;
                    case "volume":
                        MusicClip.stableVolume = number;
                        break;
                }
                FinalTouches(MusicClip.source, MusicClip);
                break;
            }
            else
            {
                return;
            }
        }
    }
    /*public void CustomeaudioSlowlyVolumeChange(string name, bool iord, float time)
    {
        foreach (MusicProperties MusicClip in properties)
        {
            if (MusicClip.name == name)
            {
                float fark = MusicClip.stableVolume - MusicClip.volume;
                switch (iord)
                {
                    case true:
                        for(float realTime = time; realTime < MusicClip.stableVolume || time <= 0; realTime -= Time.deltaTime)
                        {
                            MusicClip.volume += Time.deltaTime*fark/time;
                        }
                        break;
                    case false:
                        for (float realTime = time; realTime < MusicClip.stableVolume || time <= 0; realTime -= Time.deltaTime)
                        {
                            MusicClip.volume -= Time.deltaTime * fark / time;
                        }
                        FinalTouches(MusicClip.source, MusicClip);
                        break;    
                }
                break;
            }
            else
            {
                return;
            }
        }
    }*/
    public void FinalTouches(AudioSource audioSource, MusicProperties MusicClip)
    {
        audioSource.pitch = MusicClip.stablePitch;
        audioSource.volume = MusicClip.stableVolume;
    }
}

[System.Serializable]
public class MusicProperties
{
    public string name;
    public AudioClip clip;
    
    [Range(0f,1f)]public float stableVolume;
    [Range(.1f,3f)]public float stablePitch;


    [HideInInspector] public AudioSource source;
}
