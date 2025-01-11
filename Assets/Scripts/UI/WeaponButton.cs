using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponButton : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    public GameObject GetWeaponPrefab()
    {
        return weaponPrefab;
    }
}
