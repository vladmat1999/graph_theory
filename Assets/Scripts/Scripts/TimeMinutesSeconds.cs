using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMinutesSeconds : MonoBehaviour
{
    public int seconds1;
    public int seconds2;
    public int minutes1;
    public int minutes2;

    public void nextSecond()
    {
        seconds1++;
        if(seconds1 == 10)
        {
            seconds2++;
            seconds1 = 0;
        }
        if(seconds2 == 6)
        {
            seconds2 = 0;
            minutes1++;
        }
        if(minutes1 == 10)
        {
            minutes1 = 0;
            minutes2++;
        }
    }

    public void resetTime()
    {
        seconds1 = seconds2 = minutes1 = minutes2 = 0;
    }
}

