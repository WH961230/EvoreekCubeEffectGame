using UnityEngine;

public partial class GameData
{
    public static Node[,] InitNodePlane(int line, int column)
    {
        var nodePlane = new Node[line, column];
        for (var i = 0; i < line; ++i)
        {
            for (var j = 0; j < column; ++j)
            {
                InitNodeOfNodePlane(nodePlane,i, j, false, false, false);
                InitNodeTranOfNodePlane(nodePlane, GameData.parentTran, i, j);
            }
        }
        return nodePlane;
    }

    public static Shape InitShape(int line, int column, int nodeNum, EmShapeType type)
    {
        var shape = new Shape();
        int[,] nodeValList;
        switch (type)
        {
            case EmShapeType.O:
                nodeValList = new int[4,2] {{line,column},{line,column+1},{line+1,column},{line+1,column+1}};
                shape = InitShape(line, column, nodeNum, nodeValList, EmShapeType.O);
                Debug.Log("创建图案 O");
                break;
            case EmShapeType.I:
                nodeValList = new int[4,2] {{line,column},{line-1,column},{line+1,column},{line+2,column}};
                shape = InitShape(line, column, nodeNum, nodeValList, EmShapeType.I);
                Debug.Log("创建图案 I");
                break;
            case EmShapeType.L:
                nodeValList = new int[4,2] {{line,column},{line,column+1},{line-1,column+1},{line,column-1}};
                shape = InitShape(line, column, nodeNum, nodeValList, EmShapeType.L);
                Debug.Log("创建图案 L");
                break;
            case EmShapeType.J:
                nodeValList = new int[4,2] {{line,column},{line,column+1},{line-1,column-1},{line,column-1}};
                shape = InitShape(line, column, nodeNum, nodeValList, EmShapeType.J);
                Debug.Log("创建图案 J");
                break;
            case EmShapeType.S:
                nodeValList = new int[4,2] {{line,column},{line,column+1},{line+1,column},{line+1,column-1}};
                shape = InitShape(line, column, nodeNum, nodeValList, EmShapeType.S);
                Debug.Log("创建图案 S");
                break;
            case EmShapeType.Z:
                nodeValList = new int[4,2] {{line,column},{line,column-1},{line+1,column},{line+1,column+1}};
                shape = InitShape(line, column, nodeNum, nodeValList, EmShapeType.Z);
                Debug.Log("创建图案 Z");
                break;
            case EmShapeType.T:
                nodeValList = new int[4,2] {{line,column},{line-1,column},{line,column+1},{line,column-1}};
                shape = InitShape(line, column, nodeNum, nodeValList, EmShapeType.T);
                Debug.Log("创建图案 T");
                break;
            default:
                nodeValList = new int[4,2] {{line,column},{line,column+1},{line+1,column},{line+1,column+1}};
                shape = InitShape(line, column, nodeNum, nodeValList, EmShapeType.O);
                Debug.Log("创建图案 O");
                break;
        }
        return shape;
    }
    
    private static Shape InitShape(int line, int column, int nodeNum, int[,] nodeValList, EmShapeType type)
    {
        var shape = new Shape(); 
        // Todo 判断生成图形位置是否越界
        if (line >= 0 && line + 1 < GameData.TotalLine)
        {
            if (column >= 0 && column + 1 < GameData.TotalColumn)
            {
                var nodes = new Node[nodeNum];
                nodes[0] = InitNode(nodeValList[0,0], nodeValList[0,1], true, false, true);
                nodes[0].nodeTran = InitNodeTran(GameData.parentTran, nodeValList[0,0], nodeValList[0,1]);
                nodes[1] = InitNode(nodeValList[1,0], nodeValList[1,1], false, false, true);
                nodes[1].nodeTran = InitNodeTran(GameData.parentTran, nodeValList[1,0], nodeValList[1,1]);
                nodes[2] = InitNode(nodeValList[2,0], nodeValList[2,1], false, false, true);
                nodes[2].nodeTran = InitNodeTran(GameData.parentTran, nodeValList[2,0], nodeValList[2,1]);
                nodes[3] = InitNode(nodeValList[3,0], nodeValList[3,1], false, false, true);
                nodes[3].nodeTran = InitNodeTran(GameData.parentTran, nodeValList[3,0], nodeValList[3,1]);
                shape.nodes = nodes;
                shape.shapeType = type;
            }
        }
     
        return shape;
    }

    private static Node InitNode(int line, int column, bool isKeyNode, bool isHasNode, bool isLockNode)
    {
        var tempNode = new Node();
        tempNode.line = line;
        tempNode.column = column;
        tempNode.isKeyNode = isKeyNode;
        tempNode.isHasNode = isHasNode;
        tempNode.isLockNode = isLockNode;
        return tempNode;
    }

    private static void InitNodeOfNodePlane(Node[,] tempNode, int line, int column, bool isKeyNode, bool isHasNode, bool isLockNode)
    {
        tempNode[line,column].line = line;
        tempNode[line,column].column = column;
        tempNode[line,column].isKeyNode = isKeyNode;
        tempNode[line,column].isHasNode = isHasNode;
        tempNode[line,column].isLockNode = isLockNode;
    }

    private static Transform InitNodeTran(Transform tran, int line, int column)
    {
        if (tran == null || tran.GetChild(line) == null || tran.GetChild(line).GetChild(column) == null)
        {
            Debug.LogError($"wrong with nodeTran {line} {column}");
            return null;
        }

        return tran.GetChild(line).GetChild(column);
    }

    private static void InitNodeTranOfNodePlane(Node[,] tempNode, Transform tran, int line, int column)
    {
        if (tran == null || tran.GetChild(line) == null || tran.GetChild(line).GetChild(column) == null)
        {
            Debug.LogError($"wrong with nodeTran {line} {column}");
            return;
        }

        tempNode[line,column].nodeTran = tran.GetChild(line).GetChild(column);
    }
}
