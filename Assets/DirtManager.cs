using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtManager : MonoBehaviour
{
    public GameObject dirtPrefab;
    public GameObject playerCharacter; // Karakter objesini Inspector'dan atayın
    public float gameDuration = 60f;
    public float spawnInterval = 4f;
    public float dirtLifetime = 5f;

    private List<GameObject> activeDirts = new List<GameObject>();
    private float gameTimer;
    private float spawnTimer;
    private int score = 0;
    private Vector3 playerStartPosition; // Karakterin başlangıç pozisyonu

    // Sınırlar
    private float MinX = -500f;
    private float MaxX = -25f;
    private float spawnY = 37f;
    private float minZ = -262f;
    private float maxZ = 67f;

    void Start()
    {
        // Karakterin başlangıç pozisyonunu kaydet
        if (playerCharacter != null)
        {
            playerStartPosition = playerCharacter.transform.position;
        }
        else
        {
            Debug.LogError("Player Character atanmamış!");
        }

        gameTimer = gameDuration;
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        if (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                SpawnDirt();
                SpawnDirt();
                SpawnDirt();
                SpawnDirt();
                spawnTimer = spawnInterval;
            }
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Oyun Bitti! Skor: " + score);

        // Karakteri başlangıç pozisyonuna döndür
        if (playerCharacter != null)
        {
            playerCharacter.transform.position = playerStartPosition;
        }

        // Skoru sıfırla
        score = 0;

        // Aktif tüm kirleri temizle
        foreach (var dirt in activeDirts)
        {
            if (dirt != null)
            {
                Destroy(dirt);
            }
        }
        activeDirts.Clear();

        // Oyunu yeniden başlat
        gameTimer = gameDuration;
        spawnTimer = spawnInterval;

        Debug.Log("Oyun yeniden başlatıldı!");
    }

    void SpawnDirt()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(MinX, MaxX),
            spawnY,
            Random.Range(minZ, maxZ)
        );

        GameObject newDirt = Instantiate(dirtPrefab, spawnPosition, Quaternion.identity);
        activeDirts.Add(newDirt);
        StartCoroutine(DestroyDirtAfterTime(newDirt, dirtLifetime));
        Debug.Log($"Yeni kir: {spawnPosition}");
    }

    IEnumerator DestroyDirtAfterTime(GameObject dirt, float time)
    {
        yield return new WaitForSeconds(time);

        if (dirt != null && activeDirts.Contains(dirt))
        {
            activeDirts.Remove(dirt);
            Destroy(dirt);
        }
    }

    public void CleanDirt(GameObject dirt)
    {
        if (activeDirts.Contains(dirt))
        {
            activeDirts.Remove(dirt);
            Destroy(dirt);
            score++;
            Debug.Log("Skor: " + score);
        }
    }
}