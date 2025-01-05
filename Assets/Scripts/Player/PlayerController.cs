using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //[SerializeField] private GameObject player;
    //[SerializeField] private GameObject playerHitbox;
    //[SerializeField] private GameObject squid;
    //[SerializeField] private GameObject squidHitbox;
    float horizontalStraightMovementInput;
    float horizontalSideMovementInput;
    //float verticalMovementInput;

    MovePlayer movePlayer;
    MoveForwardCommand moveForwardCommand;
    MoveBackwardCommand moveBackwardCommand;
    TurnRightCommand turnRightCommand;
    TurnLeftCommand turnLeftCommand;
    //JumpCommand jumpCommand;
    SquidTransformCommand squidTransformCommand;

    // Start is called before the first frame update
    void Awake()
    {
        movePlayer = GetComponent<MovePlayer>();
        moveForwardCommand = new MoveForwardCommand(movePlayer);
        moveBackwardCommand = new MoveBackwardCommand(movePlayer);
        turnRightCommand = new TurnRightCommand(movePlayer);
        turnLeftCommand = new TurnLeftCommand(movePlayer);
        //jumpCommand = new JumpCommand(movePlayer);
        GameObject player = transform.Find("JellyFishGirl").gameObject;
        GameObject playerHitbox = transform.Find("CharacterHitbox").gameObject;
        GameObject squid = transform.Find("Squid_LOD2").gameObject;
        GameObject squidHitbox = transform.Find("SquidHitbox").gameObject;
        if (player == null || playerHitbox == null || squid == null || squidHitbox == null)
        {
            Debug.LogError("One or more GameObjects not found in hierarchy!", this);
            return;
        }
        squidTransformCommand = new SquidTransformCommand(player,playerHitbox,squid,squidHitbox);

       
    }

    // Update is called once per frame
    void Update()
    {
        //CheckInput();
        //ApplyMovement();
    }

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

    public void ApplyMovement(float horizontalStraightMovementInput, float horizontalSideMovementInput)
    {
        if (horizontalStraightMovementInput < 0)
        {
            Debug.Log("Atras");
            moveBackwardCommand.Execute(); 
        }
        else if (horizontalStraightMovementInput > 0)
        {
            Debug.Log("Delante");
            moveForwardCommand.Execute(); 
        }

        else if (horizontalSideMovementInput < 0) 
        { 
            turnLeftCommand.Execute(); 
        }
        else if (horizontalSideMovementInput > 0) 
        { 
            turnRightCommand.Execute(); 
        }

        //if (verticalMovementInput > 0)
        //{
        //    jumpCommand.Execute();
        //}
    }

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
