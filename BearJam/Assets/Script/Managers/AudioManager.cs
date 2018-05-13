using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager s_Instance;

    [SerializeField] private AudioSource[] _sfxSources;
    [SerializeField] private AudioSource[] _musicSources;

	// Use this for initialization
	void Awake () {
		if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public void PlaySound(string pClipName, float pVolume = 1.0f)
    {
        bool found = false;
        AudioClip clip = Resources.Load<AudioClip>(string.Format("Audio/SFX/{0}", pClipName));

        if (clip == null)
        {
            Debug.LogWarningFormat("<color=green>AudioManager::</color> Could not play clip named {0} because it was not found in Resources.", pClipName);
            return;
        }

        foreach (AudioSource source in _sfxSources)
        {
            if (source.isPlaying == false)
            {
                found = true;
                source.volume = pVolume;
                source.PlayOneShot(clip);
                break;
            }
        }

        if (found == false)
        {
            Debug.LogFormat("<color=green>AudioManager::</color> Could not play clip {0} because all audio sources were in use.", pClipName);
        }
    }

    public void PlayMusic(string pClipName, float pVolume = 1.0f, bool pShouldLoop = true)
    {
        bool found = false;
        AudioClip clip = Resources.Load<AudioClip>(string.Format("Audio/Music/{0}", pClipName));

        if (clip == null)
        {
            Debug.LogWarningFormat("<color=green>AudioManager::</color> Could not play clip named {0} because it was not found in Resources.", pClipName);
            return;
        }

        foreach (AudioSource source in _musicSources)
        {
            if (source.isPlaying == false)
            {
                found = true;
                source.loop = pShouldLoop;
                source.volume = pVolume;
				source.clip = clip;
                source.Play();
                break;
            }
        }

        if (found == false)
        {
            Debug.LogFormat("<color=green>AudioManager::</color> Could not play clip {0} because all audio sources were in use.", pClipName);
        }
    }

    public static AudioManager Instance
    {
        get { return s_Instance; }
    }
}
