using UnityEngine;

public class SaberDeflect : MonoBehaviour
{
    public float deflectForce = 20f;

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
        }
    }
}
