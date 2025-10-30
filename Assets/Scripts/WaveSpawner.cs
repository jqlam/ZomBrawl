using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Wave Size")]
    public int baseCount = 6;
    public int perWaveIncrement = 3;

    [Header("Timing")]
    public float timeBetweenWaves = 1.0f;

    [Header("Spawn Placement")]
    public float spawnPadding = 1.5f;       // off-screen padding
    public bool spawnInsideForDebug = false; // toggle ON to see spawns immediately

    private int currentWave = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        UIManager.Instance.UpdateWave(currentWave);

        while (true)
        {
            if (!isSpawning && IsRoundClear())
            {
                yield return new WaitForSeconds(timeBetweenWaves);
                SpawnWave();
            }
            yield return null;
        }
    }

    bool IsRoundClear()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    void SpawnWave()
    {
        isSpawning = true;

        currentWave++;
        UIManager.Instance.UpdateWave(currentWave);

        int toSpawn = baseCount + (currentWave - 1) * perWaveIncrement;

        for (int i = 0; i < toSpawn; i++)
        {
            Vector3 pos = spawnInsideForDebug ? RandomSpawnInsideCamera(0.8f) : RandomOffscreenSpawn();
            var go = Instantiate(enemyPrefab, pos, Quaternion.identity);
            go.tag = "Enemy";

            var rb = go.GetComponent<Rigidbody2D>();
            if (rb) rb.gravityScale = 0f;

            var enemy = go.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.maxHealth += currentWave * 2;
                enemy.speed += Mathf.Min(0.02f * currentWave, 1.5f);
            }
        }

        isSpawning = false;
    }

    Vector3 RandomOffscreenSpawn()
    {
        Camera cam = Camera.main;
        float h = cam.orthographicSize;
        float w = h * cam.aspect;

        int edge = Random.Range(0, 4);
        float x, y;
        switch (edge)
        {
            case 0: x = -w - spawnPadding;  y = Random.Range(-h, h);  break; // left
            case 1: x =  w + spawnPadding;  y = Random.Range(-h, h);  break; // right
            case 2: x = Random.Range(-w, w); y =  h + spawnPadding;   break; // top
            default:x = Random.Range(-w, w); y = -h - spawnPadding;   break; // bottom
        }
        Vector3 c = cam.transform.position;
        return new Vector3(c.x + x, c.y + y, 0f);
    }

    Vector3 RandomSpawnInsideCamera(float marginFactor)
    {
        Camera cam = Camera.main;
        float h = cam.orthographicSize;
        float w = h * cam.aspect;
        Vector3 c = cam.transform.position;

        float mx = Mathf.Clamp01(marginFactor);
        float innerW = w * mx;
        float innerH = h * mx;

        float x = Random.Range(c.x - innerW, c.x + innerW);
        float y = Random.Range(c.y - innerH, c.y + innerH);
        return new Vector3(x, y, 0f);
    }
}
