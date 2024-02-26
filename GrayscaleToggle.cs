using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GrayscaleToggle : MonoBehaviour
{
    public PostProcessProfile grayscaleProfile;
    public Toggle grayscaleToggle;

    private PostProcessVolume postProcessVolume;

    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();

        // Check if the PostProcessVolume component is present
        if (postProcessVolume == null)
        {
            Debug.LogError("PostProcessVolume component not found on the GameObject.");
        }

        // Add a listener to the toggle button
        if (grayscaleToggle != null)
        {
            grayscaleToggle.onValueChanged.AddListener(ToggleGrayscaleEffect);
        }
        else
        {
            Debug.LogError("GrayscaleToggle is not connected to a Toggle UI in the Inspector.");
        }
    }

    public void ToggleGrayscaleEffect(bool isOn)
    {
        // Check if the PostProcessVolume component is present
        if (postProcessVolume != null)
        {
            // Enable or disable the Post-Processing Volume component based on the toggle state
            postProcessVolume.enabled = isOn;

            // Apply or remove the grayscale profile based on the toggle state
            if (isOn)
            {
                postProcessVolume.profile = grayscaleProfile;
            }
            else
            {
                // Reset the profile to null to disable the effect
                postProcessVolume.profile = null;
            }
        }
        else
        {
            Debug.LogError("PostProcessVolume is null. Ensure the component is present on the GameObject.");
        }
    }
}
