using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewNode : MonoBehaviour
{
    public Vector2 position;
    public NewGridLayout grid;
    public int number;
    public bool completed = false;
    public bool hasPlayed = true;
    public void setGraphPosition(float x, float y)
    {
        position = new Vector2(x, y);
    }

    public void setPosition(float x, float y)
    {
        gameObject.transform.localPosition = new Vector2(x, y);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setNodeCompletion(float value)
    {
        if(value <= 0)
        {
            gameObject.transform.GetComponentInChildren<Text>().color = NewGridLayout.wonColor;
            gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.black;
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            completed = false;
        }

        else
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetComponentInChildren<Text>().color = Color.black;
            gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().color = new Color(0.83f,0.83f,0.83f);
            gameObject.transform.GetChild(2).transform.localPosition = new Vector3(0, -4.2f + 4.7f * value, -0.09f);
            completed = true;
        }
    }
    public void setText(int i)
    {
        gameObject.transform.GetComponentInChildren<Text>().text = "" + i;
    }

    public void setCurrentLevel(bool b)
    {
        gameObject.transform.GetChild(4).gameObject.SetActive(b);
    }

    public void OnMouseDown()
    {
        int number = int.Parse(gameObject.transform.GetComponentInChildren<Text>().text);

        if(number == 1)
        {
            GameObject.Find("List").GetComponent<List>().loadLevel(number);
            Audio.playClick();
            return;
        }
        
        else if(grid.nodes[number - 2].completed)
        {
            GameObject.Find("List").GetComponent<List>().loadLevel(number);
        }

        else  if(LevelManager.levels[number - 2] <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Forbidden"));
        }
    }

    public void playSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
        hasPlayed = true;
    }

}
