using UnityEngine;
using UnityEngine.UI;

public class TitleButtonChoiceScript : MonoBehaviour
{
    [Header("Title Buttons")]
    public Button[] buttons;          // StartButton, HowToPlayButton

    [Header("Cursor")]
    public RectTransform cursor;
    public Vector2 cursorOffset = new Vector2(-50f, 0f);

    [Header("Instruction")]
    public GameObject instruction;    // Instruction オブジェクト

    private int currentIndex = 0;

    void Start()
    {
        if (buttons != null && buttons.Length > 0)
        {
            UpdateSelection();
        }
    }

    void Update()
    {
        // Instruction表示中は一切操作させない
        if (instruction != null && instruction.activeSelf)
        {
            return;
        }

        // 上キー
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = buttons.Length - 1;
            }
            UpdateSelection();
        }

        // 下キー
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;
            if (currentIndex >= buttons.Length)
            {
                currentIndex = 0;
            }
            UpdateSelection();
        }

        // 決定キー（スペース）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    void UpdateSelection()
    {
        buttons[currentIndex].Select();

        RectTransform buttonRect =
            buttons[currentIndex].GetComponent<RectTransform>();

        cursor.position = buttonRect.position;
        cursor.anchoredPosition += cursorOffset;
    }
}
