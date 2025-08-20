using UnityEngine;
public class UIStats2D : MonoBehaviour
{
    float tick, interval = 0.25f;
    int enemyCount, bulletCount;
    void Update()
    {
        tick += Time.deltaTime;
        if (tick < interval) return;
        tick = 0f;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        bulletCount = GameObject.FindObjectsOfType<Rigidbody2D>().Length;
    }
    void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(new Rect(10, 10, 480, 120), $"Top-Down Stress Test\nEnemies: {enemyCount}\nBullets: {bulletCount}");
    }
}
