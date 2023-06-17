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

    }

    public override void Exit()
    {

    }
}
