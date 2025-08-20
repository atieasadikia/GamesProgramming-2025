using UnityEngine;

public class BulletKillsEnemy2D : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // needs enemies to have the tag "Enemy"
        if (other != null && other.CompareTag("Enemy"))
        {
            // simplest effect: remove the enemy
            GameObject.Destroy(other.gameObject);

            // remove the bullet too
            GameObject.Destroy(gameObject);
        }
    }
}
