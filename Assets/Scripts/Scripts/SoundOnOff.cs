using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnOff : MonoBehaviour
{
    public Animator anim;
    private bool isOn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        isOn = !isOn;
        anim.SetBool("On", isOn);
        AudioListener.volume = isOn ? 1 : 0;
    }
}
