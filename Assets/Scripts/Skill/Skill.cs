using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] private float cooldown;
    protected float cooldownTimer;

    protected Player player;

    public virtual void Start()
    {
        cooldownTimer = cooldown;

        player = PlayerManager.instance.player;
    }


    public virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer <= 0)
        {
            cooldownTimer = cooldown;
            UseSkill();
            return true;
        }

        Debug.Log("Skill is in cooldown!");
        return false;
    }

    public virtual void UseSkill()
    {
       
    }
}
