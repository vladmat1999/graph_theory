using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceColliders : MonoBehaviour
{
    public BoxCollider2D collider1;
    public BoxCollider2D collider2;
    public RectTransform scrollView;
    public float amount = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 size = new Vector2(collider1.size.x, scrollView.sizeDelta.y / 2 * amount);
        collider1.size = size;
        collider2.size = size;
        collider1.offset = new Vector2(0, scrollView.sizeDelta.y / 2 * (1-amount) + collider1.size.y / 2);
        collider2.offset = new Vector2(0, -scrollView.sizeDelta.y / 2 * (1-amount) - collider1.size.y / 2);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Audio.playClick();
    }
}
