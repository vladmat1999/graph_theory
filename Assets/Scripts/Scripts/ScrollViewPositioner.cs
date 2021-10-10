using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewPositioner : MonoBehaviour
{
    Vector2 smallRes = new Vector2(193, 138);
    Vector2 bigRes = new Vector2(224, 160);
    public static float scrollY;

    void Start()
    {
        float ar = ScreenRes.width / ScreenRes.height;
        float m1 = (bigRes.x - smallRes.x) / (0.4736f - 9.0f / 16);
        float m2 = (bigRes.y - smallRes.y) / (0.4736f - 9.0f / 16);
        Vector2 m = new Vector2(m1, m2);

        Vector2 newRes = new Vector2(m.x * ar - m.x * 0.4736f + bigRes.x , m.y * ar - m.y * 0.4736f + bigRes.y);

        RectTransform t = gameObject.GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(t.sizeDelta.x, newRes.y);
        t.transform.localPosition = new Vector3(t.localPosition.x, newRes.x, t.localPosition.z);
        scrollY = gameObject.GetComponent<RectTransform>().sizeDelta.y; 
    }
}
