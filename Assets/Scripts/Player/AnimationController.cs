using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] GameObject player;
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Animate(Direction direction, bool isShooting)
    {
        if (!isShooting)
        {
            //anim.SetBool("Attack", false);
            switch (direction)
            {
                case Direction.FORWARD:
                    anim.SetBool("Run", true);
                    break;
                case Direction.BACKWARD:
                    anim.SetBool("RunBack", true);
                    break;
                case Direction.LEFT:
                    anim.SetBool("RunLeft", true);
                    break;
                case Direction.RIGHT:
                    anim.SetBool("RunRight", true);
                    break;
                default:
                    anim.SetBool("Idle", true);
                    break;
            }
        }
        else
        {
            //anim.SetBool("Attack", false);
            switch (direction)
            {
                case Direction.FORWARD:
                    anim.SetBool("Run02_Attack", true);
                    break;
                case Direction.BACKWARD:
                    anim.SetBool("RunBack02_Attack", true);
                    break;
                case Direction.LEFT:
                    anim.SetBool("RunLeft02_Attack", true);
                    break;
                case Direction.RIGHT:
                    anim.SetBool("RunRight02_Attack", true);
                    break;
                default:
                    anim.SetBool("Attack", true);
                    break;
            }
        }
    }
}
