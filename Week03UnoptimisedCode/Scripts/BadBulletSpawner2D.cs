using UnityEngine;

public class BadBulletSpawner2D : MonoBehaviour
{
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.03f)
        {
            timer = 0f;

            var p = GameObject.FindGameObjectWithTag("Player"); // bad: global search every shot
            if (p == null) return;

            // make a brand-new bullet each time (bad on purpose)
            var b = new GameObject("Bullet");
            var sr = b.AddComponent<SpriteRenderer>();
            int sz = 8;
            var t = new Texture2D(sz, sz, TextureFormat.RGBA32, false);
            var cols = new Color[sz * sz];
            for (int i = 0; i < cols.Length; i++) cols[i] = new Color(1, 1, 0, 1);
            t.SetPixels(cols); t.Apply();
            sr.sprite = Sprite.Create(t, new Rect(0, 0, sz, sz), new Vector2(0.5f, 0.5f), 16f);

            b.transform.position = p.transform.position;

            var rb = b.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.drag = 0f;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            // add a tiny trigger collider so we can detect hits
            var col = b.AddComponent<CircleCollider2D>();
            col.isTrigger = true;
            col.radius = 0.18f;

            // add the hit logic (still simple and not pooled)
            b.AddComponent<BulletKillsEnemy2D>();

            // fire in a random direction
            Vector2 dir = Random.insideUnitCircle.normalized;
            rb.AddForce(dir * 120f, ForceMode2D.Impulse);

            // clean up later (still bad for GC)
            GameObject.Destroy(b, 2.5f);
        }
    }
}
