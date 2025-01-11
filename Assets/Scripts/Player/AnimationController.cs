using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] GameObject player;
    Animator anim;
    List<string> animations = new List<string>(){
    "Run", "RunBack", "RunLeft", "RunRight", "Attack",
    
};
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    

    
    public void Animate(Direction direction, bool isShooting)
    {
        

        if (isShooting)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if (direction == Direction.FORWARD)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        if (direction == Direction.LEFT)
        {
            anim.SetBool("RunLeft", true);
        }
        else
        {
            anim.SetBool("RunLeft", false);
        }

        if (direction == Direction.RIGHT)
        {
            anim.SetBool("RunRight", true);
        }
        else
        {
            anim.SetBool("RunRight", false);
        }

        if (direction == Direction.BACKWARD)
        {
            anim.SetBool("RunBack", true);
        }
        else
        {
            anim.SetBool("RunBack", false);
        }

        
        
        //else
        //{

        //    //anim.SetBool("Attack", false);
        //    switch (direction)
        //    {
        //        case Direction.FORWARD:
        //            Debug.Log("Forward shoot");
        //            anim.SetBool("Run02_Attack", true);
        //            break;
        //        case Direction.BACKWARD:
        //            anim.SetBool("RunBack02_Attack", true);
        //            break;
        //        case Direction.LEFT:
        //            anim.SetBool("RunLeft02_Attack", true);
        //            break;
        //        case Direction.RIGHT:
        //            anim.SetBool("RunRight02_Attack", true);
        //            break;
        //        default:
        //            anim.SetBool("Attack", true);
        //            break;
        //    }
        //}
    }

    
    public void StopAllAnimations()
    {
        foreach (string animation in animations)
        {
            anim.SetBool(animation, false);
        }
        
    }
}
