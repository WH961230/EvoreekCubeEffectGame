using UnityEngine;

[CreateAssetMenu(fileName = "Creatable Config", menuName = "creatable", order = 0)]
public class SOCreatable : SOBase
{
    [Tooltip("行预制名称")] public string LinePrefabName;
    [Tooltip("节点预制名称")] public string NodePrefabName;
    [Tooltip("总行数")] public int TotalLine;
    [Tooltip("总列数")] public int TotalColumn;
    [Tooltip("自动下落时间间隔")] public float AutoTimeInterval = 1f;
    [Tooltip("移动下落速率")] public float MoveDownTimeInterval = 0.05f;
    [Tooltip("水平移动速率")] public float MoveHorizontalInterval = 0.1f;
    
}
