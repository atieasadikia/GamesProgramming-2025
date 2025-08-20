using UnityEngine;
using System.Collections.Generic;

public class BulletSpawner2D : MonoBehaviour
{
    Transform player;
    Queue<GameObject> pool;
    float timer;
    float fireInterval = 0.12f;

    public void Setup(Transform p, Queue<GameObject> bulletPool)
    {
        player = p;
        pool = bulletPool;
    }

    GameObject GetBullet()
    {
        GameObject b = (pool != null && pool.Count > 0) ? pool.Dequeue() : new GameObject("Bullet");

        if (!b.activeSelf) b.SetActive(true);

        // Ensure required components
        var sr = b.GetComponent<SpriteRenderer>(); if (sr == null) sr = b.AddComponent<SpriteRenderer>();
        var rb = b.GetComponent<Rigidbody2D>();   if (rb == null) rb = b.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.drag = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Ensure bullet hit logic + trigger collider exists and is configured
        var bullet = b.GetComponent<BulletPooled2D>();
        if (bullet == null) bullet = b.AddComponent<BulletPooled2D>();
        bullet.Setup(this);

        return b;
    }

    public void ReturnBullet(GameObject b)
    {
        if (!b) return;
        b.SetActive(false);
        if (pool != null) pool.Enqueue(b);
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;
        if (timer < fireInterval) return;
        timer = 0f;

        var b = GetBullet();
        b.transform.position = player.position;

        var rb = b.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        Vector2 dir = Random.insideUnitCircle.normalized;
        rb.AddForce(dir * 120f, ForceMode2D.Impulse);

        StartCoroutine(ReturnLater(b, 2.5f));
    }

    System.Collections.IEnumerator ReturnLater(GameObject b, float t)
    {
        yield return new WaitForSeconds(t);
        if (b != null) ReturnBullet(b);
    }
}
