using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyPrefab;
    [SerializeField] Transform[] spawnPoints;


    private float waveDelay = 3f;
    private float lastSpawnTime = 0f;

    private float minDamage = 10f;
    private float maxDamage = 50f;

    private float minHealth = 30f;
    private float maxHealth = 100f;

    private float minSpeed = 2f;
    private float maxSpeed = 5f;

    private bool isStart = false;
    private List<EnemyHealth> enemyList = new List<EnemyHealth>();
    private int enemyNum;

    private Color strongColor = Color.red;
    private int prevSpawnIndex = 0;


    private void Update()
    {
        UIManager.instance.UpdateRemainEnemyText(enemyList, enemyNum);
        if(Time.time >= lastSpawnTime + waveDelay && !isStart)
        {
            isStart = true;
            StartWave();
        }

        if (isStart && enemyList.Count <= 0)
        {
            isStart = false;
            lastSpawnTime = Time.time;
        }

    }
    private void StartWave()
    {
        UIManager.instance.UpdateWaveText(++GameManager.instance.wave);
        enemyNum = Mathf.RoundToInt(GameManager.instance.wave * 1.8f);
        for (int i = 0; i < enemyNum; i++) CreateEnemy();
    }
    private void CreateEnemy() {
        // 전에 생성했던 포인트와 겹치지 않을 때까지 Random 함수 실행
        int spawnIndex;
        do
        {
            spawnIndex = Random.Range(0, spawnPoints.Length);
        }
        while (prevSpawnIndex == spawnIndex);
        EnemyHealth enemy = Instantiate<EnemyHealth>(enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        enemyList.Add(enemy);

        float intensity = Random.Range(0, 1f);
        float speed = Mathf.Lerp(minSpeed, maxSpeed, intensity);
        float health = Mathf.Lerp(minHealth, maxHealth, intensity);
        float damage = Mathf.Lerp(minDamage, maxDamage, intensity);

        int score = Mathf.RoundToInt(100 * intensity);

        Color color = Color.Lerp(Color.white, strongColor, intensity);

        enemy.SetUp(speed, color, damage, health, intensity);

        enemy.onDeath += () => Destroy(enemy.gameObject);
        enemy.onDeath += () => enemyList.Remove(enemy);
        enemy.onDeath += () => GameManager.instance.AddScore(score);
    }
}
