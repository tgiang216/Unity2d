using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class MovementSM : StateMachine
{
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Moving moveState;
    [HideInInspector]
    public Jumping jumpState;
    [HideInInspector]
    public Falling fallState;
    [HideInInspector]
    public Attack1 groundAtk1;
    [HideInInspector]
    public Attack2 groundAtk2;
    [HideInInspector]
    public Attack3 groundAtk3;
    [HideInInspector]
    public Dashing dashState;
    [HideInInspector]
    public GetHit getHitState;

    [Header("Player Setting")]
    public Rigidbody2D rb;
    public BoxCollider2D boxcollider2D;
    public float horizontalInput;   
    public bool isFacingRight = true;
    public bool isInvisible = false;
    public float invisibleTime = 0.5f;


    [Header("Animation Setting")]
    public Animator animator;
    public SpriteRenderer renderer;
    //public AnimatorStateInfo animStateInfo;
 


    [Header("Move Setting")]
    public float moveSpeed = 4f;


    [Header("Jump Setting")]
    public float jumpTime;
    public bool isJumping;
    public float buttonPressTime = 0.3f;
    public float jumpForce = 10f;
    public LayerMask jumpAbleGround;

    [Header("Attack Setting")]
    public bool isAttacking;
    public bool atkKeyPressed;

    [Header("Dash Setting")]
    public bool isDashing = false;
    public float dashCoolDown = 0.5f;
    public float dashTime = 0.3f;
    public bool canDash = true;
    public float dashForce = 20f;

    [Header("GetHit Setting")]
    public bool isGettingHit;
    public float timeRecover = 0.3f;
    public float hitForce = 50f;

    private void Awake()
    {
        idleState = new Idle(this);
        moveState = new Moving(this);
        jumpState = new Jumping(this);
        fallState = new Falling(this);
        groundAtk1 = new Attack1(this);
        groundAtk2 = new Attack2(this);
        groundAtk3 = new Attack3(this);
        dashState= new Dashing(this);
        getHitState = new GetHit(this);
    }
    protected override void StartSM()
    {
        //Debug.Log("Get Component Chile");
        rb=GetComponent<Rigidbody2D>();
        boxcollider2D=GetComponent<BoxCollider2D>();
        animator=GetComponent<Animator>();
        renderer=GetComponent<SpriteRenderer>();
        this.ChangeState(idleState);
    }
    protected override void UpdateSM()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        atkKeyPressed = Input.GetMouseButtonDown(0);
        //Debug.Log(canDash);
        if (Input.GetKey(KeyCode.K) && canDash && !isGettingHit)
        {
            this.ChangeState(dashState);
            //Debug.Log("Dash");
        }
        if (Input.GetKey(KeyCode.L) && !isGettingHit)
        {
            this.ChangeState(getHitState);
            //Debug.Log("Dash");
        }
        //Debug.Log("horizontalInput "+ horizontalInput);
        //Debug.Log("velocity " + rb.velocity);
    }
    public bool IsGround()
    {
        //Debug.DrawRay(capsuleCollider2D.bounds.center, Vector2.down * (capsuleCollider2D.bounds.size.y / 2f + 0.1f), Color.red);
        //return Physics2D.BoxCast(capsuleCollider2D.bounds.center,
        // capsuleCollider2D.bounds.size, 0f, Vector2.down, 0.1f, jumpAbleGround);
        Debug.DrawRay(boxcollider2D.bounds.center, Vector2.down * (boxcollider2D.bounds.size.y / 2f + 0.05f), Color.red);
        return Physics2D.BoxCast(boxcollider2D.bounds.center,
        boxcollider2D.bounds.size, 0f, Vector2.down, 0.05f, jumpAbleGround);
    }

    public void OnAnimStart()
    {
        
    }
    public void OnAnimEnd()
    {
        
    }
    //protected override BaseState GetInitState()
    //{
    //    Debug.Log("Innit state");
    //    return idleState;
    //}
    public float GetClipLenght(string name)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == name)
            {
                //Debug.Log("Animation Length: " + clip.length);
                return clip.length;
            }
        }
        return 0;
    }
}
