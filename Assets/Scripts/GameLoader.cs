using UnityEngine;

public partial class GameData
{
    private const string CreateConfig = "createconfig";
    private const string Prefabs = "prefabs";
    public static void LoadNodePlane(Transform parent, int totalLine, int totalColumn)
    {
        if (isLoadPlane == true)
        {
            return;
        }
        var prefabPath = ConfigManager.Instance.configDic[Prefabs];
        var lineName = config.LinePrefabName;
        var nodeName = config.NodePrefabName;

        var lineObj = AssetLoader.Instance.Load<GameObject>(prefabPath + lineName);
        var nodeObj = AssetLoader.Instance.Load<GameObject>(prefabPath + nodeName);

        for (var i = 0 ; i < totalLine ; i++)
        {
            var lineTran = Object.Instantiate(lineObj).transform;
            lineTran.SetParent(parent);
            for (var j = 0 ; j < totalColumn ; j++)
            {
                Object.Instantiate(nodeObj).transform.SetParent(lineTran);
            }
        }
        isLoadPlane = true;
    }
}
