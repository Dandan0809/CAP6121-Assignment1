using UnityEngine;
using TMPro; // Import TextMeshPro

public class SaberDeflect : MonoBehaviour
{
    public float deflectForce = 20f;
    public AudioSource deflectSound; 
    public int deflectCount = 0;
    public TextMeshProUGUI deflectText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Beam"))
        {
            Rigidbody bulletRb = other.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                Vector3 incomingVelocity = bulletRb.velocity;
                Vector3 deflectDirection = Vector3.Reflect(incomingVelocity, transform.forward);
                bulletRb.velocity = deflectDirection.normalized * deflectForce;
            }

            //increase deflect count
            deflectCount++;
            //Debug.Log("Beam Deflected! Total Deflects: " + deflectCount);

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
        }
        else
        {
            //Debug.LogWarning("Deflect UI Text is not assigned!");
        }
    }
}
