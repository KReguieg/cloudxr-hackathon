using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public float activateAutomaticallyAfter = -1;
    public AudioClip[] audioClips;
    public GameObject objectWithAudioSource;
    public float volumeScale = 1.0f;
    public float minPitchModifier = 0;
    public float maxPitchModifier = 0f;

    [HideInInspector]
    public float startPitch;
    [HideInInspector]
    public AudioSource audioSource;

    public AudioMixerGroup mixerGroupIfNoGameObjectSelected;
}


public class OneShotter : MonoBehaviour
{
    public float selfKillAfter = -1;
    public bool dontDestroyOnLoad = false;
    public float deactivatedAtStart = 0.2f;

    [HideInInspector]
    public bool isPlaying = false;

    public AudioSource defaultAudioSource;

    [Space(20)]
    public Sound[] sounds;

    void Start()
    {
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        deactivatedAtStart -= Time.deltaTime;

        if (selfKillAfter > 0)
        {
            selfKillAfter -= Time.deltaTime;
            if (selfKillAfter <= 0)
                Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            if (s.activateAutomaticallyAfter >= 0)
            {
                s.activateAutomaticallyAfter -= Time.deltaTime;
                if (s.activateAutomaticallyAfter < 0)
                {
                    PlaySound(s.name);
                }
            }
        }
    }

    public void DestroyAudioSourceAfterFadeout(string name, float velocityMinus)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                AudioSource audioSource = s.audioSource;
                if (audioSource != null)
                {
                    audioSource.volume -= velocityMinus;
                    if (audioSource.volume <= 0)
                        Destroy(audioSource);
                }
            }
        }
    }

    public void PlaySound(string _name, float _volumeModifier = 1, float _fixedPitch = 0)
    {
        //print("Call " + _name + " on " + gameObject.name);
        if (deactivatedAtStart <= 0)
        {
            foreach (Sound s in sounds)
            {
                if (s.name == _name)
                {
                    //print("THE NAME IS THE SAME! So play " + _name + " on " + gameObject.name);

                    //FIND THE RIGHT AUDIOSOURCE
                    AudioSource audioSource;
                    if (s.objectWithAudioSource != null)
                    {
                        if(s.audioSource == null)
                        {
                            audioSource = s.objectWithAudioSource.GetComponent<AudioSource>();
                            s.audioSource = audioSource;
                        }
                        //print("s.objectWithAudioSource is not null for " + _name + " on " + gameObject.name + " audiosource: " + audioSource.name);
                    }
                    else
                    {
                        if (s.audioSource != null)
                        {
                            audioSource = s.audioSource;
                            //print("s.audioSource is not null for " + _name + " on " + gameObject.name + " audiosource: " + audioSource.name);
                        }
                        else
                        {
                            audioSource = defaultAudioSource;
                            s.audioSource = audioSource;
                            //print("s.audioSource is null and set to default " + audioSource.name + " for " + _name + " on " + gameObject.name);
                        }
                        if (audioSource == null)
                        {
                            //print("After all, audiosource is STILL null!! For "  + _name + " on " + gameObject.name);
                            audioSource = gameObject.AddComponent<AudioSource>();
                            audioSource.outputAudioMixerGroup = s.mixerGroupIfNoGameObjectSelected;
                            s.audioSource = audioSource;
                        }
                    }

                    // PITCH MODIFICATION
                    if (s.startPitch == 0)
                    {
                        s.startPitch = s.audioSource.pitch;
                    }
                    float modifiedStartPitch = s.startPitch;
                    if (_fixedPitch != 0)
                    {
                        s.audioSource.pitch = _fixedPitch;
                        modifiedStartPitch = _fixedPitch;
                    }
                    if(s.minPitchModifier != s.maxPitchModifier)
                    {
                        
                        s.audioSource.pitch = modifiedStartPitch + Random.Range(s.minPitchModifier, s.maxPitchModifier);
                    }

                    //SELECT AN AUDIO CLIP
                    AudioClip audioClip;

                    audioClip = s.audioClips[Random.Range(0, s.audioClips.Length)];
                    //print("Play " + _name + " on " + gameObject.name);

                    //PLAY THE AUDIO CLIP AND RETURN
                    s.audioSource.PlayOneShot(audioClip, s.volumeScale * _volumeModifier);
                    return;
                }
            }
            Debug.LogError("There is no sound called " + _name + " on the OneShotter " + this);
        }
    }

    public int GetArrayLength(string _name)
    {
        int length;
        foreach (Sound s in sounds)
        {
            if (s.name == _name)
            {
                length = s.audioClips.Length;
                return length;
            }
        }
        return -1;
    }
}
