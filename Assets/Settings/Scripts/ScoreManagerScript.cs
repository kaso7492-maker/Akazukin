using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManagerScript : MonoBehaviour
{
    public static ScoreManagerScript instance;

    public int score = 0;

    [Header("UI")]
    public TMP_Text scoreValueText;

    void Awake()
    {
        // シングルトン + シーン引継ぎ
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        UpdateScoreText();
    }

    // ★ シーン切替時の処理
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ===== TitleSceneならスコアリセット =====
        if (scene.name == "TitleScene")
        {
            score = 0;
        }

        FindScoreTextInScene();
        UpdateScoreText();
    }

    void FindScoreTextInScene()
    {
        GameObject obj = GameObject.Find("ScoreValue");
        if (obj != null)
        {
            scoreValueText = obj.GetComponent<TMP_Text>();
        }
        else
        {
            scoreValueText = null;
        }
    }

    public void AddScore(int point)
    {
        score += point;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreValueText != null)
        {
            scoreValueText.text = score.ToString();
        }
    }
}
