using UnityEngine;

public enum EmShapeType
{ 
    O, I, L, J, S, Z, T,
}

public enum EmAct
{
    Default, MoveLeft, MoveRight, MoveDown, Rotate
}

public enum EmColor
{
    Red, Yellow, Blue, Green,
}

public struct Node
{
    public bool isKeyNode;//初始定义 true -> false
    public bool isHasNode;// false -> true
    public bool isLockNode;//初始定义 -> true -> false
    public int line;//初始定义
    public int column;//初始定义
    public Transform nodeTran;
}

public class Shape
{
    public Node[] nodes;
    public EmShapeType shapeType;
}

public partial class GameData
{
    public static int TotalLine;
    
    public static int TotalColumn;
    
    public static Node[,] nodePlane;
    
    public static Shape LockShape;
    
    public static Transform parentTran;
    //状态
    public static bool isLanded = false;
    
    public static bool isAutoMove = true;
    public static int RowCount => nodePlane.GetLength(0);
    public static int ColumnCount => nodePlane.GetLength(1);
    
    public static EmAct emAct;

    public static bool isCreateShape = false;

    private static bool isLoadPlane = false;
    private static int RowIndex => nodePlane.GetLength(0) - 1;
    private static int ColumnIndex => nodePlane.GetLength(1) - 1;

    public static int Score = 0;

    public static bool isGameOver = false;

    private static SOCreatable config;
    public static void Init()
    {
        var createConfigPath = ConfigManager.Instance.configDic[CreateConfig];
        config = AssetLoader.Instance.Load<SOCreatable>(createConfigPath);

        InitData();
    }

    private static void InitData()
    {
        TotalLine = config.TotalLine;
        TotalColumn = config.TotalColumn;
        nodePlane = new Node[TotalLine, TotalColumn];
    }
    
    public static Node GetNodeOfNodePlane(int line, int column)
    {
        if (line < 0 || column < 0 || line > RowIndex || column > ColumnIndex)
        {
            Debug.LogError("GetNode index out of range");
            return new Node();
        }
        return nodePlane[line, column];
    }
}
