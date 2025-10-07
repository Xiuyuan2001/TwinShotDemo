using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSkill : Skill
{
    [Header("Skill Info")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 10f;          // 横向速度
    [SerializeField] private float arrowGravity = 1f;
    [SerializeField] private Vector3 offset;                 // 出箭位置偏移
    public override void Start()
    {
        base.Start();
    }

    public void CreateArrow()
    {
        GameObject newArrow = Instantiate(arrowPrefab, player.transform.position + offset, Quaternion.identity);

        newArrow.GetComponent<ArrowController>().SetUpArrow(arrowSpeed,arrowGravity,player.transform);
    }

    public override void Update()
    {
        base.Update();
    }
}
