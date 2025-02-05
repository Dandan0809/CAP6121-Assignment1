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
                // Reflect the bullet based on the saber’s movement
                Vector3 incomingVelocity = bulletRb.velocity;

                // Better reflection using saber's forward direction
                Vector3 deflectDirection = Vector3.Reflect(incomingVelocity, transform.forward);

                // Apply new velocity
                bulletRb.velocity = deflectDirection.normalized * deflectForce;
            }
        }
    }
}
