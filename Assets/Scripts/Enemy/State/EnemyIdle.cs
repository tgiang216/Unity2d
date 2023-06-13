using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState
{
    private EnemyStateCtrl sm;
	public EnemyIdle(EnemyStateCtrl state) : base("EnemyDdle", state)
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
