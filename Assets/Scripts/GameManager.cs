using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum EmShapeType
{
    O,
    I,
    L,
    J,
    S,
    Z,
    T
}

public enum EmColor
{
    Red,
    Yellow,
    Blue,
    Green,
}

[Serializable]
public struct Node
{
    public bool isKeyNode;
    public bool isHasNode;
    public bool isLockNode;
    public int line;
    public int column;
    public Transform nodeTran;

    public void SetNodeColor(Color color)
    {
        nodeTran.GetComponent<Image>().color = color;
    }
}

[Serializable]
public class Shape
{
    public Node[] nodes;
    public Node keyNode;
    public EmShapeType shapeType;

    public void SetNodeColor(Color color)
    {
        if (this.nodes == null) return;
        foreach (var node in nodes)
        {
            node.nodeTran.GetComponent<Image>().color = color;
        }
    }
}

public class GameManager : MonoBehaviour
{
    [Tooltip("间隔时间")] private const int TotalLine = 25;
    private const int TotalColumn = 14;
    private const int NodeNum = 4;
    public float time = 1f;
    private float _deployTime = 0f;
    private bool _lockMove = false;
    private bool _isGameOver = false;
    private Shape _lockShape;
    private AudioManager _audioManager;
    private Node[,] _nodePlane = new Node[TotalLine, TotalColumn];
    private readonly Transform[] _lines = new Transform[TotalLine];
    private readonly Transform[][] _columns = new Transform[TotalLine][];
    private bool _createOrder = false;
    private bool _isCanCheckRemove = false;
    private readonly Color DefaultColor = Color.white; 

    private Shape CreateShapeByTypeAndKeyNode(int line, int column, EmShapeType type)
    {
        var shape = new Shape();
        switch (type)
        {
            case EmShapeType.O:
                shape = CreateShapeOfO(line, column);
                Debug.Log("创建图案 O");
                break;
            case EmShapeType.I:
                shape = CreateShapeOfI(line, column);
                Debug.Log("创建图案 I");
                break;
            case EmShapeType.L:
                shape = CreateShapeOfL(line, column);
                Debug.Log("创建图案 L");
                break;
            case EmShapeType.J:
                shape = CreateShapeOfJ(line, column);
                Debug.Log("创建图案 J");
                break;
            case EmShapeType.S:
                shape = CreateShapeOfS(line, column);
                Debug.Log("创建图案 S");
                break;
            case EmShapeType.Z:
                shape = CreateShapeOfZ(line, column);
                Debug.Log("创建图案 Z");
                break;
            case EmShapeType.T:
                shape = CreateShapeOfT(line, column);
                Debug.Log("创建图案 T");
                break;
        }

        return shape;
    }

    private Color CreateRandomColor()
    {
        var enums = Enum.GetValues(typeof(EmColor));
        var typeIndex = UnityEngine.Random.Range(0, enums.Length);
        var type = (EmColor) enums.GetValue(typeIndex);

        switch (type)
        {
            case EmColor.Red:
                Debug.Log("创建随机颜色红色");
                return Color.red;
            case EmColor.Blue:
                Debug.Log("创建随机颜色蓝色");
                return Color.blue;
            case EmColor.Green:
                Debug.Log("创建随机颜色绿色");
                return Color.green;
            case EmColor.Yellow:
                Debug.Log("创建随机颜色黄色");
                return Color.yellow;
            default:
                Debug.Log("创建随机颜色红色");
                return Color.red;
        }
    }

    private Shape CreateShapeOfI(int line, int column)
    {
        var shape = new Shape();
        if (line - 1 >= 0 && line + 2 < TotalLine)
        {
            if (column >= 0 && column < TotalColumn)
            {
                var nodes = new Node[NodeNum];

                nodes[0] = SetNode(line, column, true, true, false);
                nodes[1] = SetNode(line - 1, column, false, true, false);
                nodes[2] = SetNode(line + 1, column, false, true, false);
                nodes[3] = SetNode(line + 2, column, false, true, false);

                shape.keyNode = nodes[0];
                shape.nodes = nodes;
                shape.shapeType = EmShapeType.I;
            }
        }

        return shape;
    }

    private Shape CreateShapeOfO(int line, int column)
    {
        var shape = new Shape();
        if (line >= 0 && line + 1 < TotalLine)
        {
            if (column >= 0 && column + 1 < TotalColumn)
            {
                var nodes = new Node[NodeNum];

                nodes[0] = SetNode(line, column, true, true, false);
                nodes[1] = SetNode(line, column + 1, false, true, false);
                nodes[2] = SetNode(line + 1, column, false, true, false);
                nodes[3] = SetNode(line + 1, column + 1, false, true, false);

                shape.keyNode = nodes[0];
                shape.nodes = nodes;
                shape.shapeType = EmShapeType.I;
            }
        }

        return shape;
    }

