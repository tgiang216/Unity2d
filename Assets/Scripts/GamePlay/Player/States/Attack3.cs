using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.XR;
using UnityEngine;

public class Attack3 : BaseState
{
    private PlayerStatesCtrl sm;
    private float duration;
    private float time;
    //private float horizontalInput;
    public Attack3(PlayerStatesCtrl stateMachine) : base("Attacking", stateMachine)
    {
        sm = (PlayerStatesCtrl)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();    
        time = 0;
        sm.isAttacking = true;
        sm.animator.Play("PlayerAtk3");
        duration = sm.GetClipLenght("PlayerAtk1");
        AudioManager.Instance.PlaySE("MeleeAtk");
        //sm.animStateInfo = sm.animator.GetCurrentAnimatorStateInfo(0);
    }
    public override void UpdateLogic()
    {
        time += Time.deltaTime;
        //if(sm.atkKeyPressed) nextAtk= true;
        if (time >= duration)
        {
            FinishAtkAnim();
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
