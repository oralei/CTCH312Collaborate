using UnityEngine;

public class RotateInf : MonoBehaviour
{
    [SerializeField] private Vector3 rateVector;

    void Update()
    {
        Rotate(rateVector);
    }

    private void Rotate(Vector3 rate)
    {
        // Calculate the rotation for this frame
        Quaternion deltaRotation = Quaternion.Euler(rate * Time.deltaTime);

        // Apply the rotation to the current rotation
        transform.rotation = transform.rotation * deltaRotation;
    }
}