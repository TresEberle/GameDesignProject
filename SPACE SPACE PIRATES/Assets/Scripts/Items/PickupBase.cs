using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PickupBase : MonoBehaviour
{

    [Header("FX")]
    public ParticleSystem idleParticles;    // loop while on ground (optional)
    public ParticleSystem pickupParticles;  // one-shot on pickup (optional)
    public AudioClip pickupSFX;


    [Header("Lifetime")]
    public float autoDespawnAfter = 30f;    // 0 = never despawn

    protected virtual void Awake()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;               
        if (autoDespawnAfter > 0f) Destroy(gameObject, autoDespawnAfter);
    }
    void OnTriggerEnter2D(Collider2D other)
{
    // Grab the player's health from this collider or its parents.
    var ph = other.GetComponent<Player>() ?? other.GetComponentInParent<Player>();
    if (ph == null) return;

    // Let the specific pickup apply its effect.
    if (ApplyPickup(other))
    {
        if (pickupSFX) AudioSource.PlayClipAtPoint(pickupSFX, transform.position, 1f);
        if (pickupParticles) Instantiate(pickupParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}


    protected abstract bool ApplyPickup(Collider2D player);
}