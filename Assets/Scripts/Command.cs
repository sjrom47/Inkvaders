using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    //Base class for the commands
    //This class should always look like this to make it more general, so no constructors, parameters, etc!!!
    public abstract void Execute();

    public abstract void Undo();
}
