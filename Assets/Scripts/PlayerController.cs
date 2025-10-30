using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float margin = 0.5f; // keep this much inside the screen edges

    Rigidbody2D rb;
    Vector2 move;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;                       // ensure no gravity
        rb.freezeRotation = true;                   // same as Constraints Z
    }

    void Update()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = move * moveSpeed;
    }

    void LateUpdate()
    {
        // Clamp player inside camera view
        var cam = Camera.main;
        if (!cam) return;

        float h = cam.orthographicSize;
        float w = h * cam.aspect;

        Vector3 c = cam.transform.position;
        Vector3 pos = transform.position;

        // If you want to account for collider size, increase margin slightly (e.g., 0.6)
        pos.x = Mathf.Clamp(pos.x, c.x - w + margin, c.x + w - margin);
        pos.y = Mathf.Clamp(pos.y, c.y - h + margin, c.y + h - margin);

        transform.position = pos;
    }
}
