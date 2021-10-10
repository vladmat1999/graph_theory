using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUp : MonoBehaviour
{
    public BoxCollider2D newCollider;
    public RectTransform scrollView;
    public float amount = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 size = new Vector2(newCollider.size.x, scrollView.sizeDelta.y / 2 * amount);
        newCollider.size = size;
        newCollider.offset = new Vector2(0, scrollView.sizeDelta.y / 2 * (1-amount) + newCollider.size.y / 2 + 10);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Audio.playClick();
    }
}
