using UnityEngine;
using TMPro;
using System.Collections;

public class ThreeCountScript : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text countText;                // 3�J�E���g�\���p�e�L�X�g
    public CanvasGroup fadeCanvasGroup;       // �t�F�[�h�p�p�l��

    [Header("Gameplay Objects")]
    public GameObject itemWolfSpawner;        // �X�|�[���Ǘ�
    public MonoBehaviour playerMoveScript;    // PlayerMoveScript
    public TimeManagerScript timeManager;     // TimeBar�������X�N���v�g

    [Header("Player Rigidbody")]
    public GameObject player;                 // �v���C���[�{�́iRigidbody2D �K�{�j
    private Rigidbody2D rb;

    [Header("Settings")]
    public float fadeSpeed = 1f;              // �t�F�[�h�C�����x
    public float countDuration = 0.5f;          // 1�J�E���g�̕\������

    void Start()
    {
        // Rigidbody2D ���擾
        if (player != null)
        {
            rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.simulated = false; // �J�E���g���͓����Ȃ�
            }
        }

        // MoveScript ��~
        if (playerMoveScript != null)
            playerMoveScript.enabled = false;

        // TimeManager ��~
        if (timeManager != null)
            timeManager.enabled = false;

        // Spawner ��\��
        if (itemWolfSpawner != null)
            itemWolfSpawner.SetActive(false);

        // CountText ��\��
        if (countText != null)
            countText.gameObject.SetActive(false);

        // �t�F�[�h�J�n
        if (fadeCanvasGroup != null)
            fadeCanvasGroup.alpha = 1f;

        StartCoroutine(FadeInAndCount());
    }

    IEnumerator FadeInAndCount()
    {
        // �@ �t�F�[�h�C��
        if (fadeCanvasGroup != null)
        {
            while (fadeCanvasGroup.alpha > 0f)
            {
                fadeCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
                yield return null;
            }
            fadeCanvasGroup.alpha = 0f;
        }

        // �A 3�J�E���g�\��
        if (countText != null)
        {
            string[] counts = { "3", "2", "1", "Go!" };
            countText.gameObject.SetActive(true);

            foreach (string c in counts)
            {
                countText.text = c;
                yield return new WaitForSeconds(countDuration);
            }

            countText.gameObject.SetActive(false);
        }

        // �B �Q�[���J�n
        if (timeManager != null)
            timeManager.enabled = true;

        if (itemWolfSpawner != null)
            itemWolfSpawner.SetActive(true);

        if (playerMoveScript != null)
            playerMoveScript.enabled = true;

        if (rb != null)
            rb.simulated = true; // �J�E���g�I����ɓ�����
    }
}