    private Shape CreateShapeOfJ(int line, int column)
    {
        var shape = new Shape();
        if (line - 1 >= 0 && line < TotalLine)
        {
            if (column - 1 >= 0 && column + 1 < TotalColumn)
            {
                var nodes = new Node[NodeNum];
                nodes[0] = SetNode(line, column, true, true, false);
                nodes[1] = SetNode(line, column + 1, false, true, false);
                nodes[2] = SetNode(line - 1, column - 1, false, true, false);
                nodes[3] = SetNode(line, column - 1, false, true, false);
                shape.keyNode = nodes[0];
                shape.nodes = nodes;
                shape.shapeType = EmShapeType.J;
            }
        }

        return shape;
    }

    private Shape CreateShapeOfL(int line, int column)
    {
        var shape = new Shape();
        if (line - 1 >= 0 && line < TotalLine)
        {
            if (column - 1 >= 0 && column + 1 < TotalColumn)
            {
                var nodes = new Node[NodeNum];
                nodes[0] = SetNode(line, column, true, true, false);
                nodes[1] = SetNode(line, column + 1, false, true, false);
                nodes[2] = SetNode(line - 1, column + 1, false, true, false);
                nodes[3] = SetNode(line, column - 1, false, true, false);
                shape.keyNode = nodes[0];
                shape.nodes = nodes;
                shape.shapeType = EmShapeType.L;
            }
        }

        return shape;
    }

    private Shape CreateShapeOfS(int line, int column)
    {
        var shape = new Shape();
        if (line >= 0 && line + 1 < TotalLine)
        {
            if (column - 1 >= 0 && column + 1 < TotalColumn)
            {
                var nodes = new Node[NodeNum];
                nodes[0] = SetNode(line, column, true, true, false);
                nodes[1] = SetNode(line, column + 1, false, true, false);
                nodes[2] = SetNode(line + 1, column, false, true, false);
                nodes[3] = SetNode(line + 1, column - 1, false, true, false);
                shape.keyNode = nodes[0];
                shape.nodes = nodes;
                shape.shapeType = EmShapeType.S;
            }
        }

        return shape;
    }

    private Shape CreateShapeOfZ(int line, int column)
    {
        var shape = new Shape();
        if (line >= 0 && line + 1 < TotalLine)
        {
            if (column - 1 >= 0 && column + 1 < TotalColumn)
            {
                var nodes = new Node[NodeNum];
                nodes[0] = SetNode(line, column, true, true, false);
                nodes[1] = SetNode(line, column - 1, false, true, false);
                nodes[2] = SetNode(line + 1, column, false, true, false);
                nodes[3] = SetNode(line + 1, column + 1, false, true, false);
                shape.keyNode = nodes[0];
                shape.nodes = nodes;
                shape.shapeType = EmShapeType.Z;
            }
        }

        return shape;
    }

    private Shape CreateShapeOfT(int line, int column)
    {
        var shape = new Shape();
        if (line - 1 >= 0 && line < TotalLine)
        {
            if (column - 1 >= 0 && column + 1 < TotalColumn)
            {
                var nodes = new Node[NodeNum];
                nodes[0] = SetNode(line, column, true, true, false);
                nodes[1] = SetNode(line - 1, column, false, true, false);
                nodes[2] = SetNode(line, column + 1, false, true, false);
                nodes[3] = SetNode(line, column - 1, false, true, false);
                shape.keyNode = nodes[0];
                shape.nodes = nodes;
                shape.shapeType = EmShapeType.T;
            }
        }

        return shape;
    }

    private Node SetNode(int line, int column, bool isKeyNode, bool isHasNode, bool isLockNode)
    {
        Node tempNode;
        tempNode.line = line;
        tempNode.column = column;
        tempNode.isKeyNode = isKeyNode;
        tempNode.isHasNode = isHasNode;
        tempNode.isLockNode = isLockNode;
        tempNode.nodeTran = GetNodeTransByLineAndColumn(line, column);

        return tempNode;
    }

    private Node[,] CreateNodePlane(int line, int column)
    {
        if (line > 0 && column > 0)
        {
            var nodePlane = new Node[line, column];
            for (var i = 0; i < line; i++)
            {
                for (var j = 0; j < column; j++)
                {
                    nodePlane[i, j] = SetNode(i, j, false, false, false);
                    nodePlane[i, j].SetNodeColor(DefaultColor);
                }
            }
            Debug.Log("创建成功 NodePlane");
            return nodePlane;
        }

        return null;
    }

    private Transform GetNodeTransByLineAndColumn(int line, int column)
    {
        if (CheckNextOutOfIndex(line, column) == false)
        {
            return this.transform.GetChild(line).GetChild(column);
        }

        return null;
    }

