using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random
{
    private static System.Random rnd = new System.Random();

    public static int next(int a, int b)
    {
        return rnd.Next(a,b);
    }
}
