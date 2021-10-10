using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarScript : MonoBehaviour
{
    public void retract()
    {
        Vector2 resolution = Game.resolution;
        float distance = resolution.y * 0.25f;
        StartCoroutine(retract(distance));
    }

    public void release()
    {
        Vector2 resolution = Game.resolution;
        float distance = 0;
        StartCoroutine(retract(distance));
    }

    public IEnumerator retract(float distance)
    {
        Vector2 start = gameObject.transform.localPosition;
        Vector2 end = new Vector2(0, distance);
        for(int i=0; i<30; i++)
        {
            start = Vector2.Lerp(start, end, 0.1f);
            gameObject.transform.localPosition = start;
            yield return new WaitForFixedUpdate();
        }
    }
}
