using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover2D : MonoBehaviour
{
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(h, v);
        if (input.sqrMagnitude > 1f) input.Normalize();
        rb.AddForce(input * 25f, ForceMode2D.Force);
    }
}
