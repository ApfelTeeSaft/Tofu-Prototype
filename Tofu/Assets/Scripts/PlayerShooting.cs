using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject paintParticlePrefab;
    public Transform shootPoint;
    public float shootForce = 10f;
    public float fireRate = 0.1f;
    public GameObject player;

    private bool isShooting = false;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }

        if (isShooting && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject paintParticle = Instantiate(paintParticlePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = paintParticle.GetComponent<Rigidbody>();
        rb.useGravity = true;

        // Add randomness to the shoot direction
        Vector3 randomSpread = new Vector3(
            Random.Range(-0.1f, 0.1f),
            Random.Range(-0.1f, 0.1f),
            0
        );
        Vector3 shootDirection = shootPoint.forward + randomSpread;

        rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);

        // Get the player's material color and apply it to the paint particle
        Color playerColor = player.GetComponent<Renderer>().material.color;
        paintParticle.GetComponent<Renderer>().material.color = playerColor;
    }
}