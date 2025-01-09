using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    
    
    Player playerComponent;
    //JumpCommand jumpCommand;
    SquidTransformCommand squidTransformCommand;
    bool lastShootingValue;
    Direction direction;
    WeaponHolder weaponHolder;
    public AnimationController AnimController {  get { return animController; } }
    public WeaponHolder WeaponHolder { get { return weaponHolder; } }

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
        playerComponent = GetComponent<Player>();
        playerComponent.StartReloading += StartReloading;
        playerComponent.StopReloading += StopReloading;


    }

    public void ApplyPlayerMovementAndShot(float horizontalStraightMovementInput, float horizontalSideMovementInput, bool isShooting)
    {
        direction = Direction.NONE;
        if (!playerComponent.IsDead) 
        {
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
        }
        if (isShooting && !lastShootingValue && !playerComponent.IsSquid && !playerComponent.IsDead)
        {
            
            weaponHolder.TryShoot();
        }
        else if (!isShooting && lastShootingValue || playerComponent.IsSquid || playerComponent.IsDead) 
        {
            weaponHolder.TryStopShoot();
        }
        if (!playerComponent.IsSquid && !playerComponent.IsDead)
        {
            lastShootingValue = isShooting;

        }
        else
        {
            lastShootingValue = false;
        }

        animController.Animate(direction, lastShootingValue);
        

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

    

    // Reset the logic of the Player state
    

    // Reset the size to the one of the Player state

    public void TransformIntoSquid()
    {
        squidTransformCommand.Execute();
        playerComponent.IsSquid = true;
    }

    public void EndSquidTransformation()
    {
        squidTransformCommand.Undo();
        playerComponent.IsSquid = false;
    }

    public void StartReloading()
    {
        weaponHolder.GetCurrentWeapon().Reload();
    }

    public void StopReloading()
    {
        weaponHolder.GetCurrentWeapon().StopReloading();
    }

    private void OnDestroy()
    {
        playerComponent.StartReloading -= StartReloading;
        playerComponent.StopReloading -= StopReloading;
    }

}
