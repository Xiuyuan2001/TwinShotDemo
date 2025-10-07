using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����player-> animator �� �����ƶ����¼� -> ����player�е�AnimationTrigger����
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
