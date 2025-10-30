using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 2f; int damage; float speed; string ownerTag; Rigidbody2D rb;
    public void Init(int dmg,float spd,string owner){ damage=dmg; speed=spd; ownerTag=owner; }
    void Awake(){ rb = GetComponent<Rigidbody2D>(); Destroy(gameObject, life); }
    void Update(){ rb.linearVelocity = transform.right * speed; }
    void OnTriggerEnter2D(Collider2D other){
        if(ownerTag=="Player" && other.CompareTag("Enemy") && other.TryGetComponent<Enemy>(out var e)){ e.TakeDamage(damage); Destroy(gameObject); }
    }
}
