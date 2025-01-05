using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SaveDataHandler : MonoBehaviourSingleton<SaveDataHandler>
{
    Gamedata gamedata;
    List<ILoader> loaders;
    List<ISaver> savers;
    public void CreateSaveData()
    {
        gamedata = new Gamedata();
    }

    public void LoadSaveData()
    {
        if (gamedata == null)
        {
            gamedata = new Gamedata();
        }
    }

    public void SaveData()
    {

    }

    public List<ILoader> GetLoaders()
    {
        loaders = new List<ILoader>(FindObjectsOfType<MonoBehaviour>().OfType<ILoader>()); 
        return loaders;
    }

    public List<ISaver> GetSaver() 
    {
        savers = new List<ISaver>(FindObjectsOfType<MonoBehaviour>().OfType<ISaver>());
        return savers; 
    }
}
