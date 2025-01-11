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
        lastShootingValue = false;
        weaponHolder = GetComponent<WeaponHolder>();
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
