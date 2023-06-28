using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderAtkState : BaseState
{
    BoarBossStaresCtrl sm;
    float timer;
    int atkCount;
    public ThunderAtkState(StateMachine state) : base("Boss Atk", state)
    {
        sm = (BoarBossStaresCtrl)state;
    }

    public override void Enter()
    {
        timer = 0f;
        atkCount = 0;
        sm.isThunderatk = true;
        Debug.Log("Begin atk");
        sm.backgroundCtrl.ThunderFlash();
    }

    public override void UpdateLogic()
    {
        timer += Time.deltaTime;
        if (timer > sm.atkTnteval) 
        {
            timer = 0f;
            sm.CreateThunder(sm.player.position);
            atkCount++;
        }
        if(atkCount >= sm.atkCount)
        {
            sm.ChangeState(sm.idleState);
        }
    }
    public override void Exit()
    {
        sm.isThunderatk = false;
    }
}
