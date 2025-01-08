using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidTransformCommand : Command
{ 
    // Start is called before the first frame update
    GameObject playerCharacter;
    GameObject playerHitbox;
    GameObject squid;
    GameObject squidHitbox;

    public SquidTransformCommand(GameObject playerCharacter, GameObject playerHitbox, GameObject squid, GameObject squidHitbox)
    {
        Debug.Log("Created");
        this.playerCharacter = playerCharacter;
        this.playerHitbox = playerHitbox;
        this.squid = squid;
        this.squidHitbox = squidHitbox;
    }
    public override void Execute()
    {
        squid.SetActive(true);
        squidHitbox.SetActive(true);
        playerCharacter.SetActive(false);
        playerHitbox.SetActive(false);
    }
    public override void Undo()
    {
        playerCharacter.SetActive(true);
        playerHitbox.SetActive(true);
        squid.SetActive(false);
        squidHitbox.SetActive(false);
    }
    
}
