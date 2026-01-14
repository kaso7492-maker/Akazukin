using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOutScript : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeSpeed = 1f;
    public string nextSceneName;

    bool isFading = false;

    void Start()
    {
        // 念のため最初は透明
        if (fadeCanvasGroup != null)
            fadeCanvasGroup.alpha = 0f;
    }

    // 外部（ボタンなど）から呼ぶ
    public void StartFadeOut()
    {
        if (!isFading)
            StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        isFading = true;

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
