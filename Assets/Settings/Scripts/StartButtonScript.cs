using UnityEngine;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    public Button startButton;
    public FadeOutScript fadeOutManager; // Å© í«â¡

    void Start()
    {
        startButton.onClick.AddListener(OnClickStart);
    }

    void OnClickStart()
    {
        fadeOutManager.FadeToScene("GameScene1");
    }
}
