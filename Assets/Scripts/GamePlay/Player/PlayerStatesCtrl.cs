using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlayerStatesCtrl : StateMachine
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
    [HideInInspector]
    public AirAttack airAtkState;
    [HideInInspector]
    public HeavyAtk heavyAtkState;
    [HideInInspector]
    public Die deathState;

    [Header("Player Setting")]
    public Rigidbody2D rb;
    public BoxCollider2D boxcollider2D;
    public float horizontalInput;   
    public bool isFacingRight = true;
    public bool isInvisible = false;
    public float invisibleTime = 0.5f;
    public PlayerCombatCtrl playerCombatCtrl;


    [Header("Animation Setting")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GhostCtrl ghostCtrl;
    //public SpriteRenderer renderer;
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


    [Header("AirAtk Setting")]
    public bool canAirAtk = true;
    public float airAtkCooldown = 1;

    [Header("Player Die Setting")]
    public bool isDeath = false;
    private void Awake()
    {
        idleState = new Idle(this);
        moveState = new Moving(this);
        jumpState = new Jumping(this);
        fallState = new Falling(this);
        groundAtk1 = new Attack1(this);
        groundAtk2 = new Attack2(this);
        groundAtk3 = new Attack3(this);
        dashState = new Dashing(this);
        getHitState = new GetHit(this);
        airAtkState = new AirAttack(this);
        heavyAtkState = new HeavyAtk(this);
        deathState = new Die(this);
    }
    private void OnEnable()
    {
        PlayerStats.OnPlayerDie += OnPlayerDie;
    }
    protected override void StartSM()
    {
        //Debug.Log("Get Component Chile");
        rb=GetComponent<Rigidbody2D>();
        boxcollider2D=GetComponent<BoxCollider2D>();
        animator=GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ghostCtrl = GetComponent<GhostCtrl>();
        ghostCtrl.enabled = false;
        this.ChangeState(idleState);
        playerCombatCtrl = GetComponent<PlayerCombatCtrl>();
    }
    protected override void UpdateSM()
    {
        if (isDeath) return;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        atkKeyPressed = (Input.GetMouseButtonDown(0)|| Input.GetKey(KeyCode.J));
        if(isGettingHit)
        {
            return;
        }
        //Debug.Log(canDash);
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            this.ChangeState(dashState);
            //Debug.Log("Dash");
        }
        if (Input.GetKey(KeyCode.L) && canDash)
        {
            this.ChangeState(dashState);
            //Debug.Log("Dash");
        }

        if (isInvisible)
        {

        }
        //Debug.Log("horizontalInput "+ horizontalInput);
        //Debug.Log("velocity " + rb.velocity);
    }
    private void OnDisable()
    {
        PlayerStats.OnPlayerDie -= OnPlayerDie;
    }
    public bool IsGround()
    {
        
        //Debug.DrawRay(boxcollider2D.bounds.center, Vector2.down * (boxcollider2D.bounds.size.y / 2f + 0.05f), Color.red);
        return Physics2D.BoxCast(boxcollider2D.bounds.center,
        boxcollider2D.bounds.size, 0f, Vector2.down, 0.05f, jumpAbleGround);
    }


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

    private Coroutine invi;
    public void StartInvisible(float time)
    {
        Debug.Log("Invisible State");
        if(invi != null) StopCoroutine(invi);
        invi = StartCoroutine(InvisibleCoroutine(time));
        GetComponent<GetHitEffect>().Blink(time);
    }
    IEnumerator InvisibleCoroutine(float time)
    {
        isInvisible= true;
        //gameObject.GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(time);
        isInvisible= false;
        //gameObject.GetComponent<Collider2D>().isTrigger = false;
    }

    public void OnPlayerDie()
    {
        this.ChangeState(this.deathState);
    }
}
