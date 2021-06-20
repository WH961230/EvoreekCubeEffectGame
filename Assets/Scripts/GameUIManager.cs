using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static GameUIManager instance;

    /// <summary>
    /// 单例模式
    /// </summary>
    public static GameUIManager Instance
    {
        get => instance;
        private set
        {
            if (instance == null)
            {
                instance = value;
            }
        }
    }

    /// <summary>
    /// 单例初始化
    /// </summary>
    public void Awake()
    {
        Instance = this;
    }
}
