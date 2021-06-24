using UnityEngine;
using Object = UnityEngine.Object;

public class AssetLoader : MonoBehaviour
{
    private static AssetLoader instance;

    public static AssetLoader Instance
    {
        get
        {
            return instance;
        }
        set
        {
            if (instance == null)
            {
                instance = value;
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public T Load<T>(string path) where T : Object, new()
    {
        return Resources.Load<T>(path);
    }
}
