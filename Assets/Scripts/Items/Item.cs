using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] protected int scoreValue = 50;                 // 默认物品分值
    [SerializeField] protected bool destroyOnPickup = true;         // 拾取后是否销毁

    public bool isPicked = false;

    public Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPicked = true;
            StartCoroutine(OnPickup());  
        }
    }

    protected virtual void Update()
    {
        
    }

    protected virtual IEnumerator OnPickup()
    {
        yield return null;

        AudioManager.instance.PlayClip(AudioManager.instance.pickUpBuff);

        // 添加默认分数
        if (UI_Score.instance != null)
        {
            UI_Score.instance.AddScore(scoreValue);
        }

        // 销毁物品
        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }

    //protected virtual void OnPickup()
    //{
    //    // 添加分数
    //    if (UI_Score.instance != null)
    //    {
    //        UI_Score.instance.AddScore(scoreValue);
    //    }

    //    // 销毁物品
    //    if (destroyOnPickup)
    //    {

    //        Destroy(gameObject);
    //    }
    //}
}
