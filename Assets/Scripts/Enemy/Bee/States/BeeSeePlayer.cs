using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSeePlayer : BaseState
{
    private BeeStateCtrl sm;

    public BeeSeePlayer(StateMachine state) : base("Bee See Player", state)
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
