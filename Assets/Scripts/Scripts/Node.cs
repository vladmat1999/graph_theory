using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static void connect(Node n1, Node n2)
    {
        n1.addConnection(n2);
        n2.addConnection(n1);

        bool other = Random.next(1,3) < 2 ? true : false;
        if(other)
        {
            n2.drawLine(n1);
        }
        else
        {
        n1.drawLine(n2);
        }
    }

    public static void connect(Node n1, Node n2, Color c)
    {
        n1.addConnection(n2);
        n2.addConnection(n1);

        bool other = Random.next(1,3) < 2 ? true : false;
        if(other)
        {
            n2.drawLine(n1, c);
        }
        else
        {
        n1.drawLine(n2, c);
        }
    }

    public static void connectNoGraphics(Node n1, Node n2)
    {
        n1.addConnection(n2);
        n2.addConnection(n1);
    }

    public GameObject getColourNode()
    {
        return gameObject.transform.GetChild(0).gameObject;
    }

    public void disableClick()
    {
        gameObject.GetComponent<OnNodeClick>().enabled = false;
        gameObject.GetComponent<OnNodeClick>().isActive = false;
    }

    public void Start()
    {
        if(parent == null)
            parent = GameObject.Find("LineParent");

        nodeColor = new Color(0.8313f, 0.8131f, 0.8313f, 1);
    }

    public static GameObject parent;

    public Shader line;
    public Vector2 gridPosition;
    public int number;
    private HashSet<Node> connections = new HashSet<Node>();
    public Dictionary<Node, GameObject> lines = new Dictionary<Node, GameObject>();
    public HashSet<Node> allNodes = new HashSet<Node>();
    public HashSet<Node> newConnections = new HashSet<Node>();
    public static bool isPretty = false;
    public bool isThisPretty = false;
    public Color nodeColor = new Color(0.8313f, 0.8131f, 0.8313f, 1);

    //Constructors

    public Node() { }

    //Methods

        //Set position

    public void setPosition(Vector3 newPos)
    {
        transform.localPosition = newPos;
    }

    public void setPosition(float x, float y)
    {
        setPosition(new Vector2(x, y));
    }

    public Vector2 getPosition()
    {
        return transform.localPosition;
    }

    public Vector2 getWorldPosition()
    {
        return transform.position;
    }

    public void addLine(Node n, GameObject l)
    {
        try
        {
        lines.Add(n, l);
        }
        catch(System.Exception)
        {}
    }

    //Get Grid Coordinates

    public float getGridX()
    {
        return gridPosition.x;
    }

    public float getGridY()
    {
        return gridPosition.y;
    }

    public void setGrid(float x, float y)
    {
        gridPosition = new Vector2(x, y);
    }

    //Set connections

    void setConnections(HashSet<Node> newConnections)
    {
        connections = newConnections;
    }

    void addConnection(Node n)
    {
        connections.Add(n);
    }

    //Draw 

    public void drawNode()
    {

    }

    public void drawLine(Node other)
    {
        StartCoroutine(DrawLineEnum(this, other, new Color(0.8313f, 0.8131f, 0.8313f, 1)));
    }

    public void drawLine(Node other, Color c)
    {
        StartCoroutine(DrawLineEnum(this, other, c));
    }

    public IEnumerator DrawLineEnum(Node n, Node other, Color color)
    {
        Audio.playLine();
        Vector3 start = n.getWorldPosition();
        Vector3 end = other.getWorldPosition();

        start.z = 0;
        end.z = 0;

        GameObject myLine = new GameObject();
        myLine.transform.position = Vector2.zero;
        myLine.AddComponent<LineRenderer>();
        myLine.tag = "Line";
        myLine.transform.parent = parent.transform;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.material = GameObject.Find("Canvas").GetComponent<Game>().LineMaterial;
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lr.receiveShadows = false;
        lr.allowOcclusionWhenDynamic = false;
        lr.startColor = (Color)n.nodeColor;
        lr.endColor = (Color)other.nodeColor;
        lr.startWidth = 3.0f * Game.lineScale;
        lr.endWidth = 3.0f * Game.lineScale;
        lr.SetPosition(0, start);
        lr.numCapVertices = 2;
        
        this.addLine(other, myLine);
        other.addLine(this, myLine);

        for(int i=0; i<=30; i++)
        {
            try
            {
            start = Vector3.Lerp(start, end, 0.1f);
            lr.SetPosition(1, start);
            }
            catch(System.Exception){}
            yield return new WaitForFixedUpdate();
        }
    }

    public void retractLine(Node other)
    {
        try
        {
            GameObject line = lines[other];
            StartCoroutine(retractLineEnum(line, other));
        }
        catch(System.Exception){}
    }

    IEnumerator retractLineEnum(GameObject line, Node other)
    {
        Audio.playLine();
        LineRenderer lr = line.GetComponent<LineRenderer>();

        Vector3 end = this.getWorldPosition();
        Vector3 start = other.getWorldPosition();

        start.z = 0;
        end.z = 0;

        lr.gameObject.transform.position = Vector2.zero;

        lr.SetPosition(0, end);
        lr.SetPosition(1, start);

        for(int i=0; i<=30; i++)
        {
            start = Vector3.Lerp(start, end, 0.1f);
            lr.SetPosition(1, start);
            yield return new WaitForFixedUpdate();
        }

        GameObject.Destroy(line);
    }

    public void deleteConnection(Node n)
    {
        connections.Remove(n);
        lines.Remove(n);
    }

    public void deleteThisConnection(Node n)
    {
        connections.Remove(n);
    }

    public void deleteAll()
    {
        foreach(Node n in connections)
        {
            retractLine(n);
            n.deleteConnection(this);
            connections = new HashSet<Node>();
            lines = new Dictionary<Node, GameObject>();
        }
    }

    public void disconnectAll()
    {
        HashSet<Node> tmp  = new HashSet<Node>(connections);
        foreach(Node n in tmp)
        {
            bool rnd = Random.next(1,3) < 2 ? true : false;
            if(rnd)
                retractLine(n);
            else
                n.retractLine(this);
            n.deleteConnection(this);
        }

        HashSet<Node> temp = new HashSet<Node>(allNodes);
        temp.ExceptWith(connections);

        lines = new Dictionary<Node, GameObject>();

        connections = new HashSet<Node>(temp);

        foreach(Node n in connections)
        {
            connect(this, n);
        }
    }

    public void switchConnection()
    {
        HashSet<Node> tmp  = new HashSet<Node>(connections);
        
        foreach(Node n in tmp)
        {
            n.deleteThisConnection(this);
        }

        HashSet<Node> temp = new HashSet<Node>(allNodes);
        temp.ExceptWith(connections);

        connections = new HashSet<Node>(temp);

        foreach(Node n in connections)
        {
            connectNoGraphics(this, n);
        }
    }

    public void setPretty(bool pretty)
    {
        if(pretty)
        {
            Animator prettyAnim = gameObject.transform.GetChild(2).GetComponent<Animator>();
            isPretty = true;
            isThisPretty = true;
            prettyAnim.SetBool("isPretty", isPretty);
        }
        else
        {
            Animator prettyAnim = gameObject.transform.GetChild(2).GetComponent<Animator>();
            isPretty = false;
            isThisPretty = false;   
            prettyAnim.SetBool("isPretty", isPretty);
        }
    }

    public bool isConnectedToAll()
    {
        return connections.SetEquals(allNodes);
    }

    public bool isConnectedTo(Node n)
    {
        return connections.Contains(n);
    }

    public void addNewConnection(Node n)
    {
        newConnections.Add(n);
    }

    public void submitConnection()
    {
        HashSet<Node> diff = new HashSet<Node>(connections);
        diff.ExceptWith(newConnections);

        foreach(Node n in diff)
        {
            n.retractLine(this);
            n.deleteConnection(this);
        }

        diff = new HashSet<Node>(newConnections);
        diff.ExceptWith(connections);

        foreach(Node n in diff)
        {
            Node.connect(this, n);
        }

        connections = new HashSet<Node>(newConnections);
    }

    public void emitParticles()
    {
        ParticleSystem particles = gameObject.GetComponentInChildren<ParticleSystem>();
        GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("NodeFireworks"));
        particles.Play();
    }

    public void setClickable(bool value)
    {
        gameObject.GetComponent<OnNodeClick>().isActive = value;
    }

    public void cycleColors()
    {
        StartCoroutine(cycleNodeColors());
    }

    IEnumerator cycleNodeColors()
    {
        Color c = nodeColor;
        Vector4 startColor = c;
        Vector4 endColor, deltaColor;
        int i = System.Array.IndexOf(GameWonController.colors, c);
        SpriteRenderer sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        Node.colors++;

        for(; i < GameWonController.availableColors.Length; i = (i+1) % 5)
        {
            endColor = GameWonController.availableColors[i];
            deltaColor = (endColor - startColor) / 90f;

            for(int j = 0; j< 90; j++)
            {
                startColor += deltaColor;
                sr.color = startColor;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public static int colors=0;

    //Overriden

    override
    public int GetHashCode()
    {   
        return (int) (gridPosition.x * 100000 + gridPosition.y);
    }

    override
    public bool Equals(object other)
    {
        try
        {
            return ((Node)this).GetHashCode() == ((Node)other).GetHashCode();
        }
        catch(System.Exception)
        {
            return false;
        }
    }

    override
    public string ToString()
    {
        return "" + gridPosition.x + " " + gridPosition.y;
    }

}
