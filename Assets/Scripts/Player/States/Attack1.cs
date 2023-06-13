using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UIElements;

public class Attack1 : BaseState
{
    private MovementSM sm;
    private bool nextAtk = false;
    private float duration;
    private float time;
    //private float horizontalInput;
    public Attack1(MovementSM stateMachine) : base("Attacking", stateMachine)
    {
        sm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        sm.rb.velocity = Vector2.zero;
        nextAtk = false;
        time = 0;
        sm.isAttacking = true;
        sm.animator.Play("PlayerAtk1");
        duration = sm.GetClipLenght("PlayerAtk1");
        //Debug.Log("atk1 duration " +duration);
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
                sm.ChangeState(sm.groundAtk2);
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
        if (sm.isFacingRight && time >= duration/3 && sm.horizontalInput == -1)
        {
            FinishAtkAnim();
        }
        if (!sm.isFacingRight && time >= duration / 3 && sm.horizontalInput == 1)
        {
            FinishAtkAnim();
        }
    }
}
