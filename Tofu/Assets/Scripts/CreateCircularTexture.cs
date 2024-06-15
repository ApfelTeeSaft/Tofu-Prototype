using UnityEngine;

public class TextureUtils
{
    public static Texture2D CreateCircularTexture(int size, Color color)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
        Color[] pixels = new Color[size * size];

        int radius = size / 2;
        Vector2 center = new Vector2(radius, radius);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector2 pixelPos = new Vector2(x, y);
                float distance = Vector2.Distance(pixelPos, center);
                if (distance <= radius)
                {
                    pixels[y * size + x] = color;
                }
                else
                {
                    pixels[y * size + x] = new Color(0, 0, 0, 0); // Fully transparent
                }
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }
}