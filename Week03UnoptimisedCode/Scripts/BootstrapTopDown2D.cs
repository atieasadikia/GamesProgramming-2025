using UnityEngine;
using System.Collections.Generic;
public class BootstrapTopDown2D : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnLoad()
    {
        var go = new GameObject("~BootstrapTopDown2D");
        go.AddComponent<BootstrapTopDown2D>();
    }
    public int enemyCount = 120;
    public List<GameObject> objects = new List<GameObject>();
    void Start()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            var camGO = new GameObject("Main Camera");
            camGO.tag = "MainCamera";
            cam = camGO.AddComponent<Camera>();
        }
        cam.orthographic = true;
        cam.orthographicSize = 18;
        cam.transform.position = new Vector3(0, 0, -10);
        cam.backgroundColor = new Color(0.1f, 0.1f, 0.12f, 1f);
        Physics2D.gravity = Vector2.zero;
        var floor = new GameObject("Floor");
        var fr = floor.AddComponent<SpriteRenderer>();
        fr.sprite = MakeSolidSprite(new Color(0.12f, 0.14f, 0.18f, 1f));
        floor.transform.localScale = new Vector3(60, 35, 1);
        var player = new GameObject("Player");
        player.tag = "Player";
        var psr = player.AddComponent<SpriteRenderer>();
        psr.sprite = MakeSolidSprite(new Color(0.8f, 0.9f, 1f, 1f));
        player.AddComponent<CircleCollider2D>().radius = 0.5f;
        var prb = player.AddComponent<Rigidbody2D>();
        prb.gravityScale = 0f;
        prb.drag = 5f;
        player.AddComponent<PlayerMover2DBad>();
        var rnd = new System.Random(0);
        for (int i = 0; i < enemyCount; i++)
        {
            var e = new GameObject("Enemy_" + i);
            e.tag = "Enemy";
            var sr = e.AddComponent<SpriteRenderer>();
            sr.sprite = MakeSolidSprite(new Color(Random.value, Random.value * 0.5f, Random.value * 0.4f, 1f));
            e.transform.position = new Vector3(rnd.Next(-28, 28), rnd.Next(-14, 14), 0);
            e.AddComponent<CircleCollider2D>().radius = 0.45f;
            var rb = e.AddComponent<Rigidbody2D>(); rb.gravityScale = 0f;
            rb.drag = 3f;
            e.AddComponent<BadEnemyAI2D>();
            e.AddComponent<BadAllocatorTopDown>();
            objects.Add(e);
        }
        var spawner = new GameObject("BadBulletSpawner2D");
        spawner.AddComponent<BadBulletSpawner2D>();
        var ui = new GameObject("BadUIStats2D");
        ui.AddComponent<BadUIStats2D>();
        var shake = new GameObject("BadCameraShake2D");
        shake.AddComponent<BadCameraShake2D>();
    }
    public static Sprite MakeSolidSprite(Color c)
    {
        int size = 16; Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        var cols = new Color[size * size];
        for (int i = 0; i < cols.Length; i++)
            cols[i] = c;
        tex.SetPixels(cols);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 16f);
    }
}
