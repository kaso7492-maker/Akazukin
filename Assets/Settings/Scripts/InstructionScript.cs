using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionScript : MonoBehaviour
{
    [Header("HowTo Sprites")]
    public Sprite[] instructions;

    [Header("UI Parts")]
    public Image instructionImage;
    public GameObject nextButton;
    public GameObject backButton;
    public GameObject closeButton;
    public GameObject playButton;
    public FadeOutScript fadeOutManager; // FadeOutManager ÇÉhÉâÉbÉO

    private int currentIndex = 0;

    void Update()
    {
        if (!gameObject.activeSelf) return;

        // Å® Next
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (nextButton.activeSelf)
                OnClickNext();
        }

        // Å© Back / Close
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (backButton.activeSelf)
                OnClickBack();
            else if (closeButton.activeSelf)
                OnClickClose();
        }

        // Space Å® Play ÇÃÇ›
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playButton.activeSelf)
                OnClickPlay();
        }
    }

    public void OpenInstruction()
    {
        gameObject.SetActive(true);

        currentIndex = 0;
        instructionImage.sprite = instructions[currentIndex];

        UpdateButtons();
    }

    public void OnClickNext()
    {
        if (currentIndex < instructions.Length - 1)
        {
            currentIndex++;
            instructionImage.sprite = instructions[currentIndex];
            UpdateButtons();
        }
    }

    public void OnClickBack()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            instructionImage.sprite = instructions[currentIndex];
            UpdateButtons();
        }
    }

    public void OnClickClose()
    {
        gameObject.SetActive(false);
    }

    public void OnClickPlay()
    {
        fadeOutManager.FadeToScene("GameScene1");
    }

    private void UpdateButtons()
    {
        bool isFirst = currentIndex == 0;
        bool isLast = currentIndex == instructions.Length - 1;

        backButton.SetActive(!isFirst);
        closeButton.SetActive(isFirst);

        nextButton.SetActive(!isLast);
        playButton.SetActive(isLast);
    }
}
