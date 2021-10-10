using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioner : MonoBehaviour
{
    public Vector3 position;
    public Vector3 scale;
    public Vector3 percent;
    public Vector3 topBottom;
    public Vector3 resolution;
    // Start is called before the first frame update
    void Start()
    {
        resolution = new Vector2(GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width, GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height);
    
        resolution = new Vector2(GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width, GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height);
        Vector3 tp = position + new Vector3(resolution.x * percent.x,
                                            resolution.y * percent.y,
                                            resolution.z * percent.z) / 100 - resolution / 2 + topBottom;
        tp.z = -1 + position.z;
        transform.localPosition = tp;

        transform.localScale = scale * Game.deltaWidth;
    }
}
