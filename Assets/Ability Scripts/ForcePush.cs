using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour
{
    public GameObject forcePrefab;
    public Transform spawnPoint;

    public void SpawnPush()
    {
        Instantiate(forcePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
