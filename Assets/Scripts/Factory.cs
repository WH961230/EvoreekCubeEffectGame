// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class Factory
// {
//      private Shape CreateShapeByTypeAndKeyNode(int line, int column, int NodeNum, int TotalLine, int TotalColumn, EmShapeType type)
//         {
//             var shape = new Shape();
//             switch (type)
//             {
//                 case EmShapeType.O:
//                     shape = CreateShapeOfO(line, column, NodeNum, TotalLine, TotalColumn);
//                     Debug.Log("创建图案 O");
//                     break;
//                 case EmShapeType.I:
//                     shape = CreateShapeOfI(line, column, NodeNum, TotalLine, TotalColumn);
//                     Debug.Log("创建图案 I");
//                     break;
//                 case EmShapeType.L:
//                     shape = CreateShapeOfL(line, column, NodeNum, TotalLine, TotalColumn);
//                     Debug.Log("创建图案 L");
//                     break;
//                 case EmShapeType.J:
//                     shape = CreateShapeOfJ(line, column, NodeNum, TotalLine, TotalColumn);
//                     Debug.Log("创建图案 J");
//                     break;
//                 case EmShapeType.S:
//                     shape = CreateShapeOfS(line, column, NodeNum, TotalLine, TotalColumn);
//                     Debug.Log("创建图案 S");
//                     break;
//                 case EmShapeType.Z:
//                     shape = CreateShapeOfZ(line, column, NodeNum, TotalLine, TotalColumn);
//                     Debug.Log("创建图案 Z");
//                     break;
//                 case EmShapeType.T:
//                     shape = CreateShapeOfT(line, column, NodeNum, TotalLine, TotalColumn);
//                     Debug.Log("创建图案 T");
//                     break;
//             }
//     
//             return shape;
//         }
//     
//         private Color CreateRandomColor()
//         {
//             var enums = Enum.GetValues(typeof(EmColor));
//             var typeIndex = UnityEngine.Random.Range(0, enums.Length);
//             var type = (EmColor) enums.GetValue(typeIndex);
//     
//             switch (type)
//             {
//                 case EmColor.Red:
//                     Debug.Log("创建随机颜色红色");
//                     return Color.red;
//                 case EmColor.Blue:
//                     Debug.Log("创建随机颜色蓝色");
//                     return Color.blue;
//                 case EmColor.Green:
//                     Debug.Log("创建随机颜色绿色");
//                     return Color.green;
//                 case EmColor.Yellow:
//                     Debug.Log("创建随机颜色黄色");
//                     return Color.yellow;
//                 default:
//                     Debug.Log("创建随机颜色红色");
//                     return Color.red;
//             }
//         }
//     
//         private Shape CreateShapeOfI(int line, int column, int NodeNum, int TotalLine, int TotalColumn)
//         {
//             var shape = new Shape();
//             if (line - 1 >= 0 && line + 2 < TotalLine)
//             {
//                 if (column >= 0 && column < TotalColumn)
//                 {
//                     var nodes = new Node[NodeNum];
//                     nodes[0] = SetNode(line, column, true, true, false);
//                     nodes[1] = SetNode(line - 1, column, false, true, false);
//                     nodes[2] = SetNode(line + 1, column, false, true, false);
//                     nodes[3] = SetNode(line + 2, column, false, true, false);
//                     shape.keyNode = nodes[0];
//                     shape.nodes = nodes;
//                     shape.shapeType = EmShapeType.I;
//                 }
//             }
//     
//             return shape;
//         }
//     
//         private Shape CreateShapeOfO(int line, int column, int NodeNum, int TotalLine, int TotalColumn)
//         {
//             var shape = new Shape();
//             if (line >= 0 && line + 1 < TotalLine)
//             {
//                 if (column >= 0 && column + 1 < TotalColumn)
//                 {
//                     var nodes = new Node[NodeNum];
//                     nodes[0] = SetNode(line, column, true, true, false);
//                     nodes[1] = SetNode(line, column + 1, false, true, false);
//                     nodes[2] = SetNode(line + 1, column, false, true, false);
//                     nodes[3] = SetNode(line + 1, column + 1, false, true, false);
//                     shape.keyNode = nodes[0];
//                     shape.nodes = nodes;
//                     shape.shapeType = EmShapeType.I;
//                 }
//             }
//     
//             return shape;
//         }
//     
//         private Shape CreateShapeOfJ(int line, int column, int NodeNum, int TotalLine, int TotalColumn)
//         {
//             var shape = new Shape();
//             if (line - 1 >= 0 && line < TotalLine)
//             {
//                 if (column - 1 >= 0 && column + 1 < TotalColumn)
//                 {
//                     var nodes = new Node[NodeNum];
//                     nodes[0] = SetNode(line, column, true, true, false);
//                     nodes[1] = SetNode(line, column + 1, false, true, false);
//                     nodes[2] = SetNode(line - 1, column - 1, false, true, false);
//                     nodes[3] = SetNode(line, column - 1, false, true, false);
//                     shape.keyNode = nodes[0];
//                     shape.nodes = nodes;
//                     shape.shapeType = EmShapeType.J;
//                 }
//             }
//     
//             return shape;
//         }
//     
//         private Shape CreateShapeOfL(int line, int column, int NodeNum, int TotalLine, int TotalColumn)
//         {
//             var shape = new Shape();
//             if (line - 1 >= 0 && line < TotalLine)
//             {
//                 if (column - 1 >= 0 && column + 1 < TotalColumn)
//                 {
//                     var nodes = new Node[NodeNum];
//                     nodes[0] = SetNode(line, column, true, true, false);
//                     nodes[1] = SetNode(line, column + 1, false, true, false);
//                     nodes[2] = SetNode(line - 1, column + 1, false, true, false);
//                     nodes[3] = SetNode(line, column - 1, false, true, false);
//                     shape.keyNode = nodes[0];
//                     shape.nodes = nodes;
//                     shape.shapeType = EmShapeType.L;
//                 }
//             }
//     
//             return shape;
//         }
//     
//         private Shape CreateShapeOfS(int line, int column, int NodeNum, int TotalLine, int TotalColumn)
//         {
//             var shape = new Shape();
//             if (line >= 0 && line + 1 < TotalLine)
//             {
//                 if (column - 1 >= 0 && column + 1 < TotalColumn)
//                 {
//                     var nodes = new Node[NodeNum];
//                     nodes[0] = SetNode(line, column, true, true, false);
//                     nodes[1] = SetNode(line, column + 1, false, true, false);
//                     nodes[2] = SetNode(line + 1, column, false, true, false);
//                     nodes[3] = SetNode(line + 1, column - 1, false, true, false);
//                     shape.keyNode = nodes[0];
//                     shape.nodes = nodes;
//                     shape.shapeType = EmShapeType.S;
//                 }
//             }
//     
//             return shape;
//         }
//     
//         private Shape CreateShapeOfZ(int line, int column, int NodeNum, int TotalLine, int TotalColumn)
//         {
//             var shape = new Shape();
//             if (line >= 0 && line + 1 < TotalLine)
//             {
//                 if (column - 1 >= 0 && column + 1 < TotalColumn)
//                 {
//                     var nodes = new Node[NodeNum];
//                     nodes[0] = SetNode(line, column, true, true, false);
//                     nodes[1] = SetNode(line, column - 1, false, true, false);
//                     nodes[2] = SetNode(line + 1, column, false, true, false);
//                     nodes[3] = SetNode(line + 1, column + 1, false, true, false);
//                     shape.keyNode = nodes[0];
//                     shape.nodes = nodes;
//                     shape.shapeType = EmShapeType.Z;
//                 }
//             }
//     
//             return shape;
//         }
//     
//         private Shape CreateShapeOfT(int line, int column, int NodeNum, int TotalLine, int TotalColumn)
//         {
//             var shape = new Shape();
//             if (line - 1 >= 0 && line < TotalLine)
//             {
//                 if (column - 1 >= 0 && column + 1 < TotalColumn)
//                 {
//                     var nodes = new Node[NodeNum];
//                     nodes[0] = SetNode(line, column, true, true, false);
//                     nodes[1] = SetNode(line - 1, column, false, true, false);
//                     nodes[2] = SetNode(line, column + 1, false, true, false);
//                     nodes[3] = SetNode(line, column - 1, false, true, false);
//                     shape.keyNode = nodes[0];
//                     shape.nodes = nodes;
//                     shape.shapeType = EmShapeType.T;
//                 }
//             }
//     
//             return shape;
//         }
//     
//         public Node SetNode(int line, int column, bool isKeyNode, bool isHasNode, bool isLockNode)
//         {
//             Node tempNode;
//             tempNode.line = line;
//             tempNode.column = column;
//             tempNode.isKeyNode = isKeyNode;
//             tempNode.isHasNode = isHasNode;
//             tempNode.isLockNode = isLockNode;
//             tempNode.nodeTran = GetNodeTransByLineAndColumn(line, column, );
//     
//             return tempNode;
//         }
//
//         private bool CheckNextOutOfIndex(int line, int column, int TotalLine, int TotalColumn)
//         {
//             return line >= TotalLine || column >= TotalColumn || line < 0 || column < 0;
//         }
//
//         private Transform GetNodeTransByLineAndColumn(GameObject gameManager, int line, int column, int TotalLine, int TotalColumn)
//         {
//             if (CheckNextOutOfIndex(line, column, TotalLine, TotalColumn) == false)
//             {
//                 return gameManager.transform.GetChild(line).GetChild(column);
//             }
//
//             return null;
//         }
//
//         public Node[,] CreateNodePlane(int line, int column, Color DefaultColor)
//         {
//             if (line > 0 && column > 0)
//             {
//                 var nodePlane = new Node[line, column];
//                 for (var i = 0; i < line; i++)
//                 {
//                     for (var j = 0; j < column; j++)
//                     {
//                         nodePlane[i, j] = SetNode(i, j, false, false, false);
//                         nodePlane[i, j].SetNodeColor(DefaultColor);
//                     }
//                 }
//                 Debug.Log("创建成功 NodePlane");
//                 return nodePlane;
//             }
//     
//             return null;
//         }
// }
