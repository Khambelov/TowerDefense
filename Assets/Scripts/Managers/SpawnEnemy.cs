using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy GetSpawnEnemy;

    #region Spawns param
    [Header("Wave settings")]
    [SerializeField]
    private int waveSize;

    [SerializeField]
    private int waveCount;

    [SerializeField, Tooltip("Интервал между волнами врагов. Указывается в секундах")]
    private float waveInterval;

    [SerializeField, Tooltip("Интервал между появлением врагов. Указывается в секундах")]
    private float spawnInterval;

    [SerializeField]
    private Transform spawnPoint;

    [Header("Enemy settings")]
    [SerializeField]
    private EnemyStruct enemy;

    [SerializeField]
    private List<Transform> controlMovePoints;
    #endregion

    bool finishWave;
    int currentEnemyCount;
    int currentWave;

    public List<Transform> MovePoints { get { return controlMovePoints; } }
    public EnemyStruct GetEnemy { get { return enemy; } }

    private void Awake()
    {
        GetSpawnEnemy = this;
        currentEnemyCount = 0;
        currentWave = 0;
        finishWave = false;
    }

    void Start()
    {
        StartCoroutine(StartInterval());
    }

    public void CheckWave()
    {
        currentEnemyCount--;

        if (currentEnemyCount == 0)
        {
            finishWave = true;
        }
    }

    IEnumerator Spawn()
    {
        UIManager.GetUIManager.StopAllCoroutines();
        StartCoroutine(UIManager.GetUIManager.NewWave());

        int enemyCount = 1;
        finishWave = false;
        currentEnemyCount = waveSize;
        currentWave++;
        UIManager.GetUIManager.UpdateWaves(currentWave, waveCount);

        while (enemyCount <= waveSize)
        {
            GameObject obj = Instantiate(enemy.prefab, spawnPoint.position, Quaternion.identity);
            obj.GetComponent<Enemy>().Initialize(enemy.health, enemy.money, enemy.speed, enemy.damage,
                Instantiate(enemy.hpBar, Vector3.zero, Quaternion.identity, UIManager.GetUIManager.GetUICanvas.transform) as GameObject);
            //obj = Instantiate(enemy.hpBar, Vector3.zero, Quaternion.identity, UIManager.GetUIManager.GetUICanvas.transform);

            enemyCount++;

            yield return StartCoroutine(SpawnInterval());
        }

        while (!finishWave)
        {
            yield return null;
        }

        if (currentWave <= waveCount)
        {
            UpComplexity();
            StartCoroutine(StartInterval());
        }
        else
        {
            UIManager.GetUIManager.GameOver(false);
        }

        yield return null;
    }

    IEnumerator StartInterval()
    {
        UIManager.GetUIManager.StopAllCoroutines();
        StartCoroutine(UIManager.GetUIManager.Prepare());

        float timer = waveInterval;

        while (timer > 0f)
        {
            timer--;

            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(Spawn());

        yield return null;
    }

    IEnumerator SpawnInterval()
    {
        yield return new WaitForSeconds(spawnInterval);
    }

    void UpComplexity()
    {
        enemy.speed += 0.5f;

        for (int i = 0; i < enemy.health.Length; i++)
        {
            enemy.health[i] += 3;
        }

        waveSize += 1;
        spawnInterval -= 0.05f;

        if (currentWave == 5)
            waveInterval = 2.5f;
        else
            waveInterval = 5f;
    }
}
