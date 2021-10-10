using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryLevel : MonoBehaviour
{
    public GameWonController controller;
    public Animator retry;
    public void OnMouseDown()
    {
        controller.retryLevel();
        retry.SetBool("Ready", false);
        Audio.playClick();
        GetComponent<AudioSource>().Play();
    }
}
