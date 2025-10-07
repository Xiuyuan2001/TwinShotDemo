using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    [Header("Wrapping Settings")]
    [SerializeField] private bool wrapHorizontal = true;   // 是否启用水平环绕
    [SerializeField] private bool wrapVertical = true;     // 是否启用垂直环绕
    [SerializeField] private float wrapOffset = 0.5f;      // 环绕时的偏移量，避免立即再次触发

    [Header("Boundary Settings")]
    private float leftBoundary;
    private float rightBoundary;
    private float topBoundary;
    private float bottomBoundary;

    private Camera mainCamera;
    private bool boundsInitialized = false;

    private void Start()
    {
        InitializeBounds();
    }

    private void InitializeBounds()
    {
        mainCamera = Camera.main;

        // 获取摄像机的视野边界
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 cameraPos = mainCamera.transform.position;

        // 设置边界
        leftBoundary = cameraPos.x - cameraWidth / 2f;
        rightBoundary = cameraPos.x + cameraWidth / 2f;
        bottomBoundary = cameraPos.y - cameraHeight / 2f;
        topBoundary = cameraPos.y + cameraHeight / 2f;

        boundsInitialized = true;
    }

    // 手动设置边界 - 加载场景后 - 获取bounds信息 - BoundaryManager调用此方法更新边界
    public void SetBounds(float left, float right, float bottom, float top)
    {
        leftBoundary = left;
        rightBoundary = right;
        bottomBoundary = bottom;
        topBoundary = top;
        boundsInitialized = true;
    }

    // 使用Bounds对象设置边界
    public void SetBoundsFromCollider(Collider2D boundsCollider)
    {
        if (boundsCollider != null)
        {
            Bounds bounds = boundsCollider.bounds;
            leftBoundary = bounds.min.x;
            rightBoundary = bounds.max.x;
            bottomBoundary = bounds.min.y;
            topBoundary = bounds.max.y;
            boundsInitialized = true;
        }
    }

    private void LateUpdate()
    {
        if (!boundsInitialized)
        {
            InitializeBounds();
            return;
        }

        WrapPosition();
    }

    private void WrapPosition()
    {
        Vector3 currentPos = transform.position;
        bool wrapped = false;

        // 水平环绕
        if (wrapHorizontal)
        {
            if (currentPos.x < leftBoundary)
            {
                currentPos.x = rightBoundary - wrapOffset;
                wrapped = true;
            }
            else if (currentPos.x > rightBoundary)
            {
                currentPos.x = leftBoundary + wrapOffset;
                wrapped = true;
            }
        }

        // 垂直环绕
        if (wrapVertical)
        {
            if (currentPos.y < bottomBoundary)
            {
                currentPos.y = topBoundary - wrapOffset;
                wrapped = true;
            }
            else if (currentPos.y > topBoundary)
            {
                currentPos.y = bottomBoundary + wrapOffset;
                wrapped = true;
            }
        }

        // 如果位置发生了环绕，更新位置
        if (wrapped)
        {
            transform.position = currentPos;
        }
    }
}