using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public Collider2D coll;

    private bool facingRight = true;

    [Header("Move Info")]
    public float moveSpeed = 8f;
    public float buffedSpeed = 15f;
    public float jumpForce = 10f;
    public float flyForce = 5f;

    [Header("Check Info")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform wallCheck;

    [Header("KnockBack Info")]
    public Vector2 knockBackDir;
    public bool isHurt;

    [Header("Buff Info")]
    public bool isInvincible;
    public float invincibilityDuration = 3f;
    public float invincibilityTimer;
    public bool isSpeeding;
    public float speedingDuration = 5f;
    public float speedingTimer;
    public bool canFly;

    [Header("Flash Effect")]
    public SpriteRenderer sr;
    public Material originalMat;
    public Material flashMat;

    public PlayerStats playerStats;

    public float defaultSpeed;

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerState idleState { get; private set; }
    public PlayerState moveState { get; private set; }
    public PlayerState jumpState { get; private set; }  
    public PlayerState airState { get; private set; }
    public PlayerState attackState { get; private set; }
    public PlayerState deadState { get; private set; }
    public PlayerState flyState { get; private set; }
    #endregion

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        coll = GetComponent<Collider2D>();

        sr = GetComponentInChildren<SpriteRenderer>();

        if (sr != null)
        {
            originalMat = sr.material;
        }

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "run");
        jumpState = new PlayerJumpState(this, stateMachine, "jump");
        airState = new PlayerAirState(this, stateMachine, "jump");
        attackState = new PlayerAttackState(this, stateMachine, "attack");
        deadState = new PlayerDeadState(this, stateMachine, "dead");
        flyState = new PlayerFlyState(this, stateMachine, "fly");
    }

    protected virtual void Start()
    {
        stateMachine.Initialize(idleState);

        defaultSpeed = moveSpeed;

        invincibilityTimer = invincibilityDuration;
        speedingTimer = speedingDuration;
    }

    protected virtual void Update()
    {
        stateMachine.currentState.Update();

        if(isInvincible)
        {
            isSpeeding = false;
            canFly = false;

            invincibilityTimer -= Time.deltaTime;

            if(invincibilityTimer <= 0)
            {
                isInvincible = false;
                invincibilityTimer = invincibilityDuration;
            }
        }

        if(isSpeeding)
        {
            canFly = false;
            isInvincible = false;

            moveSpeed = buffedSpeed;

            speedingTimer -= Time.deltaTime;

            if(speedingTimer <= 0)
            {
                isSpeeding = false;
                moveSpeed = defaultSpeed;
                speedingTimer = speedingDuration;
            }
        }
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);

        FlipController(_xVelocity);
    }

    #region Damage & Flash Logic
    public virtual void DamageVisualEffect()
    {
        anim.SetBool("hurt", true);    
        StartCoroutine(KnockBack());
    }
     
    private IEnumerator KnockBack()
    {
        isHurt = true;
        isInvincible = true;

        rb.velocity = new Vector2(knockBackDir.x * -transform.localScale.x, knockBackDir.y);

        yield return null;

        isHurt = false;
        anim.SetBool("hurt", false);

        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
    #endregion

    #region Flip Logic
    public virtual void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    public virtual void FlipController(float _xVelocity)
    {
        if (facingRight && _xVelocity < 0)
            Flip();
        else if (!facingRight && _xVelocity > 0)
            Flip();
    }
    #endregion

    #region Detection Logic
    public virtual bool IsGroundDetected() => Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

    public virtual bool IsWallDetected() => Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
    #endregion

    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
