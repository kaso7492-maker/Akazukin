using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClearTransitionScript : MonoBehaviour
{
    [Header("Scene")]
    public string nextSceneName = "GameClear1";

    [Header("GameScene2 References")]
    public TimeManagerScript timeManager;
    public GameObject itemWolfSpawner;
    public PlayerMoveScript playerMoveScript;

    [Header("Clear Image")]
    public GameObject clearTransitionImage;   // 画像 ClearTransition

    [Header("Fade")]
    public CanvasGroup fadeCanvasGroup;
    public float fadeSpeed = 1.5f;

    bool isClearing = false;

    void Start()
    {
        // Clear画像は最初は非表示
        if (clearTransitionImage != null)
            clearTransitionImage.SetActive(false);

        // GameClear1 側の初期フェードイン設定
        if (timeManager == null && fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
        }
    }

    void Update()
    {
        // =========================
        // GameScene2
        // =========================
        if (timeManager != null)
        {
            if (!isClearing && timeManager.remainingTime <= 0)
            {
                StartCoroutine(ClearSequence());
            }
        }
        // =========================
        // GameClear1
        // =========================
        else
        {
            FadeIn();
        }
    }

    // -------------------------
    // TimeUp → フェードアウト
    // -------------------------
    IEnumerator ClearSequence()
    {
        isClearing = true;

        // ItemWolfSpawner 非表示
        if (itemWolfSpawner != null)
            itemWolfSpawner.SetActive(false);

        // Item / Wolf 削除
        DestroyByTag("Item");
        DestroyByTag("Wolf");

        // Player 入力停止
        if (playerMoveScript != null)
            playerMoveScript.enabled = false;

        // Clear画像表示
        if (clearTransitionImage != null)
            clearTransitionImage.SetActive(true);

        // 2秒待つ
        yield return new WaitForSeconds(2f);

        // フェードアウト開始
        while (fadeCanvasGroup.alpha < 1f)
        {
            fadeCanvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;

        SceneManager.LoadScene(nextSceneName);
    }

    // -------------------------
    // GameClear1 フェードイン
    // -------------------------
    void FadeIn()
    {
        if (fadeCanvasGroup == null) return;

        if (fadeCanvasGroup.alpha > 0f)
        {
            fadeCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
        }
        else
        {
            fadeCanvasGroup.alpha = 0f;
        }
    }

    // -------------------------
    // タグ削除
    // -------------------------
    void DestroyByTag(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }
    }
}
