using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeSeconds = 2f;
    private Rigidbody2D rb;
    private float damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Init(Vector2 velocity, float damage)
    {
        this.damage = damage;
        rb.linearVelocity = velocity;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        Destroy(gameObject, lifeSeconds);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Player")) return;

    var damageable = other.GetComponentInParent<IDamageable>();
    if (damageable != null)
    {
        damageable.TakeDamage(damage);
        Destroy(gameObject);
        return;
    }

 /*   if (other.CompareTag("Wall"))
    {
        Destroy(gameObject);
        return;
    }
        Destroy(gameObject); 
        */
    }
}
