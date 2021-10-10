using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Game game;
    public GameObject list;
    public GameObject tip;
    public GameObject reset;
    public Animator[] allGraphics;

    public void start()
    {
        StartCoroutine(step1());
    }

    IEnumerator step1()
    {
        game.newGrid.setCurrentLevel(0);
        game.gameWonController.chooseColour();
        game.scoreBoard.reset();
        game.timer.reset();
        game.timer.start();
        game.stars.reset();
        game.levelNumberText.text = "0";
        Game.levelNumber = 0;

        list.GetComponent<Collider2D>().enabled = false;
        tip.GetComponent<Collider2D>().enabled = false;
        reset.GetComponent<Collider2D>().enabled = false;

        game.grid = new GridLayout(4, 4, game);
        Node n1 = game.spawnNode(2,2);
        Game.nodes.Add(n1);
        game.grid.add(n1);
        game.grid.pack();

        yield return new WaitForSeconds(0.5f);
        Node n2 = game.spawnNode(2.1f,2);
        Game.nodes.Add(n2);
        game.grid.add(n2);
        game.grid.pack();

 

        Vector2 startPos1 = n1.gridPosition;
        Vector2 startPos2 = n2.gridPosition;
        Vector2 endPos1 = new Vector2(0.5f,2);
        Vector2 endPos2 = new Vector2(3.5f,2);

        for(int i=0; i<30;i++)
        {
            startPos1 = Vector2.Lerp(startPos1, endPos1, 0.1f);
            startPos2 = Vector2.Lerp(startPos2, endPos2, 0.1f);
            n1.gridPosition = startPos1;
            n2.gridPosition = startPos2;
            game.grid.pack();
            yield return new WaitForFixedUpdate();
        }

        n1.allNodes = new HashSet<Node>(new Node[]{n2});
        n2.allNodes = new HashSet<Node>(new Node[]{n1});

        n1.setClickable(true);
        n2.setClickable(true);

        try
        {
            allGraphics[0].gameObject.SetActive(true);
        }
        catch(System.Exception){}

        yield return new WaitForFixedUpdate();
    }

    public void completed()
    {
        list.GetComponent<Collider2D>().enabled = true;
        tip.GetComponent<Collider2D>().enabled = true;
        reset.GetComponent<Collider2D>().enabled = true;
        LevelManager.tutorialCompleted();
    }

    public void start1()
    {
        StartCoroutine(start1Enum());   
    }

    IEnumerator start1Enum()
    {
        try
        {
            allGraphics[1].gameObject.SetActive(true);
        }
        catch(System.Exception){}
        yield return null;
    }
    public void start2()
    {
        StartCoroutine(start2Enum());
    }

    IEnumerator start2Enum()
    {
        try
        {
            allGraphics[2].gameObject.SetActive(true);
        }
        catch(System.Exception){}

        yield return new WaitForSeconds(1);

        try
        {
            allGraphics[3].gameObject.transform.parent.gameObject.SetActive(true);
        }
        catch(System.Exception){}
    }
}
