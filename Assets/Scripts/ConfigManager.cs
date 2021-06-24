using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    public Dictionary<string, string> configDic = new Dictionary<string, string>();

    /// <summary>
    /// 单例模式
    /// </summary>
    private static ConfigManager instance;

    /// <summary>
    /// 单例模式
    /// </summary>
    public static ConfigManager Instance
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
        LoadConfig();
    }
    
    public void LoadConfig()
    {
        Debug.Log(Application.dataPath);//Config/Config.txt
        var texts = Resources.Load<TextAsset>("Config/Config").text.Replace("\r\n","\n");
        var streamList = texts.Split('\n');
        foreach (var line in streamList)
        {
            var val = line.Split('|');
            if (configDic.ContainsKey(val[0]))
            {
                Debug.LogError("错误 => 重复 key " + val[0]);
            }
            else
            {
                Debug.Log($"读取 => key {val[0]} value {val[1]}");
                configDic.Add(val[0],val[1]);
            }
        }
    }
}
