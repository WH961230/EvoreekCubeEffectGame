using UnityEngine;
using UnityEngine.UI;

public static class GameCaculater
{
    public static void Move(Vector2Int offset)
    {
        if (GameData.parentTran == null)
        {
            return;
        }

        for (var i = 0; i < GameData.LockShape.nodes.Length; ++i)
        {
            var oldColumn = GameData.LockShape.nodes[i].column;
            var oldLine = GameData.LockShape.nodes[i].line;
            var nextLine = oldLine + offset.x;
            var nextColumn = oldColumn + offset.y;
            var isNextLineOutOfRang = CheckLineOutOfRange(nextLine);
            var isNextLineHasNode = CheckNodeHasNode(nextLine, oldColumn);
            var isNextColumnOutOfRang = CheckColumnOutOfRange(nextColumn);
            var isNextColumnHasNode = CheckNodeHasNode(oldLine, nextColumn);
            if (isNextColumnOutOfRang || isNextColumnHasNode)
            {
                return;
            }
            if (isNextLineOutOfRang || isNextLineHasNode)
            {
                Debug.Log("node is landed");
                SetKeyNodeOfShape(false);
                SetLockNodeOfShape(false);
                SetHasNodeOfShape(true);
                GameData.isLanded = true;
                UpdateLockShapeToNodePlane();
                return;
            }
        }

        for (int i = 0; i < GameData.LockShape.nodes.Length; i++)
        {
            GameData.LockShape.nodes[i].line += offset.x;
            GameData.LockShape.nodes[i].column += offset.y;
            GameData.LockShape.nodes[i].nodeTran = GetTranByLineAndColumn(GameData.LockShape.nodes[i].line, GameData.LockShape.nodes[i].column);
        }
    }

    public static bool GameOver()
    {
        foreach (var node in GameData.LockShape.nodes)
        {
            if (node.line <= 4)
            {
                Debug.Log("您已达到最高，游戏结束");
                return true;
            }
        }

        return false;
    }

    public static void Rotate()
    {
        Debug.Log("旋转实现");
    }

    public static void Remove()
    {
        if (GameData.nodePlane == null)
        {
            return;
        }

        var lineNum = 0;
        for (var i = 0; i < GameData.RowCount; i++)
        {
            var isAllHasNode = true;
            for (var j = 0; j < GameData.ColumnCount; j++)
            {
                if (GameData.nodePlane[i, j].isHasNode == false)
                {
                    isAllHasNode = false;
                    break;
                }
            }

            if (isAllHasNode == true)
            {
                Debug.Log(i + "行消除实现");
                lineNum += 1;
                DownNode(i);
            }
        }
        Reward(lineNum);
    }

    private static void DownNode(int lineIndex)
    {
        for (int i = lineIndex - 1; i >= 0; i--)
        {
            for (int j = 0; j < GameData.ColumnCount; j++)
            {
                GameData.nodePlane[i + 1, j].isHasNode = GameData.nodePlane[i, j].isHasNode;
                GameData.nodePlane[i + 1, j].nodeTran.GetComponent<Image>().color = GameData.nodePlane[i, j].nodeTran.GetComponent<Image>().color;
                GameData.nodePlane[i + 1, j].isKeyNode = false;
                GameData.nodePlane[i + 1, j].isLockNode = false;
            }
        }
    }

    private static void Reward(int lineNum)
    {
        var addScore = 0;
        if (lineNum == 1)
        {
            addScore = 10;
        } 
        else if (lineNum == 2)
        {
            addScore = 30;
        }
        else if (lineNum == 3)
        {
            addScore = 60;
        }
        else if (lineNum == 4)
        {
            addScore = 100;
        }
        GameData.Score += addScore;
        Debug.Log("奖励实现 增加 + " + addScore + "# 总积分： " + GameData.Score);
    }
    
