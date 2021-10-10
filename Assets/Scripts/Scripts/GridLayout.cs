using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayout
{
    private Vector2 gridSize;
    private HashSet<Node> nodes = new HashSet<Node>();
    private Game game;

    public GridLayout(int x, int y, Game newGame)
    {
        gridSize = new Vector2(x, y);
        game = newGame;
    }

    public GridLayout(Vector2 newGridSize)
    {
        gridSize = newGridSize;
    }

    public void setSize(int x, int y)
    {
        gridSize = new Vector2(x, y);
    }

    public void add(Node n)
    {
        nodes.Add(n);
    }

    public void pack()
    {
        Vector2 center = Vector2.zero - new Vector2(0, ScreenRes.deltaHeight * 30);
        Vector2 resolution =  new Vector2(Game.deltaWidth * 350, Game.deltaWidth * 350);
        
        Vector2 interval = new Vector2(resolution.x / gridSize.x, resolution.y / gridSize.y);
        Vector2 topLeftCorner = new Vector2(center.x - resolution.x / 2, center.y + resolution.y / 2);

        foreach(Node node in nodes)
        {
            node.setPosition(topLeftCorner.x + node.getGridX() * interval.x,
                             topLeftCorner.y - node.getGridY() * interval.y);
        }
    }

    public IEnumerator prettify(Vector2 center, Vector2 resolution)
    {
        Color[] colors = GameWonController.availableColors;

        HashSet<Node> newNodes = new HashSet<Node>();
        foreach(Node n in nodes)
        {
            int rnd = Random.next(0, colors.Length);
            n.nodeColor = colors[rnd];
        }

        foreach(Node n in nodes)
        {
            n.emitParticles();
            GameObject colourNode = n.getColourNode();
            SpriteRenderer sr = colourNode.GetComponentInChildren<SpriteRenderer>();
            sr.color = n.nodeColor;
            colourNode.SetActive(true);
            n.disableClick();

            n.disconnectAll();

            foreach(Node n2 in n.allNodes)
            {
                try
                {
                if(!n.Equals(n2) && !n.isConnectedTo(n2))
                {
                    int rnd = Random.next(0, colors.Length);
                    Node.connect(n, n2, colors[rnd]);
                }
                }
                catch(System.Exception){}
            }

            n.lines = new Dictionary<Node, GameObject>();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void prettyPack(Vector2 center, Vector2 resolution, HashSet<Node> newNodes)
    {
        Vector2 interval = new Vector2(resolution.x / gridSize.x, resolution.y / gridSize.y);
        Vector2 topLeftCorner = new Vector2(center.x - resolution.x / 2, center.y + resolution.y / 2);

        foreach(Node node in newNodes)
        {
            node.setPosition(topLeftCorner.x + node.getGridX() * interval.x,
                             topLeftCorner.y - node.getGridY() * interval.y);
        }
    }

    IEnumerator drawAll()
    {
        yield return new WaitForSeconds(0);
    }

    public void setEnabled(bool b)
    {
        foreach(Node n in nodes)
        {
            n.setClickable(b);
        }
    }
}
