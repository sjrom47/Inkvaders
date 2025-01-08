using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    FORWARD, BACKWARD, LEFT, RIGHT, NONE
}
public class PlayerController : BaseController
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject playerHitbox;
    [SerializeField] GameObject squid;
    [SerializeField] GameObject squidHitbox;
    [SerializeField] AnimationController animController;


    //JumpCommand jumpCommand;
    SquidTransformCommand squidTransformCommand;
    bool lastShootingValue;
    Direction direction;
    WeaponHolder weaponHolder;

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        //jumpCommand = new JumpCommand(movePlayer);
        lastShootingValue = false;
        weaponHolder = GetComponent<WeaponHolder>();
        //GameObject player = transform.Find("LookingOrientation/JellyFishGirl").gameObject;
        //GameObject playerHitbox = transform.Find("LookingOrientation/CharacterHitbox").gameObject;
        //GameObject squid = transform.Find("LookingOrientation/Squid_LOD2").gameObject;
        //GameObject squidHitbox = transform.Find("LookingOrientation/SquidHitbox").gameObject;
        //if (player == null || playerHitbox == null || squid == null || squidHitbox == null)
        //{
        //    Debug.LogError("One or more GameObjects not found in hierarchy!", this);
        //    return;
        //}
        squidTransformCommand = new SquidTransformCommand(player,playerHitbox,squid,squidHitbox);

       
    }

    public void ApplyPlayerMovementAndShot(float horizontalStraightMovementInput, float horizontalSideMovementInput, bool isShooting)
    {
        direction = Direction.NONE;
        if (horizontalStraightMovementInput < 0)
        {
            moveBackwardCommand.Execute();
            direction = Direction.BACKWARD;
        }
        else if (horizontalStraightMovementInput > 0)
        {
            moveForwardCommand.Execute();
            direction = Direction.FORWARD;
        }

        else if (horizontalSideMovementInput < 0)
        {
            turnLeftCommand.Execute();
            direction = Direction.LEFT;
        }
        else if (horizontalSideMovementInput > 0)
        {
            turnRightCommand.Execute();
            direction = Direction.RIGHT;
        }
        if (isShooting && !lastShootingValue)
        {
            Debug.Log(weaponHolder);
            weaponHolder.TryShoot();
        }
        else if (!isShooting && lastShootingValue) 
        {
            weaponHolder.TryStopShoot();
        }
        lastShootingValue = isShooting;
        Debug.Log(lastShootingValue);
        animController.Animate(direction, lastShootingValue);
        

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
