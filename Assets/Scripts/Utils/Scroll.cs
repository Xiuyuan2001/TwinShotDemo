using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 2f;                      // 滚动速度。负值则反向滚动
    public bool scrollWithGameSpeed = true;             // 是否受Time.timeScale影响

    private float _spriteWidth;                         // 单个背景图的宽度
    private Transform[] _backgroundPieces;              // 所有子背景的数组
    private int _leftIndex;                             // 最左边背景的索引
    private int _rightIndex;                            // 最右边背景的索引

    void Start()
    {
        // 获取所有子背景的Transform
        _backgroundPieces = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _backgroundPieces[i] = transform.GetChild(i);
        }

        // 按X坐标排序，确定初始的左中右顺序
        SortBackgroundPiecesByPosition();

        // 获得一张背景图的宽度
        SpriteRenderer spriteRenderer = _backgroundPieces[0].GetComponent<SpriteRenderer>();
        _spriteWidth = spriteRenderer.bounds.size.x;

        // 根据数组初始化索引
        _leftIndex = 0;
        _rightIndex = _backgroundPieces.Length - 1;
    }

    void Update()
    {
        // 计算本帧的移动量
        float moveAmount = scrollSpeed * (scrollWithGameSpeed ? Time.deltaTime : Time.unscaledDeltaTime);

        // 移动所有背景子物体
        foreach (Transform piece in _backgroundPieces)
        {
            piece.Translate(Vector3.left * moveAmount);
        }

        // 检查并执行循环逻辑
        CheckAndReposition();
    }

    // 冒泡 - 按X轴坐标从小到大（从左到右）排序背景块
    private void SortBackgroundPiecesByPosition()
    {
        for (int i = 0; i < _backgroundPieces.Length - 1; i++)
        {
            for (int j = 0; j < _backgroundPieces.Length - i - 1; j++)
            {
                if (_backgroundPieces[j].position.x > _backgroundPieces[j + 1].position.x)
                {
                    Transform temp = _backgroundPieces[j];
                    _backgroundPieces[j] = _backgroundPieces[j + 1];
                    _backgroundPieces[j + 1] = temp;
                }
            }
        }
    }

    // 检查最左边的图是否已经完全移出屏幕，如果是则将其放到最右边
    private void CheckAndReposition()
    {
        Transform leftMostPiece = _backgroundPieces[_leftIndex];
        Transform rightMostPiece = _backgroundPieces[_rightIndex];

        // 检查最左边的背景是否已经完全移出相机视野左侧
        if (leftMostPiece.position.x + _spriteWidth / 2 < GetScreenLeftEdge())
        {
            // 计算新的位置：将它放到当前最右边背景的右边
            Vector3 newPosition = rightMostPiece.position;
            newPosition.x += _spriteWidth;

            leftMostPiece.position = newPosition;

            // 更新索引
            SortBackgroundPiecesByPosition();

        }
    }

    // 获取屏幕左边缘的世界坐标
    private float GetScreenLeftEdge()
    {
        // 将屏幕左下角(0,0)转换为世界坐标
        return Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
    }
}