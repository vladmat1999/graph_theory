using System.Collections;
using UnityEngine;

public static class AudioControl {
 
    public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn (AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
        audioSource.volume = 0;

        audioSource.Play();
 
        while (audioSource.volume < startVolume) {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
    }
 
 
}
 