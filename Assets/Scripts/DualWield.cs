using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWield : MonoBehaviour
{

    public GameObject secondSaber;
    public GameObject[] abilities;

    public void UnleashForce()
    {
        secondSaber.SetActive(true);
        foreach (GameObject obj in abilities)
        {
            obj.SetActive(false);
        }
    }
}
