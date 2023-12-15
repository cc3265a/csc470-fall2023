/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        src.volume = 0f;
        StartCoroutine(Fade(true, src, 2f, 0.4f));
        StartCoroutine(Fade(false, src, 2f, 0.4f));


    }

    public IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVolume)
    {
        if (!fadeIn)
        {
            double lengthOfSource = (double)source.clip.samples / source.clip.frequency;
            yield return new WaitForSecondsRealtime((float)(lengthOfSource - duration));
        }
        float time = 0f;
        float startVol = source.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/
