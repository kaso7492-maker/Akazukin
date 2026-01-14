using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOutScript : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;  // FadePanel の CanvasGroup
    public float fadeSpeed = 1f;

    bool isFading = false;

    // 外部から呼ぶ（Scene名を渡す）
    public void FadeToScene(string nextSceneName)
    {
        if (!isFading)
            StartCoroutine(FadeAndLoad(nextSceneName));
    }

    IEnumerator FadeAndLoad(string nextSceneName)
    {
        isFading = true;

        // フェードアウト
        while (fadeCanvasGroup.alpha < 1f)
        {
            fadeCanvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;

        // フェード完了後にシーン遷移
        SceneManager.LoadScene(nextSceneName);
    }
}
