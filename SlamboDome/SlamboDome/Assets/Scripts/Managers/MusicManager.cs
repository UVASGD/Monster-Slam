using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public List<AudioClip> tracks = new List<AudioClip>();
    AudioSource aud;

    bool fading = false;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!aud.clip)
            PlayRandom();
        if (!fading && (aud.clip.length - aud.time) < 2f)
            StartCoroutine(FadeOut());
        if (!aud.isPlaying)
            PlayRandom();
    }

    public void PlayRandom()
    {
        if (tracks.Count > 0)
        {
            aud.clip = tracks[Random.Range(0, tracks.Count)];
            aud.volume = 0;
            StartCoroutine(FadeIn());  
        }
    }

    IEnumerator FadeOut()
    {
        fading = true;
        while (aud.volume > 0.1f)
        {
            yield return null;
        }
        aud.Stop();
        fading = false;
    }

    IEnumerator FadeIn()
    {
        aud.Play();
        while (aud.volume < 0.9f)
        {
            aud.volume += 0.01f;
            yield return null;
        }
    }
}
