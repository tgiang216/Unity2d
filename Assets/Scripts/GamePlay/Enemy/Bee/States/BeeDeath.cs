using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeDeath : BaseState
{
    private BeeStateCtrl sm;
    public BeeDeath(StateMachine state) : base("Bee Death", state)
    {
        sm = (BeeStateCtrl)state;
    }
    public override void Enter()
    {
        Debug.Log("Bee die;");
        sm.beeHive.OnABeeDeath(sm.gameObject);
    }
}
