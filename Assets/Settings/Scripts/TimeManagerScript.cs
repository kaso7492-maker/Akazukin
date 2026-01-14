using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimeManagerScript : MonoBehaviour
{
    public float limitTime = 30f;
    public float remainingTime;
    float initialBarWidth;

    public RectTransform timeBar;

    // ===== 追加 =====
    [Header("Clear")]
    public GameObject clearTransition;     // ClearTransition画像
    public CanvasGroup fadeCanvasGroup;    // フェード用
    public float fadeSpeed = 1f;

    bool isTimeUp = false;

    // ★ シーン間で共有
    public static float sharedRemainingTime = -1f;

    void Start()
    {
        if (sharedRemainingTime < 0f)
            remainingTime = limitTime;
        else
            remainingTime = sharedRemainingTime;

        initialBarWidth = timeBar.sizeDelta.x;
        UpdateTimeBar();

        if (clearTransition != null)
            clearTransition.SetActive(false);

        if (fadeCanvasGroup != null)
            fadeCanvasGroup.alpha = 0f;
    }

    void Update()
    {
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0f)
            {
                remainingTime = 0f;
                UpdateTimeBar();
                sharedRemainingTime = remainingTime;

                if (!isTimeUp)
                    StartCoroutine(TimeUpSequence());
            }
            else
            {
                UpdateTimeBar();
                sharedRemainingTime = remainingTime;
            }
        }
    }

    IEnumerator TimeUpSequence()
    {
        isTimeUp = true;

        // ClearTransition 表示
        if (clearTransition != null)
            clearTransition.SetActive(true);

        // 2秒待つ
        yield return new WaitForSeconds(2f);

        // フェードアウト
        while (fadeCanvasGroup.alpha < 1f)
        {
            fadeCanvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;

        // ===== スコア判定 =====
        int score = 0;
        if (ScoreManagerScript.instance != null)
            score = ScoreManagerScript.instance.score;

        if (score >= 100)
            SceneManager.LoadScene("GameClear1");
        else
            SceneManager.LoadScene("GameClear2");
    }

    void UpdateTimeBar()
    {
        float ratio = remainingTime / limitTime;
        float newWidth = ratio * initialBarWidth;
        timeBar.sizeDelta = new Vector2(newWidth, timeBar.sizeDelta.y);
    }
}
