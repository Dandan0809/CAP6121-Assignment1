using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttacks : MonoBehaviour
{
    public AudioSource audioS;
    public Transform player;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
        player = Camera.main.transform;
        StartCoroutine(RandomShotTimer());
    }

    private IEnumerator RandomShotTimer()
    {
        float randomTime = Random.Range(4f, 5.5f);
        while (true)
        {
            SpawnShot();
            yield return new WaitForSeconds(randomTime);
        }

    }

    public void SpawnShot()
    {
        // Calculate direction from spawn position to target
        float randomHeight = Random.Range(0.1f, 0.7f);
        Vector3 direction = ((player.position - new Vector3(0, randomHeight, 0)) - bulletSpawnPos.position);

        // Create a rotation that looks in that direction
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Instantiate the object with the calculated rotation
        Instantiate(bulletPrefab, bulletSpawnPos.position, rotation);
        
        audioS.Play();
    }


}
