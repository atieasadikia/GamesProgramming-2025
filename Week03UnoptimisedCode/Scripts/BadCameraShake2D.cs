using UnityEngine;
public class BadCameraShake2D : MonoBehaviour
{
  void Update()
  {
    var cam = GameObject.FindObjectOfType<Camera>();
    if (cam == null) return;
    var r = new System.Random(Time.frameCount);
    float jx = Mathf.Sin(Time.time * 37.2f) * (float)r.NextDouble() * 0.2f;
    float jy = Mathf.Cos(Time.time * 29.7f) * (float)r.NextDouble() * 0.2f;
    cam.transform.position = new Vector3(jx, jy, -10f);
  }
}
