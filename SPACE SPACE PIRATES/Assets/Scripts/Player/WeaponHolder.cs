using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private GameObject currentWeapon;

    public void EquipWeapon(GameObject weaponPrefab, Vector3 offset)
    {
        if (weaponPrefab == null)
        {
            Debug.LogWarning("WeaponHolder.EquipWeapon called with null prefab.");
            return;
        }

        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        currentWeapon = Instantiate(weaponPrefab, transform);
        currentWeapon.transform.localPosition = offset;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}
