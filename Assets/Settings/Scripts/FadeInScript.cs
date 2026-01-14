using UnityEngine;
using System.Collections;

public class FadeInScript : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeSpeed = 1f;

    void Start()
    {
        // 最初は真っ黒
        if (fadeCanvasGroup != null)
            fadeCanvasGroup.alpha = 1f;

        // フェードイン開始
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        while (fadeCanvasGroup.alpha > 0f)
        {
            fadeCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
    }
}
