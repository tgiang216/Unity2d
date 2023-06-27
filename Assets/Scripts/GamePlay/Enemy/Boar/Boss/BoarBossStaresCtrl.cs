using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarBossStaresCtrl : EnemyStateCtrl
{
    [HideInInspector]
    public StunState stunState;

    public bool isWallCollision;
    public float wallCollisionCooldown = 5f;
    private float collisionTimer = 0f;

    [Header("Stun Setting")]
    public float stunTime;
    public bool isStunning;

    protected override void Awake()
    {
        base.Awake();
        stunState = new StunState(this);
    }
    protected override void StartSM()
    {
        base.StartSM();
        collisionTimer = wallCollisionCooldown;
    }

    protected override void UpdateSM()
    {
        if (isStunning) return;
        collisionTimer += Time.deltaTime; 
        base.UpdateSM();

    }
    public void OnWallCollision()
    {
        if(collisionTimer > wallCollisionCooldown)
        {
            isWallCollision = true;
            collisionTimer = 0;
            //Dung tuong
            this.ChangeState(stunState);
        }
        
    }

}
