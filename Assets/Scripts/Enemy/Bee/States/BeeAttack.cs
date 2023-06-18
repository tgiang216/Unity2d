using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttack : BaseState
{
    private BeeStateCtrl sm;

    public BeeAttack(StateMachine state) : base("Bee Chase", state)
    {
        sm = (BeeStateCtrl)state;
    }
    public override void Enter()
    {
        Debug.Log("Bee Attacking");
        sm.isAttacking= true;
        sm.animator.Play("BeeAtk");
    }

    public override void Exit()
    {
        sm.isAttacking = false;
        sm.isFoundPlayer= false;
    }
}
