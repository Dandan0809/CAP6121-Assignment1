using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosionEffect; 
    public GameObject explosionSound;  
    public float delay = 2f;
    private bool hasExploded = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded && collision.gameObject.CompareTag("Ground"))
        {
            Invoke("Explode", delay);
            hasExploded = true;
        }
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
        }

        if (explosionSound != null)
        {
            GameObject sound = Instantiate(explosionSound, transform.position, Quaternion.identity);
            AudioSource audio = sound.GetComponent<AudioSource>();

            if (audio != null)
            {
                audio.Play();
                //Destroy(sound, audio.clip.length);
            }
        }

        Destroy(gameObject);
    }
}