    private bool CheckLineAndColumnHasNode(int lineIndex, int columnIndex)
    {
        if (CheckNextOutOfIndex(lineIndex, columnIndex) == true)
        {
            return true;
        }

        if (_nodePlane[lineIndex, columnIndex].isHasNode == true && _nodePlane[lineIndex, columnIndex].isLockNode == false)
        {
            return true;
        }

        return false;
    }

    private bool CheckNextOutOfIndex(int line, int column)
    {
        return line >= TotalLine || column >= TotalColumn || line < 0 || column < 0;
    }

    private void LightColorOfPlane(Node[,] plane)
    {
        for (var i = 0; i < plane.GetLength(0); i++)
        {
            for (var j = 0; j < plane.GetLength(1); j++)
            {
                var node = plane[i, j];
                LightColorOfNode(node, node.nodeTran.GetComponent<Image>().color);
            }
        }
    }

    private void LightColorOfShape(Shape shape, Color color)
    {
        for (var i = 0; i < shape.nodes.Length; i++)
        {
            var node = shape.nodes[i];
            LightColorOfNode(node, color);
        }
    }

    private static void LightColorOfNode(Node node, Color color)
    {
        if (node.nodeTran == null || node.nodeTran.GetComponent<Image>() == null) return;
        node.nodeTran.GetComponent<Image>().color = color;
        var isWhite = Math.Abs(color.r - 1f) < 0.01f && Math.Abs(color.b - 1f) < 0.01f &&
                      Math.Abs(color.g - 1f) < 0.01f;
        if (isWhite)
        {
            var c = node.nodeTran.GetComponent<Image>().color;
            node.nodeTran.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
        }
        else
        {
            var c = node.nodeTran.GetComponent<Image>().color;
            node.nodeTran.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.6f);
        }
    }

    public void Rotate()
    {
        var image = _lockShape.nodes[0].nodeTran.GetComponent<Image>();
        if (_lockShape?.nodes == null) return;
        if (_lockMove)
        {
            LightColorOfShape(_lockShape, image.color);
            return;
        }

        for (var i = 0; i < _lockShape.nodes.Length; i++)
        {
            _lockShape.nodes[i].isHasNode = false;
        }

        if (RotateByNodes(_lockShape.nodes)) return;
        LightColorOfShape(_lockShape, image.color);
        _lockMove = false;
    }

    private bool RotateByNodes(Node[] nodes)
    {
        var color = nodes[0].nodeTran.GetComponent<Image>().color;
        LightColorOfShape(_lockShape, Color.white);
        for (var i = 0; i < nodes.Length; i++)
        {
            if (i == 0) continue;
            
            var up = nodes[i].line - nodes[0].line;
            var left = nodes[i].column - nodes[0].column;

            if (up == 0 || left == 0)
            {
                if (up < 0 || up > 0)
                {
                    left = -up;
                    up = 0;
                }
                else if (left < 0 || left > 0)
                {
                    up = left;
                    left = 0;
                }
            }
            else
            {
                var tempUp = up;
                up = left;
                left = -tempUp;
            }

            var finalLine = nodes[0].line + up;
            var finalColumn = nodes[0].column + left;
            var checkHasNode = CheckLineAndColumnHasNode(finalLine, finalColumn);
            var checkOutOfIndex = CheckNextOutOfIndex(finalLine, finalColumn);

            if (checkHasNode || checkOutOfIndex)
            {
                EditorApplication.isPaused = true;
                return true;
            }

            nodes[i].line = finalLine;
            nodes[i].column = finalColumn;
            nodes[i].nodeTran = GetNodeTransByLineAndColumn(finalLine, finalColumn);
            nodes[i].isHasNode = true;
            
        }
        LightColorOfShape(_lockShape,color);

        return false;
    }

    public void Move(Shape shape, Vector2Int offset)
    {
        if (shape == null || shape.nodes == null)
        {
            return;
        }

        var nodes = shape.nodes;
        var color = nodes[0].nodeTran.GetComponent<Image>().color;

        if (_lockMove == true)
        {
            return;
        }

        LightColorOfShape(_lockShape, Color.white);
        Vector2Int position;

        var isCanMove = true;
        var isCanSideMove = true;

        if (offset == new Vector2Int(1, 0))
        {
            for (var i = 0; i < nodes.Length; i++)
            {
                position = new Vector2Int(nodes[i].line, nodes[i].column) + offset;
                if (CheckNextOutOfIndex(position.x, position.y) == true ||
                    CheckLineAndColumnHasNode(position.x, position.y) == true)
                {
                    _createOrder = true;
                    _lockMove = true;
                    isCanMove = false;
                    isCanSideMove = false;
                    _isCanCheckRemove = true;
                    for (int j = 0; j < nodes.Length; j++)
                    {
                        _nodePlane[nodes[j].line, nodes[j].column].isHasNode = true;
                    }
                }
            }
        }
        else
        {
            for (var i = 0; i < nodes.Length; i++)
            {
                position = new Vector2Int(nodes[i].line, nodes[i].column) + offset;
                if (CheckNextOutOfIndex(position.x, position.y) == true ||
                    CheckLineAndColumnHasNode(position.x, position.y) == true)
                {
                    isCanSideMove = false;
                    break;
                }
            }
        }

        if (isCanMove)
        {
            if (isCanSideMove == false && (offset == new Vector2Int(0, 1) || offset == new Vector2Int(0, -1)))
            {
            }
            else
            {
                for (var i = 0; i < nodes.Length; i++)
                {
                    position = new Vector2Int(nodes[i].line, nodes[i].column) + offset;
                    nodes[i].line = position.x;
                    nodes[i].column = position.y;
                    nodes[i].nodeTran = GetNodeTransByLineAndColumn(position.x, position.y);
                }
            }

            _lockMove = false;
        }

        LightColorOfShape(_lockShape, color);
    }

    private void Start()
    {
        _nodePlane = CreateNodePlane(TotalLine, TotalColumn);
        LightColorOfPlane(_nodePlane);
    }

    void Update()
    {
        InputSystem();
        if (_deployTime <= 0)
        {
            RemoveSystem();
            GameOverSystem();
            CreatableSystem();
            AutoSystem();
            _deployTime = time;
        }
        else
        {
            _deployTime -= Time.deltaTime;
        }
    }

    void InputSystem()
    {
        InputManager.KeyOnce(KeyCode.Space, Rotate);
        InputManager.KeyOnce(KeyCode.S, Move, _lockShape, new Vector2Int(1, 0));
        InputManager.KeyOnce(KeyCode.A, Move, _lockShape, new Vector2Int(0, -1));
        InputManager.KeyOnce(KeyCode.D, Move, _lockShape, new Vector2Int(0, 1));
    }

    private void AutoSystem()
    {
        if (_lockShape != null)
        {
            Move(_lockShape, new Vector2Int(1, 0));
        }
        else
        {
            _createOrder = true;
        }
    }

    private void CreatableSystem()
    {
        if (_createOrder == false) return;

        _createOrder = false;
        _lockMove = false;

        var enums = Enum.GetValues(typeof(EmShapeType));
        var typeIndex = UnityEngine.Random.Range(0, enums.Length);
        var type = (EmShapeType) enums.GetValue(typeIndex);
        Debug.Log("======================== FGX ==========================");
        _lockShape = CreateShapeByTypeAndKeyNode(1, 7, type);

        var color = CreateRandomColor();
        _lockShape.SetNodeColor(color);

        LightColorOfShape(_lockShape, _lockShape.nodes[0].nodeTran.GetComponent<Image>().color);
    }

    private void RemoveSystem()
    {
        if (_isCanCheckRemove != true) return;

        _isCanCheckRemove = false;
        for (var i = 0; i < TotalLine; i++)
        {
            for (var j = 0; j < TotalColumn; j++)
            {
                var position = new Vector2Int(i, j);
                if (_nodePlane[position.x, position.y].isHasNode == false)
                {
                    break;
                }
                else if (j == TotalColumn - 1)
                {
                    for (var k = i - 1; k >= 0; k--)
                    {
                        for (var l = 0; l < TotalColumn; l++)
                        {
                            _nodePlane[k + 1, l].isHasNode = _nodePlane[k, l].isHasNode;
                            _nodePlane[k + 1, l].isLockNode = false;
                            _nodePlane[k + 1, l].isKeyNode = false;

                            LightColorOfNode(_nodePlane[k + 1, l],
                                _nodePlane[k, l].nodeTran.GetComponent<Image>().color);
                        }
                    }

                    LightColorOfPlane(_nodePlane);
                    AudioManager.instance.Play(EM_AUDIO_TYPE.REMOVE);
                }
            }
        }
    }

    private void GameOverSystem()
    {
        if (!_isGameOver) return;
        
        Debug.Log("游戏结束");
        _isGameOver = false;
    }

    private static class InputManager
    {
        public static void KeyOnce(KeyCode keyCode, Action<Shape, Vector2Int> action, Shape shape, Vector2Int offSet)
        {
            if (Input.GetKeyDown(keyCode))
            {
                action(shape, offSet);
            }
        }

        public static void KeyOnce(KeyCode keyCode, Action action)
        {
            if (Input.GetKeyDown(keyCode))
            {
                action();
            }
        }
    }
}