using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioClip musicOnStart;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
    }

    private void Start()
    {
        Play(musicOnStart);
    }

    

    AudioClip switchTo;
    [SerializeField] float timetoSwitch;
    void Play(AudioClip audioClip, bool interrupt = false)
    {
        if (interrupt)
        {
            volume = 0.1f;
            audioSource.volume = volume;
            audioSource.clip = switchTo;
            audioSource.Play();
        }
        else
        {
            switchTo = audioClip;
            StartCoroutine(SmoothSwitchMusic());
        }
    }

    float volume;

    IEnumerator SmoothSwitchMusic() {
        volume = 0.1f;
        while (volume > 0f)
        {
            volume -= Time.deltaTime / timetoSwitch;
            if(volume < 0f)
            {
                volume = 0f;
            }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }
        Play(switchTo, true);
    }
}
