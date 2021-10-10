using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWonAnimation : MonoBehaviour
{
    public Text t1;
    public Text t2;
    public Text t3;
    public static bool isOn = false;

    public void animate()
    {
        StartCoroutine(animateenum());
        isOn = true;
    }

    public void dissappear()
    {
        t1.gameObject.SetActive(false);
        t2.gameObject.SetActive(false);
        t3.gameObject.SetActive(false);
        isOn = false;
    }

    public IEnumerator animateenum()
    {
        t1.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        t2.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        t3.gameObject.SetActive(true);
    }

}
