using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksPositioner : MonoBehaviour
{
    public Vector3 position;
    public Vector3 scale;
    public Vector3 percent;
    public Vector3 topBottom;
    public Vector3 resolution;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        resolution = new Vector2(canvas.GetComponent<RectTransform>().rect.width, canvas.GetComponent<RectTransform>().rect.height);

        resolution = new Vector2(canvas.GetComponent<RectTransform>().rect.width, canvas.GetComponent<RectTransform>().rect.height);
        Vector3 tp = position + new Vector3(resolution.x * percent.x,
                                            resolution.y * percent.y,
                                            resolution.z * percent.z) / 100 - resolution / 2 + topBottom;
        tp.z = -1;
        transform.localPosition = tp;
    }

}