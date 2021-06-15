// using System;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.UI;
//
// public enum EmShapeType
// {
//     O,
//     I,
//     L,
//     J,
//     S,
//     Z,
//     T
// }
//
// public enum EmColor
// {
//     Red,
//     Yellow,
//     Blue,
//     Green,
// }
//
// [Serializable]
// public struct Node
// {
//     public bool isKeyNode;
//     public bool isHasNode;
//     public bool isLockNode;
//     public int line;
//     public int column;
//     public Transform nodeTran;
//
//     public void SetNodeColor(Color color)
//     {
//         nodeTran.GetComponent<Image>().color = color;
//     }
//
//     public Color GetNodeColor()
//     {
//         return nodeTran.GetComponent<Image>().color;
//     }
//     
//     public Image GetNodeImage()
//     {
//         if (nodeTran == null) return null;
//         return nodeTran.GetComponent<Image>();
//     }
// }
//
// [Serializable]
// public class Shape
// {
//     public Node[] nodes;
//     public Node keyNode;
//     public EmShapeType shapeType;
//
//     public void SetNodeColor(Color color)
//     {
//         if (this.nodes == null) return;
//         foreach (var node in nodes)
//         {
//             node.nodeTran.GetComponent<Image>().color = color;
//         }
//     }
//
//     public Color GetNodeColor()
//     {
//         if (this.nodes == null || this.nodes[0].nodeTran == null ||
//             this.nodes[0].nodeTran.GetComponent<Image>() == null) return default;
//         return nodes[0].nodeTran.GetComponent<Image>().color;
//     }
//
//     public Image GetNodeImage()
//     {
//         if (this.nodes == null) return default;
//         return nodes[0].nodeTran.GetComponent<Image>();
//     }
// }
//
// public class GameManager : MonoBehaviour
// {
//     [Tooltip("间隔时间")] private const int TotalLine = 25;
//     private const int TotalColumn = 14;
//     private const int NodeNum = 4;
//     public float time = 1f;
//     private float _deployTime = 0f;
//     private bool _lockMove = false;
//     private bool _isGameOver = false;
//     private Shape _lockShape;
//     private AudioManager _audioManager;
//     private Node[,] _nodePlane = new Node[TotalLine, TotalColumn];
//     private readonly Transform[] _lines = new Transform[TotalLine];
//     private readonly Transform[][] _columns = new Transform[TotalLine][];
//     private bool _createOrder = false;
//     private bool _isCanCheckRemove = false;
//     private readonly Color DefaultColor = Color.white;
//
//     private bool CheckLineAndColumnHasNode(int lineIndex, int columnIndex)
//     {
//         // if (CheckNextOutOfIndex(lineIndex, columnIndex) == true)
//         // {
//         //     return true;
//         // }
//         //
//         // if (_nodePlane[lineIndex, columnIndex].isHasNode == true && _nodePlane[lineIndex, columnIndex].isLockNode == false)
//         // {
//         //     return true;
//         // }
//
//         return false;
//     }
//
//     private void LightColorOfPlane(Node[,] plane)
//     {
//         for (var i = 0; i < plane.GetLength(0); i++)
//         {
//             for (var j = 0; j < plane.GetLength(1); j++)
//             {
//                 var node = plane[i, j];
//                 LightColorOfNode(node, node.GetNodeColor());
//             }
//         }
//     }
//
//     private void LightColorOfShape(Shape shape, Color color)
//     {
//         for (var i = 0; i < shape.nodes.Length; i++)
//         {
//             var node = shape.nodes[i];
//             LightColorOfNode(node, color);
//         }
//     }
//
//     private static void LightColorOfNode(Node node, Color color)
//     {
//         if (node.nodeTran == null || node.nodeTran.GetComponent<Image>() == null) return;
//         node.SetNodeColor(color);
//         var isWhite = Math.Abs(color.r - 1f) < 0.01f && Math.Abs(color.b - 1f) < 0.01f &&
//                       Math.Abs(color.g - 1f) < 0.01f;
//         if (isWhite)
//         {
//             node.SetNodeColor(new Color(1f, 1f, 1f, 0.3f));
//         }
//         else
//         {
//             var c = node.GetNodeColor();
//             node.SetNodeColor(new Color(c.r, c.g, c.b, 0.6f));
//         }
//     }
//
//     public void Rotate()
//     {
//         if (_lockShape?.nodes == null) return;
//         if (_lockMove)
//         {
//             LightColorOfShape(_lockShape, _lockShape.GetNodeColor());
//             return;
//         }
//
//         for (var i = 0; i < _lockShape.nodes.Length; i++)
//         {
//             _lockShape.nodes[i].isHasNode = false;
//         }
//
//         if (RotateByNodes(_lockShape.nodes)) return;
//         LightColorOfShape(_lockShape, _lockShape.GetNodeColor());
//         _lockMove = false;
//     }
//
//     private bool RotateByNodes(Node[] nodes)
//     {
//         var color = nodes[0].GetNodeColor();
//         LightColorOfShape(_lockShape, Color.white);
//         Node[] tempNodes = new Node[4];
//         for (var i = 0; i < nodes.Length; i++)
//         {
//             if (i == 0) continue;
//             
//             var up = nodes[i].line - nodes[0].line;
//             var left = nodes[i].column - nodes[0].column;
//
//             if (up == 0 || left == 0)
//             {
//                 if (up < 0 || up > 0)
//                 {
//                     left = -up;
//                     up = 0;
//                 }
//                 else if (left < 0 || left > 0)
//                 {
//                     up = left;
//                     left = 0;
//                 }
//             }
//             else
//             {
//                 var tempUp = up;
//                 up = left;
//                 left = -tempUp;
//             }
//
//             var finalLine = nodes[0].line + up;
//             var finalColumn = nodes[0].column + left;
//             var checkHasNode = CheckLineAndColumnHasNode(finalLine, finalColumn);
//             var checkOutOfIndex = CheckNextOutOfIndex(finalLine, finalColumn);
//
//             if (checkHasNode || checkOutOfIndex)
//             {
//                 EditorApplication.isPaused = true;
//                 for (var h = 0; h < _lockShape.nodes.Length; h++)
//                 {
//                     _lockShape.nodes[h].isHasNode = true;
//                 }
//                 LightColorOfShape(_lockShape,color);
//                 return true;
//             }
//
//             tempNodes[i].line = finalLine;
//             tempNodes[i].column = finalColumn;
//             tempNodes[i].nodeTran = GetNodeTransByLineAndColumn(finalLine, finalColumn);
//             tempNodes[i].isHasNode = true;
//         }
//
//         _lockShape.nodes = tempNodes;
//         LightColorOfShape(_lockShape,color);
//
//         return false;
//     }
//
//     public void Move(Shape shape, Vector2Int offset)
//     {
//         if (shape == null || shape.nodes == null)
//         {
//             return;
//         }
//         var nodes = shape.nodes;
//         var color = shape.GetNodeColor();
//
//         if (_lockMove == true)
//         {
//             return;
//         }
//
//         LightColorOfShape(_lockShape, Color.white);
//         Vector2Int position;
//
//         var isCanMove = true;
//         var isCanSideMove = true;
//
//         if (offset == new Vector2Int(1, 0))
//         {
//             for (var i = 0; i < nodes.Length; i++)
//             {
//                 position = new Vector2Int(nodes[i].line, nodes[i].column) + offset;
//                 if (CheckNextOutOfIndex(position.x, position.y) == true ||
//                     CheckLineAndColumnHasNode(position.x, position.y) == true)
//                 {
//                     _createOrder = true;
//                     _lockMove = true;
//                     isCanMove = false;
//                     isCanSideMove = false;
//                     _isCanCheckRemove = true;
//                     for (int j = 0; j < nodes.Length; j++)
//                     {
//                         _nodePlane[nodes[j].line, nodes[j].column].isHasNode = true;
//                     }
//                 }
//             }
//         }
//         else
//         {
//             for (var i = 0; i < nodes.Length; i++)
//             {
//                 position = new Vector2Int(nodes[i].line, nodes[i].column) + offset;
//                 if (CheckNextOutOfIndex(position.x, position.y) == true ||
//                     CheckLineAndColumnHasNode(position.x, position.y) == true)
//                 {
//                     isCanSideMove = false;
//                     break;
//                 }
//             }
//         }
//
//         if (isCanMove)
//         {
//             if (isCanSideMove == false && (offset == new Vector2Int(0, 1) || offset == new Vector2Int(0, -1)))
//             {
//             }
//             else
//             {
//                 for (var i = 0; i < nodes.Length; i++)
//                 {
//                     position = new Vector2Int(nodes[i].line, nodes[i].column) + offset;
//                     nodes[i].line = position.x;
//                     nodes[i].column = position.y;
//                     nodes[i].nodeTran = GetNodeTransByLineAndColumn(position.x, position.y);
//                 }
//             }
//
//             _lockMove = false;
//         }
//
//         LightColorOfShape(_lockShape, color);
//     }
//
//     private void Start()
//     {
//         _nodePlane = CreateNodePlane(TotalLine, TotalColumn);
//         LightColorOfPlane(_nodePlane);
//     }
//
//     void Update()
//     {
//         InputSystem();
//         if (_deployTime <= 0)
//         {
//             RemoveSystem();
//             GameOverSystem();
//             CreatableSystem();
//             AutoSystem();
//             _deployTime = time;
//         }
//         else
//         {
//             _deployTime -= Time.deltaTime;
//         }
//     }
//
//     void InputSystem()
//     {
//         InputManager.KeyOnce(KeyCode.Space, Rotate);
//         InputManager.KeyOnce(KeyCode.S, Move, _lockShape, new Vector2Int(1, 0));
//         InputManager.KeyOnce(KeyCode.A, Move, _lockShape, new Vector2Int(0, -1));
//         InputManager.KeyOnce(KeyCode.D, Move, _lockShape, new Vector2Int(0, 1));
//     }
//
//     private void AutoSystem()
//     {
//         if (_lockShape != null)
//         {
//             Move(_lockShape, new Vector2Int(1, 0));
//         }
//         else
//         {
//             _createOrder = true;
//         }
//     }
//
//     private void CreatableSystem()
//     {
//         if (_createOrder == false) return;
//         _createOrder = false;
//         _lockMove = false;
//         var enums = Enum.GetValues(typeof(EmShapeType));
//         var typeIndex = UnityEngine.Random.Range(0, enums.Length);
//         var type = (EmShapeType) enums.GetValue(typeIndex);
//         Debug.Log("======================== FGX ==========================");
//         _lockShape = CreateShapeByTypeAndKeyNode(1, 7, type);
//         var color = CreateRandomColor();
//         _lockShape.SetNodeColor(color);
//         LightColorOfShape(_lockShape, _lockShape.GetNodeColor());
//     }
//
//     private void RemoveSystem()
//     {
//         if (_isCanCheckRemove != true) return;
//
//         _isCanCheckRemove = false;
//         for (var i = 0; i < TotalLine; i++)
//         {
//             for (var j = 0; j < TotalColumn; j++)
//             {
//                 var position = new Vector2Int(i, j);
//                 if (_nodePlane[position.x, position.y].isHasNode == false)
//                 {
//                     break;
//                 }
//                 else if (j == TotalColumn - 1)
//                 {
//                     for (var k = i - 1; k >= 0; k--)
//                     {
//                         for (var l = 0; l < TotalColumn; l++)
//                         {
//                             _nodePlane[k + 1, l].isHasNode = _nodePlane[k, l].isHasNode;
//                             _nodePlane[k + 1, l].isLockNode = false;
//                             _nodePlane[k + 1, l].isKeyNode = false;
//
//                             LightColorOfNode(_nodePlane[k + 1, l],
//                                 _nodePlane[k, l].GetNodeColor());
//                         }
//                     }
//
//                     LightColorOfPlane(_nodePlane);
//                     AudioManager.instance.Play(EM_AUDIO_TYPE.REMOVE);
//                 }
//             }
//         }
//     }
//
//     private void GameOverSystem()
//     {
//         if (!_isGameOver) return;
//         
//         Debug.Log("游戏结束");
//         _isGameOver = false;
//     }
//
//     private static class InputManager
//     {
//         public static void KeyOnce(KeyCode keyCode, Action<Shape, Vector2Int> action, Shape shape, Vector2Int offSet)
//         {
//             if (Input.GetKeyDown(keyCode))
//             {
//                 action(shape, offSet);
//             }
//         }
//
//         public static void KeyOnce(KeyCode keyCode, Action action)
//         {
//             if (Input.GetKeyDown(keyCode))
//             {
//                 action();
//             }
//         }
//     }
// }