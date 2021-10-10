using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Game : MonoBehaviour
{
    public static float width;
    public static float height;
    public static float deltaWidth;
    public static float deltaHeight;
    public static Vector2 resolution;
    public GridLayout grid;

    public Shader line;
    public NumberContainer scoreBoard;

    public GameObject nodePrefab;
    public static List<Node> nodes = new List<Node>();
    public static int levelNumber;
    public Material LineMaterial;
    public GameWonController gameWonController;
    public Timer timer;
    public TopBarScript topBar;
    public GameObject gridObject;
    public GameObject lineParent;
    public GameObject gameWonObject;
    public Vector2[] originalPosition;
    public Text levelNumberText;
    public static float nodeScale = 1, lineScale = 1;
    public static int targetScore;
    public StartBar stars;
    public NewGridLayout newGrid;
    public LevelManager levelManager;
    public AudioClip[] music;
    public Tutorial tutorial;
    public static Tutorial staticTut;
    public GameWonAnimation gameWonAnimation;
    public Tip tip;

   

    void Awake()
    {
        //Set resolution

        
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        width = ScreenRes.width;
        height = ScreenRes.height;
        deltaWidth = ScreenRes.deltaWidth;
        deltaHeight = ScreenRes.deltaHeight;
        resolution = ScreenRes.resolution;
        
        originalPosition = new Vector2[]{Vector2.zero - new Vector2(0, Game.deltaHeight * 30), new Vector2(Game.deltaWidth * 350, Game.deltaWidth * 350)};
        
        grid = new GridLayout(0, 0, this);

        staticTut = tutorial;

        newGrid.setCurrentLevel(levelNumber-1);

        if(LevelManager.tutorialDone)
            loadLevel(levelNumber);
        else
            tutorial.start();    
    }

    public Node spawnNode(float x, float y)
    {
        GameObject go = (GameObject)Instantiate(nodePrefab);
        go.transform.SetParent(lineParent.transform);
        Node n = go.GetComponent<Node>();
        n.setGrid(x, y);
        go.transform.localScale = go.transform.lossyScale * Game.deltaHeight * nodeScale;
        return n;
    }

    void loadLevel(int number)
    {
        tip.reset();

        if(GameWonAnimation.isOn)
            gameWonAnimation.dissappear();
            
        if(number > 100)
        {
            gameWonAnimation.animate();
            timer.reset();
            scoreBoard.reset();
            return;
        }

        if(number == 0)
        {
            tutorial.start();
            return;
        }
        else if(number == 1)
        {
            tutorial.start1();
        }
        else if(number == 2)
        {
            tutorial.start2();
        }

        newGrid.setCurrentLevel(levelNumber-1);
        gameWonController.chooseColour();
        scoreBoard.reset();
        timer.reset();
        timer.start();
        stars.reset();
        levelNumberText.text = levelNumber.ToString();
        StartCoroutine(read("lvl" + number));
    }
    IEnumerator read(string filename)
    {
        StringReader reader = new StringReader(((TextAsset)Resources.Load(filename)).text);
        string[] line = reader.ReadLine().Split(' ');

        float waitTimeNodes = 1 / float.Parse(line[0]);
        float waitTimeLines = 0.5f / float.Parse(line[1]);

        line = reader.ReadLine().Split(' ');

        nodeScale = float.Parse(line[0]);
        lineScale = float.Parse(line[1]);

        targetScore = int.Parse(reader.ReadLine());

        line = reader.ReadLine().Split(' ');

        grid.setSize(int.Parse(line[0]), int.Parse(line[1]));

        int n = int.Parse(reader.ReadLine());
        

        for(int i=0; i<n; i++)
        {
            line = reader.ReadLine().Split(' ');
            
            Node node = spawnNode(int.Parse(line[0]), int.Parse(line[1]));

            nodes.Add(node);
            grid.add(node);
            grid.pack();

            yield return new WaitForSeconds(waitTimeNodes);
        }

        foreach(Node node in nodes)
        {
            line = reader.ReadLine().Split(' ');
            HashSet<Node> tmp = new HashSet<Node>();
            foreach(string nb in line)
                tmp.Add(nodes[int.Parse(nb) - 1]);
            node.allNodes = tmp;
        }

        string ln;
        int i1, i2;

        while((ln = reader.ReadLine()) != null)
        {
            line = ln.Split(' ');
            i1 = int.Parse(line[0]) - 1;
            i2 = int.Parse(line[1]) - 1;

            Node.connect(nodes[i1], nodes[i2]);

            yield return new WaitForSeconds(waitTimeLines);
        }

        foreach(Node node in nodes)
        {
            node.setClickable(true);
        }
    }

    public void reset()
    {
        scoreBoard.reset();
        timer.reset();
        stars.reset();
        StartCoroutine(resetEnum("lvl" + levelNumber));
    }

     IEnumerator resetEnum(string filename)
    {
        grid.setEnabled(false);
        StringReader reader = new StringReader(((TextAsset)Resources.Load(filename)).text);
        string[] line = reader.ReadLine().Split(' ');

        line = reader.ReadLine().Split(' ');
        line = reader.ReadLine().Split(' ');
        line = reader.ReadLine().Split(' ');

        int n = int.Parse(reader.ReadLine());
        
        for(int i=0; i<n; i++)
        {
            reader.ReadLine();
        }

        foreach(Node node in nodes)
        {
            reader.ReadLine();
        }

        string ln;
        int i1, i2;

        while((ln = reader.ReadLine()) != null)
        {
            line = ln.Split(' ');
            i1 = int.Parse(line[0]) - 1;
            i2 = int.Parse(line[1]) - 1;

            nodes[i1].addNewConnection(nodes[i2]);
            nodes[i2].addNewConnection(nodes[i1]);
        }

        foreach(Node node in nodes)
        {
            node.submitConnection();
        }

        grid.setEnabled(true);
        yield return null;
    }

    public IEnumerator moveGrid(float distance)
    {
        Vector2 start = Vector2.zero - new Vector2(0, Game.deltaHeight * 50);
        Vector2 deltaPos = Vector2.zero;
        Vector2 lastPos = deltaPos;
        Vector2 deltaDeltaPos = deltaPos - lastPos;

        Vector2 end = new Vector2(0, distance);

        for(int i=0; i<30; i++)
        {
            deltaPos = Vector2.Lerp(deltaPos, end, 0.1f);
            deltaDeltaPos = deltaPos - lastPos;
            start += deltaDeltaPos;
            lineParent.transform.localPosition += new Vector3(deltaDeltaPos.x, deltaDeltaPos.y, 0);
            lastPos = deltaPos;
            yield return new WaitForFixedUpdate();
        }
    }

    public void resetGraph()
    {
        lineParent.transform.SetParent(gameObject.transform);
        lineParent.transform.localPosition = Vector3.zero;
        lineParent.transform.localRotation = Quaternion.identity;
        lineParent.transform.localScale = Vector3.one;

        foreach(Transform child in lineParent.transform)
        {
            Destroy(child.gameObject);
        }

        grid = new GridLayout(0, 0, this);

        nodes = new List<Node>();

        loadLevel(levelNumber);
    }

    public void resetGraphT()
    {
        foreach(Transform child in lineParent.transform)
        {
            child.gameObject.AddComponent<Rigidbody2D>();
            Rigidbody2D rb = child.gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 20;
            rb.mass = 2;
            rb.AddForce(new Vector2(Random.next(10,30), Random.next(50,200)), ForceMode2D.Impulse);
        }
    }

    public void resetNodesNotPosition()
    {
         foreach(Transform child in lineParent.transform)
        {
            Destroy(child.gameObject);
        }

        grid = new GridLayout(0, 0, this);

        nodes = new List<Node>();

        loadLevel(levelNumber);
    }
    public void won()
    {
        try
        {
            if(levelNumber == 0)
            {
                tutorial.allGraphics[0].SetBool("Exit", true);
            }
            else if(levelNumber == 1)
            {
                tutorial.allGraphics[1].SetBool("Exit", true);
            }
            else if(levelNumber == 2)
            {
                tutorial.allGraphics[2].SetBool("Exit", true);
                tutorial.allGraphics[3].SetBool("Exit", true);
            }
        }
        catch(System.Exception){}

        float score = getScore();
        int nb = levelNumber - 1;
        levelManager.onLevelDone(nb, score);
        gameWonController.animate();
    }

    public float getScore()
    {
        if(levelNumber == 0)
            return 100000;
        
        int score = scoreBoard.getScore() * scoreBoard.getScore() * timer.getTime();
        score = score < targetScore ? targetScore : score;

        return Mathf.Clamp((float)targetScore * (100.0f / levelNumber) / score, 0, 1);
    }

    public void stopSong()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void playSong()
    {
        GetComponent<AudioSource>().clip = music[Random.next(0,music.Length)];
        GetComponent<AudioSource>().Play();
    }
}