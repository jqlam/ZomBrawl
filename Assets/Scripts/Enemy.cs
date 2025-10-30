using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public int maxHealth = 30;
    public float speed = 2.5f;
    public int touchDamage = 10;
    public int coinDrop = 10;

    private int currentHealth;
    private Rigidbody2D rb;
    private Transform player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // safety: ensure expected physics each spawn
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        // safety: ensure tag at runtime
        gameObject.tag = "Enemy";
    }

    void Start()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
    }

    void FixedUpdate()
    {
        if (!player) return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;

        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, ang);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameController.Instance.AddCoins(coinDrop);
        // No callback needed; WaveSpawner now polls the scene for zero enemies.
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.CompareTag("Player") && c.collider.TryGetComponent<PlayerHealth>(out var ph))
        {
            ph.TakeDamage(touchDamage);
        }
    }
}
