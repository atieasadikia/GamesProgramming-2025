using UnityEngine;
public class PlayerMover2DBad : MonoBehaviour
{
  void Update()
  {
    var rb = GetComponent<Rigidbody2D>();
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    rb.AddForce(new Vector2(h, v) * 25f, ForceMode2D.Force);
    if (Random.value < 0.002f)
    {
      Debug.Log("PlayerVel:" + rb.velocity + " t=" + Time.frameCount);
    }
  }
}
