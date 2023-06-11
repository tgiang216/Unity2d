using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.XR;
using UnityEngine;

public class Attack2 : BaseState
{
    private MovementSM sm;
    private bool nextAtk = false;
    private int atkIndex;
    private float duration;
    private float time;
    //private float horizontalInput;
    public Attack2(MovementSM stateMachine) : base("Attack2", stateMachine)
    {
        sm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        nextAtk = false;
        time = 0;
        atkIndex = 2;
        sm.isAttacking = true;
        sm.animator.Play("PlayerAtk2");
        duration = sm.GetClipLenght("PlayerAtk2");
        //sm.animStateInfo = sm.animator.GetCurrentAnimatorStateInfo(0);
    }
    public override void UpdateLogic()
    {
        time += Time.deltaTime;
        if(sm.atkKeyPressed) nextAtk= true;
        if (time >= duration)
        {
            if (nextAtk)
            {
                sm.ChangeState(sm.heavyAtkState);
            }
            else
            {
                FinishAtkAnim();
            }
        }
        CancelAtk();
    }
    public override void UpdatePhysics()
    {
       
    }
    public override void Exit()
    {
        sm.isAttacking = false;
        base.Exit();
    }

    public void FinishAtkAnim() //finish
    {
        sm.isAttacking = false;
        sm.ChangeState(sm.idleState);
    }
    private void CancelAtk()
    {
        if (sm.isFacingRight && time >= duration / 3 && sm.horizontalInput == -1)
        {
            FinishAtkAnim();
        }
        if (!sm.isFacingRight && time >= duration / 3 && sm.horizontalInput == 1)
        {
            FinishAtkAnim();
        }
    }
}
