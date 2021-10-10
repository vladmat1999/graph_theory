using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWonController : MonoBehaviour
{
    public ParticleSystem fireworks1;
    public ParticleSystem fireworks2;
    public ParticleSystem bigCircleParticles;
    public Animator animateBackground;
    public Animator cameraAnim;
    public static Color[] colors = {Color.red, Color.blue, Color.cyan, Color.green, Color.green, Color.yellow};
    public static Color mainColor;
    public static Color[] availableColors;
    public Game game;
    public GameObject lineParent;
    public GameObject canvas;
    public Animator retry;
    public StartBar stars;
    public AudioClip successFirst;
    public AudioClip applause;
    public AudioClip successTrumpets;
    public AudioSource successMusic;
    public AudioClip[] successMusicSounds;
    public Text text;
    public string[] messages = new string[]{"Well done!", "Good job!", "Congratulations!", "Sweet!", "Great!"};
    public Tutorial tutorial;
    public Material[] materials;
    public Color[][][] allColors = new Color[][][]{
        new Color[][]{
            new Color[]{Color.red, new Color(0, 0.81f, 1), Color.magenta, Color.yellow},
            new Color[]{Color.cyan, Color.yellow, Color.blue, Color.green}
        },
        new Color[][]{
            new Color[]{new Color(0.61f, 0, 1), new Color(0, 0.81f, 1), Color.red, new Color(1, 0.6f, 0, 1)},
            new Color[]{Color.cyan, Color.green, Color.yellow, Color.red,  new Color(1, 0.6f, 0, 1)}
        },
        new Color[][]{
            new Color[]{new Color(0, 0.27f, 1, 1), Color.red, Color.magenta, Color.yellow},
            new Color[]{Color.yellow, new Color(1, 0.6f, 0, 1), Color.red, Color.magenta, Color.cyan, Color.green}
        }
    };


    public void chooseColour()
    {
        int rnd = Random.next(0, allColors.Length);

        Color[][] set = allColors[rnd];

        for(int i=0; i<4; i++)
        {
            materials[i].color = set[0][i];
        }

        availableColors = set[1];
    }

    public void nextLevel()
    {
        if(Game.levelNumber == 0)
        {
            tutorial.completed();
        }
        AdManager.showAd();
        Game.levelNumber++;
        lineParent.transform.SetParent(gameObject.transform.GetChild(0).transform);
        animateBackground.SetBool("NextLevel", true);
        cameraAnim.SetBool("NextLevel", true);
        bigCircleParticles.Play();
        StartCoroutine(AudioControl.FadeOut(successMusic, 1));
        game.playSong();
    }

    public void retryLevel()
    {
        lineParent.transform.SetParent(gameObject.transform.GetChild(0).transform);
        animateBackground.SetBool("NextLevel", true);
        cameraAnim.SetBool("NextLevel", true);
        bigCircleParticles.Play();
        StartCoroutine(AudioControl.FadeOut(successMusic, 1));
        game.playSong();
    }

    public void animate()
    {
        StartCoroutine(animateEnum());
    }

    IEnumerator animateEnum()
    {
        text.text = messages[Random.next(0,messages.Length)];
        game.stopSong();
        playNextSong();
        //yield return new WaitForSeconds(0.4f);
        GetComponent<AudioSource>().PlayOneShot(successFirst);
        bigCircleParticles.Stop();
        yield return new WaitForSeconds(0.2f);
        fireworks2.Play();
        //fireworks2.gameObject.GetComponent<AudioSource>().Play();
        fireworks1.Play();
        //fireworks1.gameObject.GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().PlayOneShot(successTrumpets);
        yield return new WaitForSeconds(0.5f);
        game.topBar.retract();
        animateBackground.SetBool("Won", true);
        //yield return new WaitForSeconds(0.2f);
        StartCoroutine(game.moveGrid(Game.resolution.y * 0.3f));
        GetComponent<AudioSource>().PlayOneShot(applause);
        yield return new WaitForSeconds(0.4f);
        stars.move(game.getScore());
        yield return new WaitForSeconds(0.2f);
        retry.SetBool("Ready", true);
    }

    public void playNextSong()
    {
        successMusic.clip = successMusicSounds[Random.next(0, successMusicSounds.Length)];
        successMusic.Play();
    }
}
