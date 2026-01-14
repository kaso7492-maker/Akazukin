using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [Header("Item Settings")]
    public int point;
    public float minSpeed;
    public float maxSpeed;

    private float speed;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // 画面外に出たら削除
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // スコア加算
            if (ScoreManagerScript.instance != null)
            {
                ScoreManagerScript.instance.AddScore(point);
            }

            Destroy(gameObject);
        }
    }

}
