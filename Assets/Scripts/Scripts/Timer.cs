using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject s1;
    public GameObject s2;
    public GameObject m1;
    public GameObject m2;
    public GameObject dp;

    public Counter s1c;
    public Counter s2c;
    public Counter m1c;
    public Counter m2c;

    public Vector2 center;
    public int seconds1;
    public int seconds2;
    public int minutes1;
    public int minutes2;
    private IEnumerator coroutine;
    public static bool isStopped = true;


    // Start is called before the first frame update
    void Start()
    {
        reset();
        coroutine = a();
        if(isStopped)
        {
            StartCoroutine(coroutine);
            isStopped = false;
        }
    }

    public void reset()
    {
        resetTime();
        s1c.reset();
        s2c.reset();
        m1c.reset();
        m2c.reset();

        s1.SetActive(true);
        s2.SetActive(true);
        m1.SetActive(true);
        m2.SetActive(false);

        s1.GetComponent<RectTransform>().localPosition = center + new Vector2(10.5f, 0);
        s2.GetComponent<RectTransform>().localPosition = center + new Vector2(1.5f, 0);
        m1.GetComponent<RectTransform>().localPosition = center + new Vector2(-10.5f, 0);

        s1.GetComponent<RectTransform>().localScale = new Vector2(2, 2);
        s2.GetComponent<RectTransform>().localScale = new Vector2(2, 2);
        m1.GetComponent<RectTransform>().localScale = new Vector2(2, 2);
        m2.GetComponent<RectTransform>().localScale = new Vector2(2, 2);
    }

    void count()
    {
        seconds1++;
        s1c.next();
        
        if(seconds1 == 10)
        {
            seconds2++;
            seconds1 = 0;
            s2c.next();
        }
        if(seconds2 == 6)
        {
            seconds2 = 0;
            minutes1++;
            s2c.from5to0();
            m1c.next();
        }
        if(minutes1 == 10)
        {
            m2.SetActive(true);
            minutes1 = 0;
            minutes2++;
            m2c.next();
            StartCoroutine(moveMinutes());
        }
    }

    public void resetTime()
    {
        seconds1 = seconds2 = minutes1 = minutes2 = 0;
        s1c.reset();
        s2c.reset();
        m1c.reset();
        m2c.reset();
    }

    IEnumerator moveMinutes()
    {
        Vector2 s1end = new Vector2(12.5f, 0);
        Vector2 s2end = new Vector2(5.2f, 0);
        Vector2 m1end = new Vector2(-5.2f, 0);
        Vector2 m2end = new Vector2(-12.5f, 0);
        Vector2 endScale = s1.GetComponent<RectTransform>().localScale / 2 * 1.5f;

        Vector2 s1temp = Vector2.Lerp(s1.GetComponent<RectTransform>().localPosition, s1end, 0.1f);
        Vector2 s2temp = Vector2.Lerp(s2.GetComponent<RectTransform>().localPosition, s2end, 0.1f);
        Vector2 m1temp = Vector2.Lerp(m1.GetComponent<RectTransform>().localPosition, m1end, 0.1f);
        Vector2 m2temp = Vector2.Lerp(m2.GetComponent<RectTransform>().localPosition, m2end, 0.1f);
        Vector2 tempScale = Vector2.Lerp(s1.GetComponent<RectTransform>().localScale, endScale, 0.1f);

        for(int i=0; i<30; i++)
        {
            s1.GetComponent<RectTransform>().localPosition = s1temp;
            s2.GetComponent<RectTransform>().localPosition = s2temp;
            m1.GetComponent<RectTransform>().localPosition = m1temp;
            m2.GetComponent<RectTransform>().localPosition = m2temp;

            s1.GetComponent<RectTransform>().localScale = tempScale;
            s2.GetComponent<RectTransform>().localScale = tempScale;
            m1.GetComponent<RectTransform>().localScale = tempScale;
            m2.GetComponent<RectTransform>().localScale = tempScale;

            s1temp = Vector2.Lerp(s1temp, s1end, 0.1f);
            s2temp = Vector2.Lerp(s2temp, s2end, 0.1f);
            m1temp = Vector2.Lerp(m1temp, m1end, 0.1f);
            m2temp = Vector2.Lerp(m2temp, m2end, 0.1f);

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator a()
    {
        yield return new WaitForSeconds(1);
        
        while(true)
        {
            count();
            yield return new WaitForSeconds(1);
        }

    }

    public void stop()
    {
        StopCoroutine(coroutine);
        isStopped = true;
    }

    public void start()
    {
        if(isStopped)
        {
            StartCoroutine(coroutine);
            isStopped = false;
        }
    }

    public int getTime()
    {
        return seconds2 * 10 + seconds1 + minutes1 * 60 + minutes2 * 600;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
