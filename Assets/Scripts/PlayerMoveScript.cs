using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float speed = 5f;   // 移動速度
    public float minX = -8f;   // 画面左端
    public float maxX = 8f;    // 画面右端

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        // 初期の y, z を固定値として保存
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void Update()
    {
        float moveX = 0f;

        // 左右キー入力
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;
        }

        // X方向へ移動
        float newX = transform.position.x + moveX * speed * Time.deltaTime;

        // 画面外に出ないよう制限
        newX = Mathf.Clamp(newX, minX, maxX);

        // y, z は固定
        transform.position = new Vector3(newX, fixedY, fixedZ);
    }
}
