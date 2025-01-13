using UnityEngine;

public class SinScaling : MonoBehaviour
{
    [SerializeField] private float amplitude = 1.0f; // Maximum scale adjustment
    [SerializeField] private float frequency = 1.0f; // Speed of scaling
    [SerializeField] private Vector3 baseScale = Vector3.one; // Base scale of the object

    void Update()
    {
        ScaleObject();
    }

    private void ScaleObject()
    {
        // Calculate the absolute value of the sine wave
        float scaleOffset = Mathf.Abs(Mathf.Sin(Time.time * frequency)) * amplitude;

        // Apply the scaling to the object's base scale
        transform.localScale = baseScale + Vector3.one * scaleOffset;
    }
}