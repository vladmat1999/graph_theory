using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static float[] levels = new float[100];
    public static bool tutorialDone = false;
    public static int levelsSoFar = 0;
    public NewGridLayout newGrid;
    void Start()
    {
        loadLevels();

        int lvl = 99;
        for(int i=0; i<100; i++)
        {
            if(levels[i] <= 0)
                {
                    lvl = i;
                    break;
                }
        }

        tutorialDone = getTutorial();

        Game.levelNumber = lvl + 1;
    }

    public static void saveLevels()
    {
        for(int i=0; i<100; i++)
        {
            PlayerPrefs.SetFloat("" + i, levels[i]);
        }
        PlayerPrefs.Save();
    }

    public static void loadLevels()
    {
        for(int i=0; i<100; i++)
        {
            levels[i] = PlayerPrefs.GetFloat("" + i);
        }

        levelsSoFar = PlayerPrefs.GetInt("levelsSoFar");
    }

    public static void saveLevel(int i)
    {
        PlayerPrefs.SetFloat("" + i, levels[i]);
        PlayerPrefs.Save();
    }
    public void onLevelDone(int level, float score)
    {
        if(level >= 0)
        {
            if(score > levels[level])
                levels[level] = score;
            saveLevel(level);
            newGrid.setLevelCompletion(level, levels[level]);
        }

        levelsSoFar++;
        PlayerPrefs.SetInt("levelsSoFar", levelsSoFar);
        PlayerPrefs.Save();
    }

    public static void reset()
    {
        for(int i=0; i<100; i++)
            levels[i] = 0;

        levelsSoFar = 0;

        saveLevels();
    }

    public static bool getTutorial()
    {
        try
        {
            int n = PlayerPrefs.GetInt("Tutorial");
            return n==1 ? true : false;
        }
        catch(System.Exception)
        {
            PlayerPrefs.SetInt("Tutorial", 0);
            return false;
        }
    }

    public static void tutorialCompleted()
    {
        tutorialDone = true;
        PlayerPrefs.SetInt("Tutorial" , 1);
    }
}
