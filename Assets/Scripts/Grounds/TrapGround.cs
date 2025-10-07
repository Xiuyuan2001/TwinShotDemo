using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGround : MonoBehaviour
{
    [Header("Ground Settings")]
    [SerializeField] private float waitBeforeDisappear = 1f;
    [SerializeField] private float disappearDuration = 2f;
    [SerializeField] private float disappearTimer;

    [Header("Components")]
    private Animator anim;
    private Collider2D groundCollider;
    private SpriteRenderer sr;

    [Header("Status")]
    [SerializeField] private bool isDisappearing = false;
    [SerializeField] private Transform playerCheck;

    // 用作 smash 动画结束后 - 触发 隐藏 collider 等
    private bool TriggerCalled;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        groundCollider = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        disappearTimer = disappearDuration;
    }

    public bool IsPlayerOnGround() => Physics2D.OverlapCircle(playerCheck.position, 0.1f, LayerMask.GetMask("Player"));

    private void Update()
    {
        if(IsPlayerOnGround() && !isDisappearing)
        {
            StartCoroutine(Delay());
            //SmashGround();
        }

        if (TriggerCalled)
        { 
            DisappearGround();

            disappearTimer -= Time.deltaTime;

            if (disappearTimer <= 0)
                RespawnGround();
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitBeforeDisappear);
        SmashGround();
    }

    private void RespawnGround()
    {
        anim.SetTrigger("respawn");
        isDisappearing = false;
        groundCollider.enabled = true;
        sr.enabled = true;
        TriggerCalled = false;
    }

    private void DisappearGround()
    {
        groundCollider.enabled = false;
        sr.enabled = false;
    }

    private void SmashGround()
    {
        isDisappearing = true;
        anim.SetTrigger("disappear");
        disappearTimer = disappearDuration;
    }

    public void AnimationFinishTrigger() => TriggerCalled = true;

}
