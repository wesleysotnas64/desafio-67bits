using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FpsCalc : MonoBehaviour
{
    public float fps;
    public TMP_Text textFps;
    void Start()
    {
        StartCoroutine(CalcFpsEnum());
    }

    IEnumerator CalcFpsEnum()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            fps = 1.0f / Time.deltaTime;
            textFps.text = $"{Math.Floor(fps)} fps";
        }
    }

}
