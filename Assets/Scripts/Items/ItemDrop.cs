using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject[] possibleDrops;

    public void GenerateDrop()
    {
        // 检查是否有可掉落的物品
        if (possibleDrops == null || possibleDrops.Length == 0)
            return;

        // 先保存位置，避免transform被销毁
        Vector3 dropPosition = transform.position;

        int randomIndex = Random.Range(0, possibleDrops.Length);

        // 检查选中的预制体是否为null
        if (possibleDrops[randomIndex] != null)
        {
            Instantiate(possibleDrops[randomIndex], dropPosition, Quaternion.identity);
        }
    }
}
