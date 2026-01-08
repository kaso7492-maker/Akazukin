using UnityEngine;
using UnityEngine.SceneManagement;

public class CrealTransitionScript : MonoBehaviour
{
    [Header("References")]
    public TimeManagerScript timeManager;
    public GameObject itemWolfSpawner;
    public GameObject player;

    [Header("Transition")]
    public RectTransform transitionRect;
    public CanvasGroup transitionCanvasGroup;

    [Header("Move Settings")]
    public float playerMoveSpeed = 5f;
    public float transitionMoveSpeed = 500f;

    private bool timeUp = false;
    private bool transitionMoving = false;
    private bool sceneLoading = false;

    void Update()
    {
        TimeUpCheck();

        if (timeUp)
        {
            MovePlayerToTarget();
            CheckPlayerHeight();
        }

        if (transitionMoving)
        {
            MoveTransitionUp();
        }
    }

    void Awake()
    {
        // GameScene2 開始時は非表示
        transitionCanvasGroup.alpha = 0f;
        transitionCanvasGroup.gameObject.SetActive(false);
    }


    // remainingTime が 0 になったか確認
    void TimeUpCheck()
    {
        if (timeUp) return;

        if (timeManager.remainingTime <= 0f)
        {
            timeUp = true;

            // ItemWolfSpawner を非表示
            if (itemWolfSpawner != null)
            {
                itemWolfSpawner.SetActive(false);
            }

            // Item と Wolf を全消去
            DestroyAllByTag("Item");
            DestroyAllByTag("Wolf");

            // PlayerMoveScript を無効化（★方法①の核心）
            PlayerMoveScript move = player.GetComponent<PlayerMoveScript>();
            if (move != null)
            {
                move.enabled = false;
            }
        }
    }

    // Player を (0,10) へ移動
    void MovePlayerToTarget()
    {
        Vector3 target = new Vector3(0f, 10f, player.transform.position.z);
        player.transform.position = Vector3.MoveTowards(
            player.transform.position,
            target,
            playerMoveSpeed * Time.deltaTime
        );
    }

    // Player の y >= 3 を確認
    void CheckPlayerHeight()
    {
        if (player.transform.position.y >= 3f)
        {
            transitionMoving = true;
        }
    }

    // Transition を y=500 まで移動
    void MoveTransitionUp()
    {
        Vector2 pos = transitionRect.anchoredPosition;
        pos.y += transitionMoveSpeed * Time.deltaTime;
        transitionRect.anchoredPosition = pos;

        if (transitionRect.anchoredPosition.y >= 500f && !sceneLoading)
        {
            sceneLoading = true;
            SceneManager.LoadScene("GameCreal1");
        }
    }

    // Item / Wolf をタグで全削除
    void DestroyAllByTag(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }
    }

    // ===== GameCreal1 用 =====
    void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "GameCreal1")
        {
            StartCoroutine(FadeOut());
        }
    }

    System.Collections.IEnumerator FadeOut()
    {
        transitionCanvasGroup.alpha = 1f;
        transitionCanvasGroup.gameObject.SetActive(true);

        while (transitionCanvasGroup.alpha > 0f)
        {
            transitionCanvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        transitionCanvasGroup.alpha = 0f;
        transitionCanvasGroup.gameObject.SetActive(false);
    }
}
