using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TimeManagerScript : MonoBehaviour
{
    public float limitTime = 30f;              // 制限時間（30秒）
    public float remainingTime;                       // 残り時間
    float initialBarWidth;                     // 最初のTimeBarの幅

    public RectTransform timeBar;              // TimeBarオブジェクト
    //public GameObject endNoticeImage;          // EndNotice（画像）
    //public GameObject player;                  // Playerオブジェクト

    void Start()
    {
        // 残り時間を制限時間で初期化
        remainingTime = limitTime;

        // TimeBar の最初の幅を記録
        initialBarWidth = timeBar.sizeDelta.x;

        // EndNotice の非表示
        //endNoticeImage.SetActive(false);
    }

    void Update()
    {
        if (remainingTime > 0f)
        {
            // 時間経過
            remainingTime -= Time.deltaTime;

            // TimeBarの長さを更新
            float newWidth = (remainingTime / limitTime) * initialBarWidth;
            timeBar.sizeDelta = new Vector2(newWidth, timeBar.sizeDelta.y);

            // 幅が 0 を下回らないように
            if (remainingTime <= 0f)
            {
                remainingTime = 0f;
                //StartCoroutine(TimeUp());
            }
        }
    }

    /*
    IEnumerator TimeUp()
    {
        // Player を消失
        Destroy(player);

        // EndNotice を表示
        endNoticeImage.SetActive(true);

        // 1秒待つ
        yield return new WaitForSeconds(2f);

        // ScoreScene に移行
        SceneManager.LoadScene("ScoreScene");
    }
    */
}
