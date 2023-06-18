using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttack : BaseState
{
    private BeeStateCtrl sm;
    private float duration;
    private float timer;

    public BeeAttack(StateMachine state) : base("Bee Chase", state)
    {
        sm = (BeeStateCtrl)state;
    }
    public override void Enter()
    {
        Debug.Log("Bee Attacking");
        sm.isAttacking= true;
        sm.animator.Play("BeeAtk");
        timer = 0f;
        duration = sm.GetClipLenght("BeeAtk");
    }
    public override void UpdatePhysics()
    {
        timer += Time.deltaTime;
        if(timer>= duration)
        {
            sm.ChangeState(sm.idleState);
        }
    }
    public override void Exit()
    {
        sm.isAttacking = false;
        //sm.isFoundPlayer = false;
    }
}
