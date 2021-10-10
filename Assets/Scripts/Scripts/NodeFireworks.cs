using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFireworks : MonoBehaviour
{
    public void OnMouseDown()
    {
        Node node = gameObject.GetComponentInParent<Node>();
        Audio.playClick();
        node.emitParticles();
    }
}
