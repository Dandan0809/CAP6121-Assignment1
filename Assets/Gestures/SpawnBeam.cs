using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeam : MonoBehaviour
{
    public GameObject beamPrefab;
    public Transform spawnPoint;

    public void BeamAppear()
    {
        Instantiate(beamPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
