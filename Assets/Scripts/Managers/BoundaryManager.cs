using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundaryManager : MonoBehaviour
{
    public static BoundaryManager instance;

    public CinemachineConfiner2D confiner;

    [Header("Boundary Settings")]
    public float leftBoundary;
    public float rightBoundary;
    public float topBoundary;
    public float bottomBoundary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 场景加载后更新边界
        StartCoroutine(UpdateBoundsAfterSceneLoad());
    }

    private IEnumerator UpdateBoundsAfterSceneLoad()
    {
        // 等待一帧，确保场景完全加载
        yield return null;

        UpdateBounds();
    }

    public void UpdateBounds()
    {
        if (confiner.m_BoundingShape2D == null)
            return;
      
        // 从confiner获取边界
        Collider2D boundsCollider = confiner.m_BoundingShape2D;

        if (boundsCollider != null)
        {
            { 
                SetBoundsFromCollider(boundsCollider);

                // 更新所有挂在了 ScreenWrappe 的物体的边界信息
                UpdateAllScreenWrappers();
                return;
            }
        }

        // 如果没有找到Bounds对象，使用摄像机视野
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            SetBoundsFromCamera(mainCamera);
            Debug.Log("Boundaries set from Main Camera");

            // 更新所有ScreenWrapper组件
            UpdateAllScreenWrappers();
        }
        else
        {
            Debug.LogWarning("Neither Bounds object nor Main Camera found!");
        }
    }

    private void SetBoundsFromCollider(Collider2D collider)
    {
        Bounds bounds = collider.bounds;
        leftBoundary = bounds.min.x;
        rightBoundary = bounds.max.x;
        bottomBoundary = bounds.min.y;
        topBoundary = bounds.max.y;
    }

    private void SetBoundsFromCamera(Camera camera)
    {
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        Vector3 cameraPos = camera.transform.position;

        leftBoundary = cameraPos.x - cameraWidth / 2f;
        rightBoundary = cameraPos.x + cameraWidth / 2f;
        bottomBoundary = cameraPos.y - cameraHeight / 2f;
        topBoundary = cameraPos.y + cameraHeight / 2f;
    }

    private void UpdateAllScreenWrappers()
    {
        // 查找所有挂载了ScreenWrapper的gameobejct - 并更新新的边界 - Player/Enemy/ItemBuff?
        ScreenWrapper[] wrappers = FindObjectsByType<ScreenWrapper>(FindObjectsSortMode.None);

        foreach (var wrapper in wrappers)
        {
            wrapper.SetBounds(leftBoundary, rightBoundary, bottomBoundary, topBoundary);
        }
    }

    // 获取边界信息的外部接口
    public Vector4 GetBounds()
    {
        return new Vector4(leftBoundary, rightBoundary, bottomBoundary, topBoundary);
    }

    // 检查某个位置是否在边界外
    public bool IsOutOfBounds(Vector3 position)
    {
        return position.x < leftBoundary || position.x > rightBoundary ||
               position.y < bottomBoundary || position.y > topBoundary;
    }

    // 获取环绕后的位置
    public Vector3 GetWrappedPosition(Vector3 position, float offset = 0.5f)
    {
        Vector3 wrappedPos = position;

        if (position.x < leftBoundary)
            wrappedPos.x = rightBoundary - offset;
        else if (position.x > rightBoundary)
            wrappedPos.x = leftBoundary + offset;

        if (position.y < bottomBoundary)
            wrappedPos.y = topBoundary - offset;
        else if (position.y > topBoundary)
            wrappedPos.y = bottomBoundary + offset;

        return wrappedPos;
    }
}