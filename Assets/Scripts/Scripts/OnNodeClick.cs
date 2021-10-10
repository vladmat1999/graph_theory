using UnityEngine;

public class OnNodeClick : MonoBehaviour
{
    public static NumberContainer counter;
    public static Game game;
    public static Timer timer;
    public bool isActive = false;
    public AudioSource audioSource;
    public void OnMouseDown()
    {
        audioSource.Play();

        if(isActive)
        {
            Node node = gameObject.GetComponent<Node>();
            node.disconnectAll();
            counter.next();

            if(Node.isPretty)
                foreach(Node n in Game.nodes)
                {
                    if(n.isThisPretty)
                        n.setPretty(false);
                }

            if(Solver.test())
            {
                StartCoroutine(game.grid.prettify(Vector2.zero - new Vector2(0, Game.deltaHeight * 30), new Vector2(Game.deltaWidth * 350, Game.deltaWidth * 350)));
                game.won();
                timer.stop();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(counter == null)
            counter = GameObject.Find("NumberContainer").GetComponent<NumberContainer>();

        if(game == null)
            game = GameObject.Find("Canvas").GetComponent<Game>();

        if(timer == null)
            timer = GameObject.Find("Timer").GetComponent<Timer>();
    }
}
