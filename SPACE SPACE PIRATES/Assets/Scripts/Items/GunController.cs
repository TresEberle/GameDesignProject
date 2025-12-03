using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 5f; // shots per second
    public int damage = 10;

    private float nextFireTime;

    void Update()
    {
        AimAtMouse();

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryShoot();
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

        Vector3 localScale = transform.localScale;
        if (angle > 90f || angle < -90f)
            localScale.y = -1f;
        else
            localScale.y = 1f;
        transform.localScale = localScale;
    }

    public void TryShoot()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + 1f / fireRate;

        if (bulletPrefab == null || firePoint == null) return;

        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            Vector2 vel = (Vector2)(firePoint.right * bulletSpeed);
            bullet.Init(vel, damage);
        }
    }
}
