using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemWolfSpawnerScript : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject wolfPrefab;
    public GameObject flowerPrefab;
    public GameObject cakePrefab;
    public GameObject winePrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 1.5f;
    public float spawnXRange = 2.5f;
    public float spawnY = 6f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        int wolfRarity = 0;
        int flowerRarity = 50;
        int cakeRarity = 30;
        int wineRarity = 20;

        // GameScene3 �̏ꍇ
        if (SceneManager.GetActiveScene().name == "GameScene3")
        {
            spawnInterval = 0.8f;

            wolfRarity = 15;
            flowerRarity = 30;
            cakeRarity = 30;
            wineRarity = 25;
        }

        int total = wolfRarity + flowerRarity + cakeRarity + wineRarity;
        int rand = Random.Range(0, total);

        GameObject spawnObj = null;

        if (rand < wolfRarity)
        {
            spawnObj = wolfPrefab;
        }
        else if (rand < wolfRarity + flowerRarity)
        {
            spawnObj = flowerPrefab;
        }
        else if (rand < wolfRarity + flowerRarity + cakeRarity)
        {
            spawnObj = cakePrefab;
        }
        else
        {
            spawnObj = winePrefab;
        }

        float x = Random.Range(-spawnXRange, spawnXRange);
        Instantiate(spawnObj, new Vector3(x, spawnY, 0f), Quaternion.identity);
    }
}
