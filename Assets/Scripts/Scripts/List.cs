using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List : MonoBehaviour
{
    public Animator animator;
    public GameObject topBar;
    public GameObject graph;
    public GameObject stayOnList;
    public GameObject moveUp;
    public Game game;
    public RectTransform scrollView;
    private bool listOn = false;
    public GameObject soundCollider;
    public NewGridLayout gridLayout;
    public Tutorial tutorial;

    public void OnMouseDown()
    {
        listOn = !listOn;
        animator.SetBool("ListOn", listOn);
        if(listOn)
            StartCoroutine(listOnAnim());
        else
        {           
            StartCoroutine(listOffAnim());
        }

        Audio.playClick();
    }

    public IEnumerator listOnAnim()
    {
        try
        {
            if(Game.levelNumber == 0)
            {
                Game.staticTut.allGraphics[0].SetBool("Exit", true);
            }
            else if(Game.levelNumber == 1)
            {
                Game.staticTut.allGraphics[1].SetBool("Exit", true);
            }
            else if(Game.levelNumber == 2)
            {
                Game.staticTut.allGraphics[2].SetBool("Exit", true);
                Game.staticTut.allGraphics[3].SetBool("Exit", true);
            }
        }
        catch(System.Exception){}
        
        gridLayout.gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        int distance = (int) Game.resolution.y;

        Vector3 start = Vector3.zero;
        Vector3 startMoveUp = Vector3.zero;

        Vector3 end = new Vector3(0, -distance, 0);
        Vector3 endMoveUp = new Vector3(0, distance * 0.2f, 0);

        for(int i=0; i<60; i++)
        {
            start = Vector3.Lerp(start, end, 0.1f);
            startMoveUp = Vector3.Lerp(startMoveUp, endMoveUp, 0.1f);

            topBar.transform.localPosition = start;
            graph.transform.localPosition = start;
            stayOnList.transform.localPosition = -start;
            moveUp.transform.localPosition = startMoveUp;
            gridLayout.gameObject.SetActive(true);
            yield return new WaitForFixedUpdate();
        }
        soundCollider.SetActive(true);
    }

    public IEnumerator listOffAnim()
    {
        soundCollider.SetActive(false);
        GetComponent<AudioSource>().Play();
        int distance = (int) Game.resolution.y;

        Vector3 end = Vector3.zero;
        Vector3 endMoveUp = Vector3.zero;

        Vector3 start = new Vector3(0, -distance, 0);
        Vector3 startMoveUp = new Vector3(0, distance * 0.2f, 0);

        for(int i=0; i<60; i++)
        {
            start = Vector3.Lerp(start, end, 0.1f);
            startMoveUp = Vector3.Lerp(startMoveUp, endMoveUp, 0.1f);

            topBar.transform.localPosition = start;
            graph.transform.localPosition = start;
            stayOnList.transform.localPosition = -start;
            moveUp.transform.localPosition = startMoveUp;
            yield return new WaitForFixedUpdate();
        }
        gridLayout.gameObject.SetActive(false);
    }

    public void loadLevel(int i)
    {
        game.stopSong();
        listOn = !listOn;
        animator.SetBool("ListOn", listOn);
        StartCoroutine(listOffAnim());
        Game.levelNumber = i;
        game.resetGraph();
        game.playSong();
    }
}
