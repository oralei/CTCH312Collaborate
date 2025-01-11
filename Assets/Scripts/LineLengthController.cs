using TMPro;
using UnityEngine;

public class LineLengthController : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public Camera raycastCamera;

    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public LayerMask layersToHit;

    public float maxLength = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        UpdateLine();
    }

    void UpdateLine()
    {
        screenPosition = Input.mousePosition;
        Ray ray = raycastCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            worldPosition = hitData.point;
        }

        // Set positions for the line renderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, worldPosition);
    }
}
