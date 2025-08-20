using UnityEngine;

public class BulletPooled2D : MonoBehaviour
{
    BulletSpawner2D spawner;

    // Called by the spawner after (re)creating or fetching the bullet
    public void Setup(BulletSpawner2D s)
    {
        spawner = s;
        // Ensure we have a small trigger collider once
        var col = GetComponent<CircleCollider2D>();
        if (col == null) col = gameObject.AddComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = 0.18f; // small bullet
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other || !other.gameObject) return;

        // Hit an enemy? Deactivate it (or do whatever effect you like)
        if (other.CompareTag("Enemy"))
        {
            // Example: briefly disable enemy to show the hit
            other.gameObject.SetActive(false);

            // Return this bullet to the pool immediately
            if (spawner != null) spawner.ReturnBullet(gameObject);
            else gameObject.SetActive(false);
        }
    }
}
