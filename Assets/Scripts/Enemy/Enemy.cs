using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Move Info")]
    public float moveSpeed = 3f;

    [Header("Check Info")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform wallCheck;

    [Header("Drop Info")]
    public ItemDrop itemDrop;

    public bool facingRight = true;

    public EnemyStateMachine stateMachine { get; private set; }

    public EnemyStats enemyStats { get; private set; }

    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
        enemyStats = GetComponent<EnemyStats>();
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        itemDrop = GetComponent<ItemDrop>();
    }

    protected virtual void Update()
    {
        stateMachine.currentState.Update();
    }

    public bool IsGroundDetected() => Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

    public bool IsWallDetected() => Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);

    public void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);

        FlipController(_xVelocity);
    }

    public virtual void FlipController(float _xVelocity)
    {
        if (facingRight && _xVelocity < 0)
            Flip();
        else if (!facingRight && _xVelocity > 0)
            Flip();
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

}
