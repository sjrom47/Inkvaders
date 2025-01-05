using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{

    //[SerializeField] private GameObject player;
    //[SerializeField] private GameObject playerHitbox;
    //[SerializeField] private GameObject squid;
    //[SerializeField] private GameObject squidHitbox;
    
    //JumpCommand jumpCommand;
    SquidTransformCommand squidTransformCommand;

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        //jumpCommand = new JumpCommand(movePlayer);
        GameObject player = transform.Find("LookingOrientation/JellyFishGirl").gameObject;
        GameObject playerHitbox = transform.Find("LookingOrientation/CharacterHitbox").gameObject;
        GameObject squid = transform.Find("LookingOrientation/Squid_LOD2").gameObject;
        GameObject squidHitbox = transform.Find("LookingOrientation/SquidHitbox").gameObject;
        if (player == null || playerHitbox == null || squid == null || squidHitbox == null)
        {
            Debug.LogError("One or more GameObjects not found in hierarchy!", this);
            return;
        }
        squidTransformCommand = new SquidTransformCommand(player,playerHitbox,squid,squidHitbox);

       
    }

    // Update is called once per frame
    

    //void CheckInput()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        GeneralFlipSquidTransormation();
    //    }
    //    horizontalStraightMovementInput = Input.GetAxis("Vertical");
    //    horizontalSideMovementInput = Input.GetAxis("Horizontal");
    //    verticalMovementInput = Input.GetAxis("Jump");
    //}

    

    // Reset the logic of the Player state
    

    // Reset the size to the one of the Player state

    public void TransformIntoSquid()
    {
        squidTransformCommand.Execute();
    }

    public void EndSquidTransformation()
    {
        squidTransformCommand.Undo();
    }
    
}
