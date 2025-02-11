using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWield : MonoBehaviour
{

    public MeshRenderer secondSaber;
    public MeshRenderer saberBase;
    public Collider saberCollider;
    public GameObject[] abilities;
    public PlayerHealth player;

    public GameObject[] forceBlasts;
    public void UnleashForce()
    {
        secondSaber.enabled = true;
        saberBase.enabled = true;
        saberCollider.enabled = true;
        foreach (GameObject obj in abilities)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in forceBlasts)
        { obj.SetActive(true); }
        StartCoroutine(Regenerate());
    }

    IEnumerator Regenerate()
    {
        while (true)
        {
            player.CastHeal();
            yield return new WaitForSeconds(8f);
        }
    }
}
