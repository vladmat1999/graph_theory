using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurst : MonoBehaviour
{
    public ParticleSystem particles;

    public void OnMouseDown()
    {
        particles.Play();
    }
}
