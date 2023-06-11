using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAtk : BaseState
{
    private MovementSM sm;
    private float time;
    private float duration;

    public HeavyAtk(StateMachine state) : base("HeavyAtk", state)
    {
        sm = (MovementSM)state;
    }
    public override void Enter()
    {
        base.Enter();
        time = 0;
        sm.isAttacking = true;
        sm.animator.Play("PlayerAtk1"); // Play a heavy atk
        duration = sm.GetClipLenght("PlayerAtk1");
        sm.combatCtrl.CreateThunder();
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
