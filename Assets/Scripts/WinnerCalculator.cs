using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinnerCalculator 
{
    public static Team FindWinningTeam(Team playerTeam, Team enemyTeam, Dictionary<Color, int> colorCounts)
    {
        var largestNonBlack = colorCounts.Where(color => !(color.Key.r == 0 && color.Key.g == 0 && color.Key.b == 0)) // Exclude black
            .OrderByDescending(color => color.Value) // Order by value descending
            .FirstOrDefault();
        if (!largestNonBlack.Equals(default(KeyValuePair<Color, int>)))
        {
            return ColorChecker.ColorsAreClose(largestNonBlack.Key, playerTeam.GetTeamColor()) ? playerTeam : enemyTeam;
        }
        else
        {
            return null;
        }
    }
}
