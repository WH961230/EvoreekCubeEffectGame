using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
     public float AutoTimeInterval = 1f;
     public float MoveDownTimeInterval = 0.05f;
     public float MoveHorizontalInterval = 0.1f;
     private float deployTimer = 0f;
     private float inputTimer = 0f;
     private bool initParam = false;
     private bool initPlane = false;
     private bool pause = false;
     public Text ScoreText;
     public Transform GameOverPlane;
     private int score;

     private void Start()
     {
          GameOverPlane.GetComponentInChildren<Button>().onClick.AddListener(RestartGame);
     }

     private void Update()
     {
          Debug.Log(GameData.emAct);
          if (GameData.isGameOver == true)
          {
               return;
          }

          if (Init() == false)
          {
               return;
          }

          if (Input.GetKeyDown(KeyCode.P))
          {
               pause = !pause;
          }

          if (pause == true)
          {
               return;
          }

          RewardListener();
          ActListener();
          InternalTimeEvent();
     }

     private void RewardListener()
     {
          if (GameData.Score != score)
          {
               score = GameData.Score;
               ScoreText.text = score.ToString();
          }
     }

     private void RemoveListener()
     {
          GameCaculater.Remove();
          Debug.Log("消除检测");
     }

     private void GameOverListener()
     {
          GameData.isGameOver = GameCaculater.GameOver();
          if (GameData.isGameOver == true)
          {
               GameOverPlane.gameObject.SetActive(true);
               GameOverPlane.GetComponentInChildren<Text>().text = "GameOver";
          }
     }

     private void ActListener()
     {
          if (GameData.isLanded == false && GameData.LockShape != null)
          {
               if (Input.GetKey(KeyCode.D))
               {
                    GameData.emAct = EmAct.MoveRight;
               }
               else if (Input.GetKey(KeyCode.A))
               {
                    GameData.emAct = EmAct.MoveLeft;
               }
               else if (Input.GetKey(KeyCode.S))
               {
                    GameData.emAct = EmAct.MoveDown;
               } 
               else if (Input.GetKeyDown(KeyCode.Space))
               {
                    GameData.emAct = EmAct.Rotate;
               }

               if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Space))
               {
                    GameData.emAct = EmAct.Default;
                    GameData.isAutoMove = true;
               }

               switch (GameData.emAct)
               {
                    case EmAct.MoveLeft:
                         PlayMove(new Vector2Int(0, -1),MoveHorizontalInterval);
                         break;
                    case EmAct.MoveRight:
                         PlayMove(new Vector2Int(0, 1),MoveHorizontalInterval);
                         break;
                    case EmAct.MoveDown:
                         GameData.isAutoMove = false;
                         PlayMove(new Vector2Int(1, 0),MoveDownTimeInterval);
                         break;
                    case EmAct.Rotate:
                         PlayRotate();
                         GameData.emAct = EmAct.Default;
                         break;
                    default:
                         break;
               }
          }
     }

     private void PlayMove(Vector2Int offset, float interval)
     {
          if (inputTimer > 0)
          {
               inputTimer -= Time.deltaTime;
          }
          else
          {
               ColorLockShape(Color.white);
               GameCaculater.Move(offset);
               ColorLockShape(Color.red);
               inputTimer = interval;
          }
     }

     private void PlayRotate()
     {
          GameCaculater.Rotate();
     }

     private void InternalTimeEvent()
     {
          if (deployTimer > 0)
          {
               deployTimer -= Time.deltaTime;
          }
          else
          {
               if (GameData.isLanded == false && GameData.LockShape != null)
               {
                    if (GameData.isAutoMove == false)
                    {
                         deployTimer = 0f;
                         return;
                    }
                    ColorLockShape(Color.white);
                    GameCaculater.Move(new Vector2Int(1,0));
                    ColorLockShape(Color.red);  
               }
               else
               {

                    if (GameData.isLanded == true)
                    {
                         RemoveListener();
                         GameOverListener();
                    }
                    GameData.LockShape = GameData.InitShape(2,GameData.TotalColumn / 2,4, EmShapeType.Z);

                    GameCaculater.ResetState();
               }
               deployTimer = AutoTimeInterval;
          }
     }

     private void RestartGame()
     {
          GameOverPlane.gameObject.SetActive(false);
          //清理表盘
          ColorNodePlane(Color.white);
          GameData.nodePlane = null;
          GameData.LockShape = null;
          initParam = false;
          initPlane = false;
          GameData.isGameOver = false;
     }

     private void ColorLockShape(Color color)
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

     private void ColorNodePlane(Color color)
     {
          for (var i = 0; i < GameData.RowCount; i++)
          {
               for (var j = 0; j < GameData.ColumnCount; j++)
               {
                    GameData.nodePlane[i, j].nodeTran.GetComponent<Image>().color = color;
               }
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

          score = GameData.Score;
          ScoreText.text = score.ToString();
          return false;
     }
}
