using UnityEngine;

public class PaintParticle : MonoBehaviour
{
    public float splatterSize = 0.5f;
    private bool hasSplattered = false;

    void Start()
    {
        // Disable the collider initially
        GetComponent<Collider>().enabled = false;
        // Enable the collider after a short delay
        StartCoroutine(EnableColliderAfterDelay(0.1f));
    }

    private System.Collections.IEnumerator EnableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider>().enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasSplattered)
        {
            hasSplattered = true;
            CreateSplatter(collision.contacts[0].point, collision.contacts[0].normal);
        }

        // Destroy the particle after collision
        Destroy(gameObject);
    }

    void CreateSplatter(Vector3 position, Vector3 normal)
    {
        // Create a GameObject to hold the splatter effect
        GameObject splatter = new GameObject("Splatter");
        splatter.transform.position = position + normal * 0.01f; // Slightly offset to avoid z-fighting
        splatter.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal); // Align with the surface

        // Create the splatter effect using a single quad
        GameObject segment = GameObject.CreatePrimitive(PrimitiveType.Quad);
        segment.transform.parent = splatter.transform;
        segment.transform.localPosition = Vector3.zero;
        segment.transform.localScale = new Vector3(splatterSize, splatterSize, 1);
        segment.transform.rotation = Quaternion.Euler(90, 0, 0); // Rotate to be flat on the ground

        // Get the paint particle color
        Color paintColor = GetComponent<Renderer>().material.color;

        // Create a circular texture and apply it to the segment
        Texture2D splatterTexture = TextureUtils.CreateCircularTexture(256, paintColor);
        Material splatterMaterial = new Material(Shader.Find("Transparent/Diffuse"));
        splatterMaterial.mainTexture = splatterTexture;
        splatterMaterial.SetFloat("_Glossiness", 0f);

        // Set the material to the segment
        Renderer segmentRenderer = segment.GetComponent<Renderer>();
        segmentRenderer.material = splatterMaterial;

        // Disable the collider on the segment
        Destroy(segment.GetComponent<Collider>());
    }
}