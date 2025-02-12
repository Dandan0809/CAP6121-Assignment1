using UnityEngine;
using TMPro; 

public class SaberDeflect : MonoBehaviour
{
    public float deflectForce = 20f;
    public AudioSource deflectSound; 
    public int deflectCount = 0;
    public TextMeshProUGUI deflectText;
    //public TextMeshProUGUI missText;
    public int shotCount = 0;
    //private int missCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Beam"))
        {
            Rigidbody bulletRb = other.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                Vector3 incomingVelocity = bulletRb.velocity;
                Vector3 deflectDirection = Vector3.Reflect(incomingVelocity, other.transform.forward);
                bulletRb.velocity = deflectDirection.normalized * deflectForce;
            }

            //increase deflect count
            deflectCount++;
            //Debug.Log("Beam Deflected! Total Deflects: " + deflectCount);
            //missCount = shotCount - deflectCount;
            //Debug.Log("shotCount: " + shotCount);
            //Debug.Log("Misses: " + missCount);

            UpdateDeflectUI();

            if (deflectSound != null)
            {
                deflectSound.Play();
            }
            else
            {
                //Debug.LogWarning("Deflect sound is not assigned!");
            }
        }
    }

    public void UpdateDeflectUI()
    {
        if (deflectText != null)
        {
            deflectText.text = "Deflects: " + deflectCount;
            //Debug.Log("shotCount: " + shotCount);
            //missCount = shotCount - deflectCount;
            //missText.text = "Misses: " + missCount;
        }
        else
        {
            //Debug.LogWarning("Deflect UI Text is not assigned!");
        }
    }
}
