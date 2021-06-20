using UnityEngine;

[CreateAssetMenu(fileName = "Creatable Config", menuName = "creatable", order = 0)]
public class SOCreatable : SOBase
{
    [Tooltip("行预制名称")] public string LinePrefabName;
    [Tooltip("节点预制名称")] public string NodePrefabName;
    [Tooltip("总行数")] public int TotalLine;
    [Tooltip("总列数")] public int TotalColumn;
}
