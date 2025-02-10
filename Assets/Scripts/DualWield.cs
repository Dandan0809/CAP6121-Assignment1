using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWield : MonoBehaviour
{

    public MeshRenderer secondSaber;
    public Collider saberCollider;
    public GameObject[] abilities;

    public void UnleashForce()
    {
        secondSaber.enabled = true;
        saberCollider.enabled = true;
        foreach (GameObject obj in abilities)
        {
            obj.SetActive(false);
        }
    }
}
