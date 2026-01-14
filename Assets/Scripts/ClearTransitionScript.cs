using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearTransitionScript : MonoBehaviour
{
    [Header("Scene")]
    public string nextSceneName = "GameCreal1";

    [Header("References (GameScene2 only)")]
    public TimeManagerScript timeManager;
    public GameObject itemWolfSpawner;
    public PlayerMoveScript playerMoveScript;

    [Header("Player")]
    public Transform player;
    public float playerMoveSpeed = 3f;

    [Header("Transition")]
    public RectTransform transitionRect;
    public CanvasGroup transitionCanvasGroup;
    public float transitionMoveSpeed = 600f;
    public float fadeSpeed = 1.5f;

    bool timeUp = false;
    bool sceneChanging = false;

    void Start()
    {
        // Player 自動取得（未設定対策）
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        // GameCreal1 側初期化
        if (timeManager == null && transitionCanvasGroup != null)
        {
            transitionCanvasGroup.gameObject.SetActive(true);
            transitionCanvasGroup.alpha = 1f;
        }
    }

    void Update()
    {
        // =====================
        // GameScene2
        // =====================
        if (timeManager != null)
        {
            if (!timeUp && timeManager.remainingTime <= 0)
            {
                OnTimeUp();
            }

            if (timeUp && player != null)
            {
                MovePlayer();

                if (player.position.y >= 3f)
                {
                    MoveTransitionUp();
                }
            }
        }
        // =====================
        // GameCreal1
        // =====================
        else
        {
            FadeOut();
        }
    }

    // --------------------
    // TimeUp 処理
    // --------------------
    void OnTimeUp()
    {
        timeUp = true;

        // Spawner 非表示
        if (itemWolfSpawner != null)
            itemWolfSpawner.SetActive(false);

        // Item / Wolf 削除
        DestroyAllWithTag("Item");
        DestroyAllWithTag("Wolf");

        // Player 操作不可
        if (playerMoveScript != null)
            playerMoveScript.enabled = false;
    }

    // --------------------
    // Player 移動 (0,10)
    // --------------------
    void MovePlayer()
    {
        Vector3 target = new Vector3(0f, 10f, player.position.z);
        player.position = Vector3.MoveTowards(
            player.position,
            target,
            playerMoveSpeed * Time.deltaTime
        );
    }

    // --------------------
    // Transition 上移動
    // --------------------
    void MoveTransitionUp()
    {
        if (sceneChanging || transitionRect == null) return;

        transitionRect.anchoredPosition += Vector2.up * transitionMoveSpeed * Time.deltaTime;

        if (transitionRect.anchoredPosition.y >= 500f)
        {
            sceneChanging = true;
            SceneManager.LoadScene(nextSceneName);
        }
    }

    // --------------------
    // フェードアウト
    // --------------------
    void FadeOut()
    {
        if (transitionCanvasGroup == null) return;

        transitionCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;

        if (transitionCanvasGroup.alpha <= 0f)
        {
            transitionCanvasGroup.alpha = 0f;
            transitionCanvasGroup.gameObject.SetActive(false);
        }
    }

    // --------------------
    // タグ削除
    // --------------------
    void DestroyAllWithTag(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }
    }
}
