using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class scaleRendering : MonoBehaviour
{
    [Tooltip("Render scale at the reference resolution (e.g., 1920x1080).")]
    public float referenceRenderScale = 0.247f;

    [Tooltip("Reference width resolution (e.g., 1920 for 1080p).")]
    public int referenceWidth = 1920;

    private UniversalRenderPipelineAsset urpAsset;

    void Start()
    {
        urpAsset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;

        if (urpAsset == null)
        {
            Debug.LogError("URP is not active. Please make sure you are using the Universal Render Pipeline.");
            return;
        }

        AdjustRenderScale();
    }

    void Update()
    {
        // Continuously adjust render scale if the resolution changes at runtime.
        // AdjustRenderScale();
    }

    void AdjustRenderScale()
    {
        if (urpAsset != null)
        {
            // Calculate target render width based on the reference resolution
            float targetRenderWidth = referenceWidth * referenceRenderScale;

            // Calculate new render scale to maintain fixed pixel count
            float newRenderScale = targetRenderWidth / Screen.width;

            // Clamp the render scale within valid ranges
            urpAsset.renderScale = Mathf.Clamp(newRenderScale, 0.1f, 2.0f);

            Debug.Log($"Adjusted Render Scale: {urpAsset.renderScale} (Screen: {Screen.width}x{Screen.height})");
        }
    }
}
