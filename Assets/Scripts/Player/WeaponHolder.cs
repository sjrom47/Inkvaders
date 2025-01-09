using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    Weapon currentWeapon;
    [SerializeField] Transform weaponPlacement;

    private bool isShooting = false;
    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        GameObject weaponInstance = Instantiate(weaponPrefab, weaponPlacement);
        currentWeapon = weaponInstance.GetComponent<Weapon>();
    }
    public Transform GetWeaponPlacement()
    {
        return weaponPlacement;
    }
    public void AssignColor2Weapon(Color color)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetColor(color);
        }
    }

    // Method for PlayerController to access
    public void TryShoot()
    {
        if (currentWeapon != null && !isShooting)
        {
            currentWeapon.Shoot();
            isShooting = true;
        }
    }

    public void TryStopShoot()
    {
        if (currentWeapon != null && isShooting)
        {
            currentWeapon.StopShooting();
            isShooting = false;
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
}

