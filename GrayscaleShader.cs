using UnityEngine;

public class GrayscaleShader : MonoBehaviour
{
    private Material material;
    public Shader shader;

    void Start()
    {
        // Check if the shader is assigned
        if (shader == null)
        {
            Debug.LogError("Shader is not assigned to the GrayscaleShader script!");
            return;
        }

        // Create a material with the assigned shader
        material = new Material(shader);

        // Check if the material was created successfully
        if (material == null)
        {
            Debug.LogError("Failed to create material with the assigned shader!");
            return;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Apply the material with the shader to the rendered image
        Graphics.Blit(source, destination, material);
    }
}
