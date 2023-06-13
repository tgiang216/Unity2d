using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingPlayer : BaseState
{
    private EnemyStateCtrl sm;
    public ChasingPlayer(EnemyStateCtrl state) : base("Chasing Player", state)
    {
        sm = (EnemyStateCtrl)state;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
