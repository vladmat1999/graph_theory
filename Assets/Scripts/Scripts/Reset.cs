using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public Animator anim;
    public Game game;
    public Timer timer;

    public void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        anim.SetBool("reset", true);
        game.reset();
        timer.resetTime();
        if(Node.isPretty)
        {
            foreach(Node n in Game.nodes)
            {
                if(n.isThisPretty)
                    n.setPretty(false);
            }
        }
    }
}
