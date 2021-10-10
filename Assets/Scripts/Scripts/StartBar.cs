using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBar : MonoBehaviour
{
    private static Vector3 startPos = new Vector3(-10, -1.73f, 0.2f);
    private static Vector3 endPos = new Vector3(-3.5f, -1.73f, 0.2f);
    public ParticleSystem startBurst;
    public void move(float f)
    {
        StartCoroutine(moveEnum(f));
    }
    public void reset()
    {
        gameObject.transform.localPosition = startPos;
    }

    private IEnumerator moveEnum(float distance)
    {
        Vector3 temp = new Vector3(-11, -1.73f, 0.2f);
        Vector3 newEnd = Vector3.Lerp(startPos, endPos, distance);

        for(int i=0; i<30; i++)
        {
            temp = Vector3.Lerp(temp, newEnd, 0.1f);
            gameObject.transform.localPosition = temp;
            yield return new WaitForFixedUpdate();
        }

        if(distance > 0.95f)
        {
            startBurst.Play();
            GetComponent<AudioSource>().Play();
        }

        for(int i=0; i<30; i++)
        {
            temp = Vector3.Lerp(temp, newEnd, 0.1f);
            gameObject.transform.localPosition = temp;
            yield return new WaitForFixedUpdate();
        }

    }
}
