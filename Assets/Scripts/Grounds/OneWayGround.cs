using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OneWayGround : MonoBehaviour
{
    PlatformEffector2D platformEffector;
    TilemapCollider2D oneWayTile;

    private void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();

        oneWayTile = GetComponent<TilemapCollider2D>();

        SetOneWay();

    }

    private void SetOneWay()
    {
        platformEffector.useOneWay = true;
        platformEffector.surfaceArc = 90;       // 允许从下方穿透 - 单向表面角度
        platformEffector.rotationalOffset = 0;  // 旋转偏移为0

        oneWayTile.usedByEffector = true;      
    }
}
