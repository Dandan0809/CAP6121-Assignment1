using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public Transform point1;
    public Transform point2;

    public int[] enemyWaves;

    public int currentEnemiesLeftCount = 0;
    public int currentWave = 0;

    public GameObject enemyPrefab;

    public GameObject waveIncomingText;
    public GameObject waveCountText;
    public GameObject waveCountdownText;
    public AudioSource audioS;

    public GameObject ForceAbility;
    public GameObject bossPrefab;

    public bool hasFoughtBoss = false;

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
        if (hasFoughtBoss)
        {
            if (currentEnemiesLeftCount != 0)
                return;
            waveIncomingText.GetComponent<TMP_Text>().text = "Victory!";
            waveIncomingText.SetActive(true);
            StartCoroutine(EndGame());
            return;
        }

        currentEnemiesLeftCount--;
        audioS.Play();
        if (currentEnemiesLeftCount == 0)
        {
            currentWave++;
            if (currentWave >= enemyWaves.Length)
            {
                StartCoroutine(BossSpawn());
                hasFoughtBoss = true;
                return;
            }
            else
            {
                StartWave();
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        waveIncomingText.SetActive(true);
        waveCountText.GetComponent<TMP_Text>().text = "Wave Count: " + (currentWave + 1);
        waveCountText.SetActive(true);
        yield return new WaitForSeconds(1f);
        waveCountdownText.GetComponent<TMP_Text>().text = "3";
        waveCountdownText.SetActive(true);
        yield return new WaitForSeconds(1f);
        waveCountdownText.GetComponent<TMP_Text>().text = "2";
        yield return new WaitForSeconds(1f);
        waveCountdownText.GetComponent<TMP_Text>().text = "1";
        yield return new WaitForSeconds(1f);
        waveCountdownText.GetComponent<TMP_Text>().text = "Start!";
        yield return new WaitForSeconds(1f);
        waveCountdownText.SetActive(false);
        waveCountText.SetActive(false);
        waveIncomingText.SetActive(false);

        currentEnemiesLeftCount = enemyWaves[currentWave];
        float k = enemyWaves[currentWave];
        for (int i = 0; i < k; i++)
        {
            float t = Random.Range(0f, 1f); // Random value between 0 and 1
            Instantiate(enemyPrefab, Vector3.Lerp(point1.position, point2.position, t), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator BossSpawn()
    {
        waveIncomingText.SetActive(true);
        waveCountText.GetComponent<TMP_Text>().text = "Final Wave";
        waveCountText.SetActive(true);
        yield return new WaitForSeconds(1f);
        waveCountdownText.GetComponent<TMP_Text>().text = "3";
        waveCountdownText.SetActive(true);
        yield return new WaitForSeconds(1f);
        waveCountdownText.GetComponent<TMP_Text>().text = "2";
        yield return new WaitForSeconds(1f);
        waveCountdownText.GetComponent<TMP_Text>().text = "1";
        yield return new WaitForSeconds(1f);
        waveCountdownText.GetComponent<TMP_Text>().text = "Start!";
        yield return new WaitForSeconds(1f);
        waveCountdownText.SetActive(false);
        waveCountText.SetActive(false);
        waveIncomingText.SetActive(false);

        ForceAbility.SetActive(true);
        currentEnemiesLeftCount = 6;
        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(0f, 1f); // Random value between 0 and 1
            Instantiate(enemyPrefab, Vector3.Lerp(point1.position, point2.position, x), Quaternion.identity);
        }

        float t = Random.Range(0f, 1f); // Random value between 0 and 1
        Instantiate(bossPrefab, Vector3.Lerp(point1.position, point2.position, t), Quaternion.identity);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("StartScene");
    }

    public void LostGame()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();

        foreach (EnemyAI enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        waveIncomingText.GetComponent<TMP_Text>().text = "Defeat...";
        waveIncomingText.SetActive(true);
        StartCoroutine(EndGame());
    }
}
