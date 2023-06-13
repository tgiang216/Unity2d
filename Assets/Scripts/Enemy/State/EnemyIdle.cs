using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState
{
    private EnemyStateCtrl sm;
    private float timer;
	public EnemyIdle(EnemyStateCtrl state) : base("EnemyDdle", state)
	{
		sm = (EnemyStateCtrl)state;
        
	}

    public override void Enter()
    {
        timer = 0;
        sm.inIdleStateTime = Random.Range(2f, 5f);
    }
    public override void UpdateLogic()
    {
        timer += Time.deltaTime;
        if (timer > sm.inIdleStateTime && sm.isMovingAround) 
        {
            sm.ChangeState(sm.moveState);    
        }
    }
    public override void Exit() 
    { 
        
    }
}
