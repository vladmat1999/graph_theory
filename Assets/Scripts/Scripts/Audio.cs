using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static AudioSource audioSource;
    static AudioClip click;
    static AudioClip line;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        click = (AudioClip)Resources.Load("Click");
        line = (AudioClip)Resources.Load("line");
    }

    public static void playClick()
    {
        audioSource.PlayOneShot(click);
    }

    public static void playLine()
    {
        audioSource.PlayOneShot(line, 0.5f);
    }
}
