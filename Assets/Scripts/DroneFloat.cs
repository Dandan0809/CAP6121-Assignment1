using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFloat : MonoBehaviour
{
    public float floatSpeed = 1f;  // Speed of the floating motion
    public float floatAmplitude = 0.5f;  // Height of the floating motion

    private Vector3 startPosition;
    private float timeOffset;

    void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.Range(0f, Mathf.PI * 2); // Random offset to make multiple enemies float out of sync
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed + timeOffset) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
