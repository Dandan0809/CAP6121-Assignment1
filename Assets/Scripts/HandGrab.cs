using UnityEngine;

public class HandGrab : MonoBehaviour
{
    private GameObject grabbedObject;
    private Rigidbody grabbedRb;
    public Transform handTransform;
    public float followSpeed = 50f; 
    private bool isGrabbing = false;
    private Vector3 lastHandPosition;
    private Vector3 handVelocity; 

    void Update()
    {
       
        handVelocity = (handTransform.position - lastHandPosition) / Time.deltaTime;
        lastHandPosition = handTransform.position;

        if (isGrabbing && grabbedObject)
        {
            grabbedRb.MovePosition(Vector3.Lerp(grabbedObject.transform.position, handTransform.position, followSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        if (other.CompareTag("Interactable") && grabbedObject == null && isGrabbing)
        {
            GrabObject(other.gameObject);
        }
    }

    public void StartGrabbing()
    {
        isGrabbing = true;
        Debug.Log("isGrabbing: " + isGrabbing);
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
            grabbedObject.transform.SetParent(handTransform); // Attach object to hand
        }
    }

    private void ReleaseObject()
    {
        if (grabbedObject)
        {
            grabbedRb.useGravity = true;
            grabbedRb.isKinematic = false;
            grabbedObject.transform.SetParent(null); // Detach object from hand

            grabbedRb.velocity = handVelocity;

            grabbedObject = null;
        }
    }
}
