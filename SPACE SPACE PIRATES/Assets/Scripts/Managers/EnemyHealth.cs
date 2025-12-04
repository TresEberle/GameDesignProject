using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class EnemyHealth : MonoBehaviour, IDamageable
{

    [Header("Health")]
    public float maxHealth = 30f;

    [Header("Damage to Player")]
    public float damagePerSecond = 5f;

    [Header("On Death")]
    public bool destroyOnDeath = true;
    private float health;


    [Header("SFX")]
    public AudioClip damageSFX;
    public float damageSoundCooldown = 0.5f; // tweak 
    float nextDamageSoundTime;

    void Awake()
    {
        health = maxHealth;
        var col = GetComponent<Collider2D>();
        col.isTrigger = false;
        
    }

    public void TakeDamage(float amount)
    {
        if (health <= 0f) return;
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }

    }

    void Die()
    {
        if (destroyOnDeath) {
            EnemySpawner._instance.enemiesSpawning.Remove(this);
            EnemySpawner.NotifyEnemyKilled();
            Destroy(gameObject);
            
        } 
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Player")) return;

        var ph = col.collider.GetComponent<Player>();
        if (ph)
        {
            ph.TakeDamage(damagePerSecond * Time.deltaTime);
        }
        if (damageSFX && Time.time >= nextDamageSoundTime)
        {
            AudioSource.PlayClipAtPoint(damageSFX, transform.position, 1f);
            nextDamageSoundTime = Time.time + damageSoundCooldown;
        }

    }
}
