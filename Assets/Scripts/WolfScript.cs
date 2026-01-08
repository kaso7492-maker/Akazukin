using UnityEngine;

public class WolfScript : MonoBehaviour
{
    [Header("Wolf Settings")]
    public float minSpeed = 3f;
    public float maxSpeed = 6f;

    private float speed;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // ‰æ–ÊŠO‚Éo‚½‚çíœ
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player‚ÉG‚ê‚½‚çÁ‹
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
