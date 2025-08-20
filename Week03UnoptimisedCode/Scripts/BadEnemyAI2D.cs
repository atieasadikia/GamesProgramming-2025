using UnityEngine; using System.Linq;
public class BadEnemyAI2D : MonoBehaviour {
  void Update()
  {
    var player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
    if (player != null)
    {
      var dir = (player.transform.position - transform.position).normalized;
      var rb = GetComponent<Rigidbody2D>(); rb.AddForce(dir * 15f, ForceMode2D.Force);
    }
    transform.Rotate(0, 0, 90f * Time.deltaTime);
    var all = GameObject.FindObjectsOfType<BadEnemyAI2D>();
    foreach (var a in all)
    {
      if (a == null) { }
    }
  }
}
