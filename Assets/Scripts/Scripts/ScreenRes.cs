using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRes : MonoBehaviour
{
    public static float width;
    public static float height;
    public static float deltaWidth;
    public static float deltaHeight;
    public static Vector2 resolution;
    void Start()
    {
        width = gameObject.GetComponent<RectTransform>().rect.width;
        height = gameObject.GetComponent<RectTransform>().rect.height;
        deltaWidth = width / 510;
        deltaHeight = height / 510;
        resolution = new Vector2(width, height);
    }
}
