using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
     public float timeInterval = 1f;
     private float time = 0f;
     private bool initParam = false;
     private bool initPlane = false;
     private bool pause = false;
     private void Update()
     {
          if (Init() == false)
          {
               return;
          }

          if (Input.GetKeyDown(KeyCode.Space))
          {
               pause = !pause;
          }

          if (pause == true)
          {
               return;
          }

          if (Input.GetKeyDown(KeyCode.D))
          {
               if (GameData.isLanded == false && GameData.LockShape != null){
                    GameCaculater.Move(new Vector2Int(0,1));
               }
          }

          if (time > 0)
          {
               time -= Time.deltaTime;
          }
          else
          {
               Excute();
               time = timeInterval;
          }
     }

     private void Excute()
     {
          if (GameData.isLanded == false && GameData.LockShape != null)
          {
               AddColor(Color.white);
               GameCaculater.Move(new Vector2Int(1,0));
               AddColor(Color.red);  
          }
          else
          {
               GameData.LockShape = GameData.InitShape(2,GameData.TotalColumn / 2,4, EmShapeType.Z);
               GameCaculater.ResetState();
          }
     }

     private void AddColor(Color color)
     {
          foreach (var node in GameData.LockShape.nodes)
          {
               if (node.nodeTran == null || node.nodeTran.GetComponent<Image>() == null)
               {
                    return;
               }
               node.nodeTran.GetComponent<Image>().color = color;
          }
     }

     private bool InitParam()
     {
          GameData.parentTran = transform;
          return true;
     }

     private bool InitPlane()
     {
          if (initPlane == true)
          {
               Debug.LogError("Don't need init plane because plane has already inited");
               return true;
          }
          if (initParam == false)
          {
               Debug.LogError("wrong with init plane because param is init error");
               return false;
          }
          GameData.nodePlane = GameData.InitNodePlane(GameData.TotalLine, GameData.TotalColumn);
          return true;
     }

     private bool Init()
     {
          if (initParam == true && initPlane == true)
          {
               return true;
          }

          if (initParam == false)
          {
               initParam = InitParam(); // 初始化参数
          }

          if (initPlane == false)
          {
               initPlane = InitPlane(); // 初始化表盘
          }
          
          return false;
     }
}
