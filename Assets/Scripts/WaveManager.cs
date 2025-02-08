using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Transform point1;
    public Transform point2;

    public int[] enemyWaves;

    public int currentEnemiesLeftCount = 0;
    public int currentWave = 0;

    public GameObject enemyPrefab;

    private void Start()
    {
        StartWave();
    }
    public void StartWave()
    {
        StartCoroutine(SpawnEnemies());
    }

    public void DecreaseEnemyCount()
    {
        currentEnemiesLeftCount--;
        if (currentEnemiesLeftCount == 0)
        {
            currentWave++;
            if (currentWave >= enemyWaves.Length)
            {
                Debug.Log("Game Over");
            }
            else
            {
                StartWave();
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        currentEnemiesLeftCount = enemyWaves[currentWave];
        for (int i = 0; i < enemyWaves[currentWave]; i++)
        {
            float t = Random.Range(0f, 1f); // Random value between 0 and 1
            Instantiate(enemyPrefab, Vector3.Lerp(point1.position, point2.position, t), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
