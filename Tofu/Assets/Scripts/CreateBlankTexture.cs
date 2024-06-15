using UnityEngine;

public class CreateBlankTexture : MonoBehaviour
{
    public GameObject ground;
    public int textureSize = 512;
    public Color initialColor = Color.white;

    void Start()
    {
        Texture2D blankTexture = new Texture2D(textureSize, textureSize);
        Color[] pixels = new Color[textureSize * textureSize];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = initialColor;
        }

        blankTexture.SetPixels(pixels);
        blankTexture.Apply();

        Material material = new Material(Shader.Find("Standard"));
        material.mainTexture = blankTexture;

        if (ground != null)
        {
            Renderer renderer = ground.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
            else
            {
                Debug.LogError("No Renderer component found on the ground object.");
            }
        }
        else
        {
            Debug.LogError("Ground object is not assigned.");
        }
    }
}