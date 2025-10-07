using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 挂在player-> animator 上 ，控制动画事件 -> 调用player中的AnimationTrigger方法
public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    // Animation Event
    public void AnimationTrigger()
    {
        player.AnimationFinishTrigger();
    }

    public void Flip()
    {
        player.Flip();
    }
}
