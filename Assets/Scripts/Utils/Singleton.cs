using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Singleton<T> where T : new()
{
    // We implement the singleton as a generic class
    private static T instance;

    public static T Instance()
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }
}

