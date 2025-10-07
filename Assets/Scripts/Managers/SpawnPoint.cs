using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private bool isDefaultSpawn = true;
    //[SerializeField] private string spawnPointName = "Default";

    [Header("Visual Settings")]
    [SerializeField] private Color gizmoColor = Color.green;
    [SerializeField] private float gizmoSize = 1f;

    // 在Scene视图中绘制出生点标记
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        // 绘制一个球体表示出生点
        Gizmos.DrawWireSphere(transform.position, gizmoSize * 0.5f);
    }

    public bool IsDefaultSpawn()
    {
        return isDefaultSpawn;
    }

}