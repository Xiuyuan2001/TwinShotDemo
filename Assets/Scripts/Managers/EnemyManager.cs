using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Enemy Tracking")]
    [SerializeField] private List<GameObject> activeEnemies = new List<GameObject>();
    private int totalEnemiesInLevel;

    [Header("Victory Settings")]
    [SerializeField] private float victoryDelay = 2f;

    public bool levelCompleted = false;

    public GameObject victor_UI;

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
        // 监听场景加载完成事件 - 场景加载完成后自动触发
        SceneManager.sceneLoaded += OnSceneLoadedCallback;
    }

    private void OnDisable()
    {
        // 取消监听场景加载完成事件
        SceneManager.sceneLoaded -= OnSceneLoadedCallback;
    }

    private void OnSceneLoadedCallback(Scene scene, LoadSceneMode mode)
    {
        // 场景加载后重新查找敌人
        StartCoroutine(DelayedEnemySearch());

        // 重新查找Victory UI
        FindVictorUI();
    }

    private void Start()
    {
        // 延迟查找敌人，确保场景已经加载完成
        StartCoroutine(DelayedEnemySearch());

        // 查找Victory UI
        FindVictorUI();
    }

    private void FindVictorUI()
    {
        // 先在所有场景中查找Canvas
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                GameObject[] rootObjects = scene.GetRootGameObjects();
                foreach (GameObject rootObj in rootObjects)
                {
                    if (rootObj.name == "Canvas")
                    {
                        Transform victorUITransform = rootObj.transform.Find("Victor_UI");
                        if (victorUITransform != null)
                        {
                            victor_UI = victorUITransform.gameObject;
                            return;
                        }
                    }
                }
            }
        }

        if (victor_UI == null)
        {
            Debug.LogWarning("cant find Victor_UI");
        }
    }

    private IEnumerator DelayedEnemySearch()
    {
        // 等待一帧，确保所有场景都已加载
        yield return null;
        FindAllEnemies();
    }

    public void FindAllEnemies()
    {
        activeEnemies.Clear();

        // 遍历所有加载的场景来查找敌人
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                // 获取场景中的所有根对象
                GameObject[] rootObjects = scene.GetRootGameObjects();

                foreach (GameObject rootObj in rootObjects)
                {
                    // 查找该对象及其所有子对象中的EnemyStats组件
                    EnemyStats[] enemies = rootObj.GetComponentsInChildren<EnemyStats>(true);

                    foreach (EnemyStats enemy in enemies)
                    {
                        if (enemy != null && !enemy.isDead)
                        {
                            activeEnemies.Add(enemy.gameObject);
                        }
                    }
                }
            }
        }

        totalEnemiesInLevel = activeEnemies.Count;
    }

    public void RegisterEnemy(GameObject enemy)
    {
        if (!activeEnemies.Contains(enemy))
        {
            activeEnemies.Add(enemy);
            totalEnemiesInLevel++;
        }
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);

            CheckVictoryCondition();
        }
    }

    private void CheckVictoryCondition()
    {
        // 清理null引用
        activeEnemies.RemoveAll(e => e == null);

        if (activeEnemies.Count == 0 && !levelCompleted && totalEnemiesInLevel > 0)
        {
            levelCompleted = true;
            StartCoroutine(TriggerVictory());
        }
    }

    private IEnumerator TriggerVictory()
    {
        yield return new WaitForSeconds(victoryDelay);

        AudioManager.instance.PlayClip(AudioManager.instance.victor);

        // TODO : 显示胜利UI + 冻结玩家移动
        if(victor_UI != null)
        {
            Time.timeScale = 0f;
            victor_UI.SetActive(true);
        }
        else
        {
            //Debug.LogWarning("Victor_UI not found in scene.");
        }
    }

    // 为新场景加载后调用的公共方法
    public void OnSceneLoaded()
    {
        StartCoroutine(DelayedEnemySearch());
    }
}