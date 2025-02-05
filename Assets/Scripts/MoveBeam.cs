using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeam : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed; // Move using Rigidbody
    }
}
