using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChecker
{
    public static bool ColorsAreClose(Color color1, Color color2)
    {
        return Mathf.Abs(color1.r - color2.r) < 0.05 && Mathf.Abs(color1.g - color2.g) < 0.05 && Mathf.Abs(color1.b - color2.b) < 0.05;
    }
}
