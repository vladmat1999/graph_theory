using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public Animator anim;
    private bool isTip = true;
    public PopUp popUp;

    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();

        if(isTip)
        {
            //isTip = !isTip;
            anim.SetBool("isTip", isTip);

            Solver solve = new Solver();

            List<Node> l = solve.solveGraph();

            if(l != null)
            {
                try
                {
                l[0].setPretty(true);
                }
                catch(System.Exception){}
                StartCoroutine(tipRoutine());
            }
        }
        else
        {
            popUp.gameObject.SetActive(true);
            popUp.activate();
        }
    }

    public void reset()
    {
        isTip = true;
        anim.SetBool("isTip", isTip);
    }

    IEnumerator tipRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Tip"));
    }
}
