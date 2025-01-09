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
    SkinnedMeshRenderer[] skinnedMeshRenderers;
    MeshRenderer weaponMeshRenderer;

    public SquidTransformCommand(GameObject playerCharacter, GameObject playerHitbox, GameObject squid, GameObject squidHitbox)
    {
        this.playerCharacter = playerCharacter;
        this.playerHitbox = playerHitbox;
        this.squid = squid;
        this.squidHitbox = squidHitbox;
        skinnedMeshRenderers = playerCharacter.GetComponentsInChildren<SkinnedMeshRenderer>();
        weaponMeshRenderer = playerCharacter.GetComponentInChildren<Weapon>().GetComponent<MeshRenderer>();

    }
    public override void Execute()
    {
        squid.SetActive(true);
        squidHitbox.SetActive(true);
        //playerCharacter.SetActive(false);
        
        foreach(SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
        {
            skinnedMeshRenderer.enabled = false;
        }
        weaponMeshRenderer.enabled= false;
        playerHitbox.SetActive(false);
    }
    public override void Undo()
    {
        //playerCharacter.SetActive(true);
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
        {
            skinnedMeshRenderer.enabled = true;
        }
        weaponMeshRenderer.enabled = false;
        playerHitbox.SetActive(true);
        squid.SetActive(false);
        squidHitbox.SetActive(false);
    }
    
}
