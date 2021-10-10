using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public Animator animator;
    public GameObject a;
    private int number = 0;
    public Counter()
    {}

    public void next()
    {
        number = number % 10 + 1;
        animator.SetInteger("n", number); 
    }

    public void from5to0()
    {
        animator.SetInteger("n", 60); 
        number = 0;
    }

    public int get()
    {
        return number;
    }
    public void reset()
    {
        number = 0;
        animator.SetInteger("n", 0); 
    }

    public static GameObject createCounter()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/CounterPrefab") as GameObject);
        Counter c = go.GetComponent<Counter>();
        c.animator = go.GetComponent<Animator>();
        return go;
    }
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
}
