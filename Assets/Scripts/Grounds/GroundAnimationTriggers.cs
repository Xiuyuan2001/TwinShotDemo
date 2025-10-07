using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAnimationTriggers : MonoBehaviour
{
    private TrapGround trapGround => GetComponentInParent<TrapGround>();

    // Animation Event
    public void AnimationTrigger()
    {
        trapGround.AnimationFinishTrigger();
    }
}
