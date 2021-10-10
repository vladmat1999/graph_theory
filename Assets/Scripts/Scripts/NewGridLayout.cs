using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NewGridLayout : MonoBehaviour
{
    
    public Vector2 gridSize;
    public Vector2 resolution = new Vector2(0.27f * 2, 5.9f * 2);
    public List<NewNode> nodes = new List<NewNode>();
    private List<LineRenderer> lines = new List<LineRenderer>();
    private List<Vector3[]> originalCoords = new List<Vector3[]>();
    public Game game;
    public GameObject newNodePrefab;
    public RectTransform scrollView;
    public static float scrollY;
    public static float amount = 0.3f;
    public static Color wonColor = new Color(0.83f,0.83f,0.83f);
    public string asset;
    void Start()
    {
        StringReader reader = new StringReader(((TextAsset)Resources.Load("levels")).text);
        string[] line;
        string ln;
        float i1,i2;
        NewNode n;
        int i = 1;

        ln = reader.ReadLine();
        line = ln.Split(' ');
        i1 = float.Parse(line[0]);
        i2 = float.Parse(line[1]);
        n = createNode(i1,i2, i);
        n.setText(1);
        n.setNodeCompletion(LevelManager.levels[i-1]);
        i++;
        float pi1 = i1;
        float pi2 = i2;

        while((ln = reader.ReadLine()) != null)
        {
            line = ln.Split(' ');
            i1 = float.Parse(line[0]);
            i2 = float.Parse(line[1]);
            n = createNode(i1,i2, i);
            n.setText(i);
            n.setNodeCompletion(LevelManager.levels[i-1]);
            drawLine(pi1, pi2, i1, i2, nodes[i-2].completed, nodes[i-1].completed);
            i++;
            pi1=i1;
            pi2=i2;
        }
        pack();

        setCurrentLevel(Game.levelNumber);

        gameObject.SetActive(false);
    }

    public void setCurrentLevel(int i)
    {
        foreach(NewNode a in nodes)
        {
            a.setCurrentLevel(false);
        }
        nodes[i].setCurrentLevel(true);
    }
    public void setLevelCompletion(int i, float f)
    {
        try{
            nodes[i].setNodeCompletion(f);
            lines[i].startColor = wonColor;
            lines[i].endColor = nodes[i+1].completed ? wonColor : Color.black;
            lines[i-1].endColor = wonColor;
        }
        catch(System.Exception){};
    }
    public void add(NewNode n)
    {
        nodes.Add(n);
    }

    public void pack()
    {
        Vector2 center = Vector2.zero;
        Vector2 interval = new Vector2(resolution.x / gridSize.x, resolution.y / gridSize.y);
        Vector2 topLeftCorner = new Vector2(center.x - resolution.x / 2, center.y + resolution.y / 2);

        foreach(NewNode node in nodes)
        {
            node.setPosition(topLeftCorner.x + node.position.x * interval.x,
                             topLeftCorner.y - node.position.y * interval.y);
        }
    }

    public NewNode createNode(float x, float y, int n)
    {
        GameObject node = GameObject.Instantiate(newNodePrefab, Vector3.zero, Quaternion.identity);
        node.transform.SetParent(transform);
        node.transform.localPosition = Vector3.zero;
        node.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        NewNode newNode = node.GetComponent<NewNode>();
        newNode.setGraphPosition(x ,y);
        nodes.Add(newNode);
        newNode.grid = this;
        newNode.number = n;
        return newNode;
    }

    public LineRenderer drawLine(float x1, float y1, float x2, float y2, bool completed = false, bool completed2 = false)
    {
        Color c1, c2;
        if(completed)
            c1 = wonColor;
        else
            c1 = Color.black;

        if(completed2)
            c2 = wonColor;
        else
            c2 = Color.black;

        Vector2 center = Vector2.zero;
        Vector2 interval = new Vector2(resolution.x / gridSize.x, resolution.y / gridSize.y);
        Vector2 topLeftCorner = new Vector2(center.x - resolution.x / 2, center.y + resolution.y / 2);
        
        Vector3 start = new Vector2(topLeftCorner.x + x1 * interval.x, topLeftCorner.y - y1 * interval.y);
        Vector3 end = new Vector2(topLeftCorner.x + x2 * interval.x, topLeftCorner.y - y2 * interval.y);

        start.z = 0;
        end.z = 0;

        GameObject myLine = new GameObject();
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.material = GameObject.Find("Canvas").GetComponent<Game>().LineMaterial;
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lr.receiveShadows = false;
        lr.allowOcclusionWhenDynamic = false;
        lr.startColor = c1;
        lr.endColor = c2;
        lr.startWidth = 2.0f;
        lr.endWidth = 2.0f;
        myLine.transform.SetParent(transform);
        myLine.transform.localPosition = Vector3.zero;
        myLine.transform.localScale = new Vector3(1,1,1);
        lr.SetPosition(0, start);
        lr.numCapVertices = 2;
        lr.SetPosition(1, end);
        lines.Add(lr);
        originalCoords.Add(new Vector3[]{start, end});
        return lr;
    }

    public void interpolateLine(int x1, int y1, int x2, int y2, LineRenderer line, float ratio)
    {
        Vector2 center = Vector2.zero;
        Vector2 interval = new Vector2(resolution.x / gridSize.x, resolution.y / gridSize.y);
        Vector2 topLeftCorner = new Vector2(center.x - resolution.x / 2, center.y + resolution.y / 2);
        
        Vector3 start = new Vector2(topLeftCorner.x + x1 * interval.x, topLeftCorner.y + y1 * interval.y);
        Vector3 end = new Vector2(topLeftCorner.x + x2 * interval.x, topLeftCorner.y - y2 * interval.y);
        end = start + (end - start) * ratio;

        start.z = 0;
        end.z = 0;

        line.startWidth = 2.0f * ratio;
        line.endWidth = 2.0f * ratio;

        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    public void sizeNodes()
    {
        if(gameObject.activeSelf)
        {
            float delta = scrollView.sizeDelta.y / 2 * amount;
            foreach(NewNode n in nodes)
            {
                float realY = (n.gameObject.transform.localPosition + transform.localPosition).y;
                float percent = 1;

                if(realY >= scrollView.sizeDelta.y / 2 - delta)
                {
                    percent = (scrollView.sizeDelta.y / 2 - realY) / delta;
                }   
                else if(realY <= -scrollView.sizeDelta.y / 2 + delta)
                {
                    percent = (scrollView.sizeDelta.y / 2 + realY) / delta;
                }

                percent = percent < 0 ? 0 : percent;
                percent = percent > 1 ? 1 : percent;

                n.gameObject.transform.localScale = new Vector3(2,2,0.02f) * percent;
            }

            for(int i=0; i<lines.Count; i++)
            {
                LineRenderer lr = lines[i];
                int maxPoint = lr.GetPosition(0).y > lr.GetPosition(1).y ? 0 : 1;

                float realYMax = (originalCoords[i][maxPoint] + transform.localPosition).y;
                float percent = 1;

                int minPoint = lr.GetPosition(0).y <= lr.GetPosition(1).y ? 0 : 1;
                float realYMin = (originalCoords[i][minPoint] + transform.localPosition).y;

                if(realYMax >= scrollView.sizeDelta.y / 2 - delta && realYMax <= scrollView.sizeDelta.y/2)
                {
                    percent = (scrollView.sizeDelta.y / 2 - realYMax) / delta;
                }
                else if(realYMax > scrollView.sizeDelta.y/2)   
                {
                    percent = 0;
                }

                else if(realYMin <= -scrollView.sizeDelta.y / 2 + delta && realYMin >= -scrollView.sizeDelta.y)
                {
                    percent = (scrollView.sizeDelta.y / 2 + realYMin) / delta;
                }
                else if(realYMin < -scrollView.sizeDelta.y/2)
                {
                    percent = 0;
                }
                else
                {
                    percent = 1;
                }

                percent = percent < 0 ? 0 : percent;
                percent = percent > 1 ? 1 : percent;

                if(realYMax > scrollView.sizeDelta.y / 2 - delta && realYMax <= scrollView.sizeDelta.y / 2)
                {
                    Vector3 start = originalCoords[i][0];
                    Vector3 end = originalCoords[i][1];
                    Vector3 deltaLine = (end - start) * percent;
                    start = start + deltaLine;
                    lr.SetPosition(1, start);
                }   


                else if(realYMin < -scrollView.sizeDelta.y / 2 + delta && realYMin >= -scrollView.sizeDelta.y / 2)
                {
                    Vector3 start = originalCoords[i][0];
                    Vector3 end = originalCoords[i][1];
                    Vector3 deltaLine = (start - end) * percent;
                    lr.SetPosition(0, end + deltaLine);
                }
                else
                {
                    lr.SetPositions(originalCoords[i]);
                }


                if(percent == 0)
                {
                    lr.gameObject.SetActive(false);
                }
                else
                {
                    lr.gameObject.SetActive(true);
                }

            }
        }
    }

    public void exportNode()
    {
        using (System.IO.StreamWriter file = 
            new System.IO.StreamWriter("Assets/levels.txt"))
        {
            foreach (NewNode n in nodes)
            {
                file.WriteLine("" + n.position.x + " " + n.position.y);
            }
        }
    }

    void Update()
    {
        //sizeNodes();
    }
/*
    void Update()
    {
        pack();
        for(int i=0; i<lines.Count; i++)
        {
            try
            {
                lines[i].SetPosition(0,nodes[i].gameObject.transform.localPosition);
                lines[i].SetPosition(1,nodes[i+1].gameObject.transform.localPosition);
            }
            catch(System.Exception){}
        }
    }
    */
}
