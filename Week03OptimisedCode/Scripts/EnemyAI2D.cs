using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI2D : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    float tick, interval = 0.1f;
    public void Init(Transform t)
    {
        target = t;
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        tick += Time.deltaTime;
        if (tick < interval) return;
        tick = 0f;
        if (target == null) return; Vector2 dir = ((Vector2)target.position - rb.position).normalized;
        rb.AddForce(dir * 15f, ForceMode2D.Force);
    }
}
