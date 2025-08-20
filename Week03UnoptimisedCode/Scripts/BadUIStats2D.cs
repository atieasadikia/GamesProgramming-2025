using UnityEngine;
using System.Text;
using System.Linq;
public class BadUIStats2D : MonoBehaviour
{
  void OnGUI()
  {
    var enemies = GameObject.FindGameObjectsWithTag("Enemy");
    var bullets = GameObject.FindObjectsOfType<Rigidbody2D>().Where(r => r.name.Contains("Bullet")).Count();
    StringBuilder sb = new StringBuilder();
    sb.Append("Top-Down Stress Test");
    sb.Append("Enemies: ").Append(enemies.Length).Append(""); sb.Append("Bullets: ").Append(bullets).Append("");
    sb.Append("Time: ").Append(Time.time).Append("  Frame: ").Append(Time.frameCount).Append("");
    GUI.color = Color.white; GUI.Label(new Rect(10, 10, 500, 200), sb.ToString());
  }
}
