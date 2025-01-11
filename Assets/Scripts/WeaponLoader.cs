using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoader : IContainerLoader
{
    public void LoadInformationContainer(IInformationContainer container)
    {
        InformationContainer<GameObject> weaponContainer = (InformationContainer<GameObject>)container ;
        GameManager.Instance().SetChosenWeapon(weaponContainer.Content);
    }
}
