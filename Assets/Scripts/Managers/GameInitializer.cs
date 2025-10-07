using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    [Header("Initial Scene Settings")]
    [SerializeField] private bool loadMenuOnStart = true;       // 是否在启动时加载菜单场景 - Persistent + Menu
    [SerializeField] private int menuSceneIndex = 1;

    void Start()
    {
        // 获得当前活跃的场景的索引 - 索引为0的场景 - Persistent
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (loadMenuOnStart)
            {
                // 加法加载菜单场景
                StartCoroutine(LoadInitialScene());
            }
        }
    }

    private IEnumerator LoadInitialScene()
    {
        // 等待一帧确保所有持久对象初始化完成
        yield return null;

        // 异步加载菜单场景 - 加法加载 - 加载到现有Persistent场景中，不会覆盖它 - AsyncOperation 异步操作类 - 表示一个异步加载/操作
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(menuSceneIndex, LoadSceneMode.Additive);

        // 不断循环等待加载完成 
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // 设置菜单场景为活动场景
        // SetActiveScene - 设置当前活跃场景
        // SceneManager.GetSceneByBuildIndex - 根据索引来获取对应的场景 - 返回值是一个Scene对象
        // 意即：根据Menu索引获得Menu场景，并设置为活动场景
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(menuSceneIndex));

        Debug.Log("Menu scene loaded additively. Persistent scene remains active.");
    }
}