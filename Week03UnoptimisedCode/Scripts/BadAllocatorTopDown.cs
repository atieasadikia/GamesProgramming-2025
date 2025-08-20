using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class BadAllocatorTopDown : MonoBehaviour
{
  void Update()
  {
    // Still BAD on purpose: allocates every frame to create GC pressure
    var list = new List<Vector2>(512);
    for (int i = 0; i < 512; i++)
    {
      float x = Mathf.PerlinNoise(i, Time.time);
      float y = Mathf.Sin(i * 0.1f + Time.time);
      list.Add(new Vector2(x, y));
    }

    var sb = new StringBuilder();
    for (int i = 0; i < 120; i++)
      sb.Append("TD-").Append(i).Append("-").Append(Time.frameCount).Append(';');

    if (Random.value < 0.0005f)
      Debug.Log(sb.ToString());
  }
}
