using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoader : IContainerLoader
{
    public void LoadInformationContainer(InformationContainer container)
    {
        GameObjectInformationContainer weaponContainer = (GameObjectInformationContainer)container ;
        Debug.LogWarning(weaponContainer.ToString());
        GameManager.Instance().SetChosenWeapon(weaponContainer.Content);
    }
}
