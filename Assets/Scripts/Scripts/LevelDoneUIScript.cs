using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoneUIScript : MonoBehaviour
{
    public GameWonController controller;
    public StartBar stars;
    public float f;
    public Animator retry;
    public void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        Audio.playClick();
        controller.nextLevel();
        retry.SetBool("Ready", false);
    }
}
