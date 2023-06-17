using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeeMove : BaseState
{
    private BeeStateCtrl sm;
    private float distance;
    private Vector3 currentPos;
    private Tween tween;
    public BeeMove(StateMachine state) : base("Bee Move", state)
    {
        sm = (BeeStateCtrl)state;
    }
    public override void Enter()
    {
        Debug.Log("Enter Move state Bee");
        //sm.animator.Play("BoarEnemyWalk");
        currentPos = sm.transform.position;
        distance = Mathf.Abs(sm.transform.position.x - sm.targetToMove.x);
        sm.isMoving = true;
        sm.targetToMove = GetTarGetToMove();
        if (currentPos.x < sm.targetToMove.x)
        {
            sm.isFacinRight = true;
            sm.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            sm.isFacinRight = false;
            sm.transform.localScale = new Vector3(1, 1, 1);
        }
        tween = sm.rb.DOMove(sm.targetToMove, 3f, false).OnComplete(OnMoveComplete);
    }

    public override void Exit()
    {
        sm.isMoving = false;
    }
    private void OnMoveComplete()
    {
        if (sm.isFoundPlayer) sm.ChangeState(sm.atkState);
        sm.ChangeState(sm.idleState);
        sm.isMoving = false;
    }

    private Vector3 GetTarGetToMove()
    {
        if (sm.isFoundPlayer)
        {
            return GetAttackPos(sm.player.position, 1f, Random.Range(10, 15));
        }

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        randomDirection.y = Mathf.Clamp(randomDirection.y, -Mathf.Sqrt(3) / 2, 0f);
        Vector2 randomPoint = (Vector2)sm.pointToAround.position + randomDirection * sm.moveRange;

        return randomPoint;
    }

    Vector3 GetAttackPos(Vector3 center, float radius, float angleInHours)
    {
        // Chat GPT
        float angleInRadians = Mathf.Deg2Rad * ((angleInHours - 3f) * 30f);

        float x = center.x + radius * Mathf.Cos(angleInRadians);
        float y = center.y;
        float z = center.z + radius * Mathf.Sin(angleInRadians);

        return new Vector3(x, y, z);
    }
}
