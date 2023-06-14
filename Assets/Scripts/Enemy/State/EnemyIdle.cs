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
        Debug.Log("Enter IDLe state enemy");
        sm.animator.Play("BoarEnemyIdle");
        timer = 0;
        sm.inIdleStateTime = Random.Range(2f, 3f);
    }
    public override void UpdateLogic()
    {
        timer += Time.deltaTime;
        if (timer > sm.inIdleStateTime && !sm.IsPlayerInRange) 
        {
            sm.targetToMove = new Vector3(Random.Range(sm.pointToAround.x - sm.moveRange, sm.pointToAround.x + sm.moveRange), 0);
            sm.ChangeState(sm.moveState);    
        }
        if (sm.IsPlayerInRange)
        {
            sm.ChangeState(sm.foundState);
        }
    }
    public override void Exit() 
    { 
        
    }
}
