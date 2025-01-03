using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCounter 
{
    Dictionary<Color, int> colorCounts;
    public ColorCounter() { }
    public Dictionary<Color, int> CountColorsInTexture(Texture2D texture)
    {
        // Dictionary to hold color counts
        colorCounts = new Dictionary<Color, int>();

        // Get all pixels from the texture
        Color[] pixels = texture.GetPixels();

        foreach (Color pixel in pixels)
        {
            // Check if the color is already in the dictionary
            if (colorCounts.ContainsKey(pixel))
            {
                // Increment the count
                colorCounts[pixel]++;
            }
            else
            {
                // Add the color to the dictionary with an initial count of 1
                colorCounts[pixel] = 1;
            }
        }

        return colorCounts;
    }

}
