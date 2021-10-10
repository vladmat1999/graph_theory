using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberContainer : MonoBehaviour
{
    public GameObject u, z, s;
    public Counter uc, zc, sc;
    public Vector2 center;
    public Vector2 resolution;
    private int number = 0;
    public void next()
    {
        number++;
        if(number < 10)
            uc.next();
        else if(number == 10)
        {
            uc.next();
            z.SetActive(true);
            StartCoroutine(moveTens());
            zc.next();
        }
        else if(number < 100 && number > 10 && number % 10 == 0)
        {
            uc.next();
            zc.next();
        }
        else if(number < 100 && number > 10)
        {
            uc.next();
        }
        else if(number == 100)
        {
            s.SetActive(true);
            StartCoroutine(moveHundreds());
            uc.next();
            zc.next();
            sc.next();
        }
        
        
        else if(number > 100 && number != 100 && number % 100 == 0)
        {
            uc.next();
            zc.next();
            sc.next();
        }
        
        else if(number > 100 && number % 10 == 0)
        {
            uc.next();
            zc.next();
        }
        else if(number > 100)
        {
            uc.next();
        }
    }

    IEnumerator moveTens()
    {
        Vector2 end1 = new Vector2(3f, 0);
        Vector2 end2 = new Vector2(-3f, 0);
        Vector2 temp1 = Vector2.Lerp(center, end1, 0.1f);
        Vector2 temp2 = Vector2.Lerp(center, end2, 0.1f);

        for(int i=0; i<30; i++)
        {
             u.GetComponent<RectTransform>().localPosition = temp1;
             z.GetComponent<RectTransform>().localPosition = temp2;
             temp1 = Vector2.Lerp(temp1, end1, 0.1f);
             temp2 = Vector2.Lerp(temp2, end2, 0.1f);
             yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator moveHundreds()
    {
        Vector2 end1 = new Vector2(6f, 0);
        Vector2 end2 = new Vector2(0, 0);
        Vector2 end3 = new Vector2(-6f, 0);
        Vector2 endscale = u.GetComponent<RectTransform>().localScale *= 0.7f;;
        
        Vector2 temp1 = Vector2.Lerp(u.GetComponent<RectTransform>().localPosition, end1, 0.1f);
        Vector2 temp2 = Vector2.Lerp(z.GetComponent<RectTransform>().localPosition, end2, 0.1f);
        Vector2 temp3 = Vector2.Lerp(s.GetComponent<RectTransform>().localPosition, end3, 0.1f);
        Vector2 tempScale = Vector2.Lerp(u.GetComponent<RectTransform>().localScale, endscale, 0.1f);

         for(int i=0; i<30; i++)
        {
             u.GetComponent<RectTransform>().localPosition = temp1;
             z.GetComponent<RectTransform>().localPosition = temp2;
             s.GetComponent<RectTransform>().localPosition = temp3;

             u.GetComponent<RectTransform>().localScale = tempScale;
             z.GetComponent<RectTransform>().localScale = tempScale;
             s.GetComponent<RectTransform>().localScale = tempScale;

             temp1 = Vector2.Lerp(temp1, end1, 0.1f);
             temp2 = Vector2.Lerp(temp2, end2, 0.1f);
             temp3 = Vector2.Lerp(temp3, end3, 0.1f);
             tempScale = Vector2.Lerp(tempScale, endscale, 0.1f);
             yield return new WaitForFixedUpdate();
        }

    }

    public void reset()
    {
        number = 0;
        uc.reset();
        zc.reset();
        sc.reset();

        u.SetActive(true);
        z.SetActive(false);
        s.SetActive(false);
        u.GetComponent<RectTransform>().localPosition = center;
        
        u.GetComponent<RectTransform>().localScale = new Vector2(1.61f, 1.61f);
        z.GetComponent<RectTransform>().localScale = new Vector2(1.61f, 1.61f);
        s.GetComponent<RectTransform>().localScale = new Vector2(1.61f, 1.61f);
    }
    // Start is called before the first frame update
    void Start()
    {
        number = 0;
        uc.reset();
        zc.reset();
        sc.reset();

        u.GetComponent<RectTransform>().localScale = new Vector2(1.61f, 1.61f);
        z.GetComponent<RectTransform>().localScale = new Vector2(1.61f, 1.61f);
        s.GetComponent<RectTransform>().localScale = new Vector2(1.61f, 1.61f);

        u.SetActive(true);
        z.SetActive(false);
        s.SetActive(false);
        u.GetComponent<RectTransform>().localPosition = center;
    }

    IEnumerator a()
    {
        yield return new WaitForSeconds(1);
        for(int i=0; i<97; i++)
        {
            next ();
            yield return new WaitForFixedUpdate();
        }
        next ();
            yield return new WaitForSeconds(1);
            next ();
            yield return new WaitForSeconds(1);
            next ();
            yield return new WaitForSeconds(1);
        reset();
        yield return new WaitForSeconds(1);
        for(int i=0; i<210; i++)
        {
            next ();
            yield return new WaitForSeconds(0.2f);
        }
           
    }

    public int getScore()
    {
        return number;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
