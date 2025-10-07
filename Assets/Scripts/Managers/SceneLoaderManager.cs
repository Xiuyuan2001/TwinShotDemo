using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 0 - persistent ;  1 - Menu ; 2 - Level 1
public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager instance;

    public GetBounds getBounds;

    public Player player;
    public static int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;

    [Header("Player Spawn Settings")]
    [SerializeField] private Vector3 defaultSpawnPosition = Vector3.zero;
    private Vector3 nextSpawnPosition;
    private bool hasCustomSpawnPosition = false;        // 是否有自定义的出生位置

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        getBounds = GameObject.FindAnyObjectByType<GetBounds>();
        player = PlayerManager.instance.player;
    }

    private void OnEnable()
    {
        // 订阅场景加载完成事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 取消订阅
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 场景加载完成后 得到摄像机边界 + 设置玩家位置
        getBounds.SetBounds();

        StartCoroutine(SetPlayerPositionAfterLoad());
    }

    private IEnumerator SetPlayerPositionAfterLoad()
    {
        // 等待一帧，确保所有对象都已初始化
        yield return null;

        // 如果有自定义出生位置，使用它；否则使用默认出生点
        if (hasCustomSpawnPosition)
        {
            SetPlayerPosition(nextSpawnPosition);
            hasCustomSpawnPosition = false;
        }
        else
        {
            // 根据tag查找场景中的默认出生点 - ！
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            if (spawnPoint != null)
            {
                SetPlayerPosition(spawnPoint.transform.position);
            }
        }
    }

    private void SetPlayerPosition(Vector3 position)
    {
        // 查找玩家对象
        GameObject player = null;

        // 通过PlayerManager查找玩家对象
        if (PlayerManager.instance != null && PlayerManager.instance.player != null)
        {
            player = PlayerManager.instance.player.gameObject;
        }

        // 设置位置
        if (player != null)
        {
            player.transform.position = position;      
        }
        else
        {
            Debug.LogWarning("Player object not found!");
        }
    }

    public void LoadMenuScene()
    {
        //player.playerStats.ResetStats();

        EnemyManager.instance.levelCompleted = false;

        hasCustomSpawnPosition = false;

        // 如果从游戏场景（menu索引为1，索引大于1的为关卡场景）返回菜单，需要先卸载当前场景
        if (CurrentSceneIndex > 1)
        {
            AudioManager.instance.PlayerBGM(AudioManager.instance.MenuBgm);
            StartCoroutine(LoadSceneAsync(1));
        }
        else
        {
            AudioManager.instance.PlayerBGM(AudioManager.instance.MenuBgm);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }

    // 点完Play后的逻辑 - 进入Level 1
    public void LoadFirstScene()
    {
        //player.playerStats.ResetStats();

        getBounds.SetBounds();

        hasCustomSpawnPosition = false;
        StartCoroutine(LoadSceneAsync(2));
        AudioManager.instance.PlayerBGM(AudioManager.instance.GameBgm);
    }

    // 从UI_Victor脚本调用 - 进入下一关 - Continue逻辑
    public void LoadNextScene()
    {
        //player.playerStats.ResetStats();

        getBounds.SetBounds();

        EnemyManager.instance.levelCompleted = false;           // 重置关卡完成状态
        hasCustomSpawnPosition = false;
        StartCoroutine(LoadSceneAsync(CurrentSceneIndex + 1));
    }

    public void ReloadCurrentScene()
    {
        //player.playerStats.ResetStats();

        getBounds.SetBounds();

        EnemyManager.instance.levelCompleted = false;           // 重置关卡完成状态
        hasCustomSpawnPosition = false;
        StartCoroutine(LoadSceneAsync(CurrentSceneIndex));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        player.playerStats.ResetStats();

        // 获取当前活动场景
        int currentScene = CurrentSceneIndex;

        // 如果不是Persistent场景(0)，先卸载当前场景
        if (currentScene != 0)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
            while (!unloadOperation.isDone)
            {
                yield return null;
            }
        }

        // 加法加载新场景
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // 设置新场景为活动场景
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
    }

}