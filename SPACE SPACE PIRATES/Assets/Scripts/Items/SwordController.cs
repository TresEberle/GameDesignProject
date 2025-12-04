using UnityEngine;
using UnityEngine.InputSystem;

public class SwordController : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRate = 2f;      // slashes per second
    public int damage = 20;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    [Header("Refs")]
    public Transform attackPoint;
    public Animator animator;

    private float nextAttackTime;

    void Update()
    {
        // Aim at mouse like gun (optional; comment out if sword doesn't rotate)
        AimAtMouse();

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryAttack();
        }
    }

    void AimAtMouse()
    {
        if (Camera.main == null) return;

        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = transform.position.z;

        Vector3 dir = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Flip Y like your gun so sprite doesn't go upside down
        Vector3 localScale = transform.localScale;
        if (angle > 90f || angle < -90f)
            localScale.y = -1f;
        else
            localScale.y = 1f;
        transform.localScale = localScale;
    }

    void TryAttack()
    {
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + 1f / attackRate;

        if (animator != null)
            animator.SetTrigger("Slash");

        if (attackPoint == null)
        {
            Debug.LogWarning("SwordController: AttackPoint not assigned.");
            return;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );

        foreach (Collider2D hit in hits)
        {
            // Replace "Enemy" with your enemy health script
            var enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
