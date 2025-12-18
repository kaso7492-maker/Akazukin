using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(OnClickStart);
    }

    void OnClickStart()
    {
        SceneManager.LoadScene("GameScene1");
    }
}