    // public static void Rotate()
    // {
    //      Node[] tempNodes = new Node[4];
    //      for (var i = 0; i < GameData.LockShape.nodes.Length; i++)
    //      {
    //          if (i == 0) continue;
    //          
    //          var up = GameData.LockShape.nodes[i].line - GameData.LockShape.nodes[0].line;
    //          var left = GameData.LockShape.nodes[i].column - GameData.LockShape.nodes[0].column;
    //
    //          if (up == 0 || left == 0)
    //          {
    //              if (up < 0 || up > 0)
    //              {
    //                  left = -up;
    //                  up = 0;
    //              }
    //              else if (left < 0 || left > 0)
    //              {
    //                  up = left;
    //                  left = 0;
    //              }
    //          }
    //          else
    //          {
    //              var tempUp = up;
    //              up = left;
    //              left = -tempUp;
    //          }
    //
    //          var nextLine = GameData.LockShape.nodes[0].line + up;
    //          var nextColumn = GameData.LockShape.nodes[0].column + left;
    //
    //          if (CheckNodeHasNode(nextLine,nextColumn) || CheckOutOfRange(nextLine,nextColumn))
    //          {
    //              for (var h = 0; h < GameData.LockShape.nodes.Length; h++)
    //              {
    //                  GameData.LockShape.nodes[h].isHasNode = true;
    //              }
    //              return;
    //          }
    //
    //          tempNodes[i].line = nextLine;
    //          tempNodes[i].column = nextColumn;
    //          tempNodes[i].nodeTran = GetTranByLineAndColumn(nextLine, nextColumn);
    //          tempNodes[i].isHasNode = true;
    //      }
    //
    //      GameData.LockShape.nodes = tempNodes;
    // }
    public static void ResetState()
    {
        GameData.isLanded = false;
    }

    private static void SetKeyNodeOfShape(bool value)
    {
        if (GameData.LockShape == null)
        {
            return;
        }

        GameData.LockShape.nodes[0].isKeyNode = value;
    }

    private static void SetLockNodeOfShape(bool value)
    {
        if (GameData.LockShape == null)
        {
            return;
        }

        for (var i = 0; i < GameData.LockShape.nodes.Length; i++)
        {
            GameData.LockShape.nodes[i].isLockNode = value;
        }
    }

    private static void SetHasNodeOfShape(bool value)
    {
        if (GameData.LockShape == null)
        {
            return;
        }

        for (var i = 0; i < GameData.LockShape.nodes.Length; i++)
        {
            GameData.LockShape.nodes[i].isHasNode = value;
        }
    }

    private static bool CheckColumnOutOfRange(int column)
    {
        return column >= GameData.TotalColumn ||column < 0;
    }

    private static bool CheckLineOutOfRange(int line)
    {
        return line >= GameData.TotalLine || line < 0;
    }

    private static bool CheckNodeHasNode(int line, int column)
    {
        if (GameData.nodePlane == null)
        {
            Debug.LogError("nodeplane is null");
            return true;
        }

        return GameData.GetNodeOfNodePlane(line,column).isHasNode;
    }

    private static Transform GetTranByLineAndColumn(int line, int column)
    {
        return GameData.parentTran.GetChild(line).GetChild(column);
    }

    private static void UpdateLockShapeToNodePlane()
    {
        if (GameData.LockShape == null || GameData.nodePlane == null)
        {
            Debug.LogError("lockshape or nodeplane is null");
            return;
        }

        for (var i = 0; i < GameData.LockShape.nodes.Length; i++)
        {
            var line = GameData.LockShape.nodes[i].line;
            var column = GameData.LockShape.nodes[i].column;
            GameData.nodePlane[line, column].isKeyNode = GameData.LockShape.nodes[i].isKeyNode;
            GameData.nodePlane[line, column].isHasNode = GameData.LockShape.nodes[i].isHasNode;
            GameData.nodePlane[line, column].isLockNode = GameData.LockShape.nodes[i].isLockNode;
            GameData.nodePlane[line, column].nodeTran = GameData.LockShape.nodes[i].nodeTran;
        }
    }
}
