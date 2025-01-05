using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamedata
{
    int coinNumber;
    List<string> unlockedWeapons;
    List<string> unlockedSecondaryWeapons;
    int waveRecord;
    int turfModeWins;

    public Gamedata() 
    {
        // TODO: see if we do unlocking of weapons or not
        unlockedWeapons = new List<string>();
        unlockedSecondaryWeapons = new List<string>();
        coinNumber = 0;
        waveRecord = 0;
        turfModeWins = 0;
    }
    
}
