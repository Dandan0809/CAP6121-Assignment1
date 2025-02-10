using UnityEngine;
using System.Collections.Generic;

public class HandGrab : MonoBehaviour
{
    private GameObject grabbedObject;
    private Rigidbody grabbedRb;
    public Transform handTransform;
    public float followSpeed = 50f;
    private bool isGrabbing = false;
    private Vector3 lastHandPosition;
    private List<Vector3> velocityHistory = new List<Vector3>(); // Stores past velocities
    public float velocityRecordTime = 3.0f; // Adjustable time window for storing past velocity
    public float velocityMultiplier = 2.0f; // Increase this value to make throws faster

    void Update()
    {
        // Calculate current hand velocity
        Vector3 handVelocity = (handTransform.position - lastHandPosition) / Time.deltaTime;
        lastHandPosition = handTransform.position;

        // Store velocity in history
        velocityHistory.Add(handVelocity);

        // Ensure we only store velocities within the specified time window
        int maxStoredFrames = Mathf.CeilToInt(velocityRecordTime / Time.deltaTime);
        if (velocityHistory.Count > maxStoredFrames)
        {
            velocityHistory.RemoveAt(0); // Remove oldest velocity entry
        }

        if (isGrabbing && grabbedObject)
        {
            grabbedRb.MovePosition(Vector3.Lerp(grabbedObject.transform.position, handTransform.position, followSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable") && grabbedObject == null && isGrabbing)
        {
            GrabObject(other.gameObject);
        }
    }

    public void StartGrabbing()
    {
        isGrabbing = true;
        velocityHistory.Clear(); // Reset velocity history when grabbing
    }

    public void StopGrabbing()
    {
        isGrabbing = false;
        ReleaseObject();
    }

    private void GrabObject(GameObject obj)
    {
        grabbedObject = obj;
        grabbedRb = obj.GetComponent<Rigidbody>();

        if (grabbedRb)
        {
            grabbedRb.useGravity = false;
            grabbedRb.isKinematic = true;
            grabbedObject.transform.SetParent(handTransform);
        }
    }

    private void ReleaseObject()
    {
        if (grabbedObject)
        {
            grabbedRb.useGravity = true;
            grabbedRb.isKinematic = false;
            grabbedObject.transform.SetParent(null);

            // Calculate the average velocity over the stored time window
            Vector3 averageVelocity = Vector3.zero;
            foreach (Vector3 vel in velocityHistory)
            {
                averageVelocity += vel;
            }
            if (velocityHistory.Count > 0)
            {
                averageVelocity /= velocityHistory.Count;
            }

            // Apply amplified average velocity when releasing
            grabbedRb.velocity = averageVelocity * velocityMultiplier;

            Debug.Log("Stored Velocities: " + velocityHistory.Count);
            Debug.Log("Velocity Record Time: " + velocityRecordTime + "s");
            Debug.Log("Average Hand Velocity: " + averageVelocity);
            Debug.Log("Applied Object Velocity: " + grabbedRb.velocity);

            grabbedObject = null;
        }
    }
}
