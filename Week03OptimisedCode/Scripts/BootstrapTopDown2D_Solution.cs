using UnityEngine;
using System.Collections.Generic;

public class BootstrapTopDown2D_Solution : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnLoad()
    {
        var go = new GameObject("~BootstrapTopDown2D_Solution");
        go.AddComponent<BootstrapTopDown2D_Solution>();
    }

    public int enemyCount = 120;
    public List<GameObject> pool = new List<GameObject>();
    public Queue<GameObject> bulletPool = new Queue<GameObject>();
    Sprite bulletSprite;

    void Start()
    {
        // Camera
        Camera cam = Camera.main;
        if (cam == null)
        {
            var camGO = new GameObject("Main Camera");
            camGO.tag = "MainCamera";
            cam = camGO.AddComponent<Camera>();
        }
        cam.orthographic = true;
        cam.orthographicSize = 18;
        cam.transform.position = new Vector3(0f, 0f, -10f); // z offset required
        cam.backgroundColor = new Color(0.1f, 0.1f, 0.12f, 1f);

        // Disable gravity (top-down)
        Physics2D.gravity = Vector2.zero;

        // Floor
        var floor = new GameObject("Floor");
        var fr = floor.AddComponent<SpriteRenderer>();
        fr.sprite = MakeSolidSprite(new Color(0.12f, 0.14f, 0.18f, 1f));
        floor.transform.localScale = new Vector3(60f, 35f, 1f);
        floor.transform.position = Vector3.zero;

        // Player
        var player = new GameObject("Player");
        player.tag = "Player";
        var psr = player.AddComponent<SpriteRenderer>();
        psr.sprite = MakeSolidSprite(new Color(0.8f, 0.9f, 1f, 1f));
        player.AddComponent<CircleCollider2D>().radius = 0.5f;

        var prb = player.AddComponent<Rigidbody2D>();
        prb.gravityScale = 0f;
        prb.drag = 5f;
        player.AddComponent<PlayerMover2D>();

        // Enemies
        var rnd = new System.Random(0);
        for (int i = 0; i < enemyCount; i++)
        {
            var e = new GameObject("Enemy_" + i);
            e.tag = "Enemy";
            var sr = e.AddComponent<SpriteRenderer>();
            sr.sprite = MakeSolidSprite(new Color(Random.value, Random.value * 0.5f, Random.value * 0.4f, 1f));

            // Position in 2D space (z=0)
            e.transform.position = new Vector3(rnd.Next(-28, 28), rnd.Next(-14, 14), 0f);

            e.AddComponent<CircleCollider2D>().radius = 0.45f;
            var rb = e.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.drag = 3f;

            var ai = e.AddComponent<EnemyAI2D>();
            ai.Init(player.transform);

            pool.Add(e);
        }

        // Bullet pool
        bulletSprite = MakeSolidSprite(new Color(1f, 1f, 0f, 1f));
        for (int i = 0; i < 100; i++)
        {
            var b = new GameObject("Bullet");
            var sr = b.AddComponent<SpriteRenderer>();
            sr.sprite = bulletSprite;
            var rb = b.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.drag = 0f;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            b.SetActive(false);
            bulletPool.Enqueue(b);
        }

        // Attach managers
        var spawner = new GameObject("BulletSpawner");
        var bs = spawner.AddComponent<BulletSpawner2D>();
        bs.Setup(player.transform, bulletPool);

        var ui = new GameObject("UIStats2D");
        ui.AddComponent<UIStats2D>();

        var shake = new GameObject("CameraShake2D");
        shake.AddComponent<CameraShake2D>();
    }

    public static Sprite MakeSolidSprite(Color c)
    {
        int size = 16;
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        var cols = new Color[size * size];
        for (int i = 0; i < cols.Length; i++)
            cols[i] = c;
        tex.SetPixels(cols);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 16f);
    }
}
