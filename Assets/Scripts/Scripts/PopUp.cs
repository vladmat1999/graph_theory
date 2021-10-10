using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public Material material;
    public SpriteRenderer spriteRenderer;

    public void activate()
    {
        StartCoroutine(activateEnum());
    }

    public void deactivate()
    {
        StartCoroutine(deactivateEnum());
    }

    IEnumerator activateEnum()
    {
        float blurAmount = Game.deltaHeight * 3;
        float c = 0;
        float t = 0;
        Vector3 scale = new Vector3(0.08f, 0.08f, 1);

        for(int i=0; i<15; i++)
        {
            material.SetFloat(Shader.PropertyToID("_Size"), c);
            c = Mathf.Lerp(c , blurAmount, 0.3f);
            spriteRenderer.color = new Color(1,1,1,t);
            t = Mathf.Lerp(t, 1, 0.3f);
            spriteRenderer.gameObject.transform.localScale = scale;
            scale = Vector3.Lerp(scale, new Vector3(0.1f,0.1f,1),0.3f);

            foreach(Transform child in spriteRenderer.transform)
            {
                Color tmp = child.GetComponent<Text>().color; 
                child.GetComponent<Text>().color = new Color(tmp.r, tmp.g, tmp.b, t);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator deactivateEnum()
    {
        float blurAmount = Game.deltaHeight * 4;
        float c = blurAmount;
        float t = spriteRenderer.color.a;
        Vector3 scale = spriteRenderer.gameObject.transform.localScale;

        for(int i=0; i<15; i++)
        {
            material.SetFloat(Shader.PropertyToID("_Size"), c);
            c = Mathf.Lerp(c , 0, 0.3f);
            spriteRenderer.color = new Color(1,1,1,t);
            t = Mathf.Lerp(t,0,0.3f);
            spriteRenderer.gameObject.transform.localScale = scale;
            scale = Vector3.Lerp(scale, new Vector3(0.12f,0.12f,1),0.3f);

            foreach(Transform child in spriteRenderer.transform)
            {
                Color tmp = child.GetComponent<Text>().color; 
                child.GetComponent<Text>().color = new Color(tmp.r, tmp.g, tmp.b, t);
            }

            yield return new WaitForFixedUpdate();
        }
        material.SetFloat(Shader.PropertyToID("_Size"), 0);
        spriteRenderer.color = new Color(1,1,1,0);
        gameObject.SetActive(false);
    }

    public void OnMouseDown()
    {
        StartCoroutine(deactivateEnum());
        Audio.playClick();
    }
}
