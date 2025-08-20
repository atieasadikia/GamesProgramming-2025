using UnityEngine;
public class CameraShake2D : MonoBehaviour
{
    Camera cam;
    float amp = 0.1f;
    void Awake()
    {
        cam = Camera.main;
    }
    void LateUpdate()
    {
        if (cam == null) return;
        float jx = Mathf.PerlinNoise(0f, Time.time * 5f) * amp;
        float jy = Mathf.PerlinNoise(1f, Time.time * 5f) * amp;
        cam.transform.position = new Vector3(jx, jy, -10f);
    }
}
