using System;
using UnityEngine;
using UnityEngine.Serialization;
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
     public Transform GameStartPlane;
     public Button GameRestartBtn; 
     public Button GameBackMenuBtn;
     public Button GameStartBtn;
     public Button GameQuitBtn;
     private int score;

     private void Start()
     {
          GameData.Init();
          GameRestartBtn.onClick.AddListener(RestartGame);
          GameStartBtn.onClick.AddListener(RestartGame);
          GameBackMenuBtn.onClick.AddListener(BackMenuGame);
          GameQuitBtn.onClick.AddListener(QuitGame);
          GameData.isCreateShape = true;
     }

     private void Update()
     {
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
          CreateListener();
     }

     private void CreateListener()
     {
          if (GameData.isCreateShape == false)
          {
               return;
          }
          var type = Enum.GetValues(typeof(EmShapeType));
          var temp = UnityEngine.Random.Range(0, type.Length);
          var typeRet = (EmShapeType)type.GetValue(temp);
          GameData.LockShape = GameData.InitShape(2,GameData.TotalColumn / 2,4, typeRet);
          GameCaculater.ResetState();
          GameData.isCreateShape = false;
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

               if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) ||
                   Input.GetKeyUp(KeyCode.Space))
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
               if (GameData.isLanded == true)
               {
                    AfterLandDo();
               }
          }
     }

     private void PlayRotate()
     {
          ColorLockShape(Color.white);
          GameCaculater.Rotate();
          ColorLockShape(Color.red);
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
                    if (GameData.isLanded == true)
                    {
                         AfterLandDo();
                    }
               }
               deployTimer = AutoTimeInterval;
          }
     }

     private void AfterLandDo()
     {
          RemoveListener();
          GameOverListener();
          GameData.isCreateShape = true;
     }

     private void RestartGame()
     {
          GameStartPlane.gameObject.SetActive(false);
          GameOverPlane.gameObject.SetActive(false);
          //清理表盘
          ColorNodePlane(Color.white);
          GameData.nodePlane = null;
          GameData.LockShape = null;
          GameData.Score = 0;
          initParam = false;
          initPlane = false;
          GameData.isGameOver = false;
          GameData.isCreateShape = true;
          GameData.isAutoMove = true;
     }

     private void BackMenuGame()
     {
          GameOverPlane.gameObject.SetActive(false);
          GameStartPlane.gameObject.SetActive(true);
          ColorNodePlane(Color.white);
          GameData.LockShape = null;
          GameData.Score = 0;
          GameData.isGameOver = true;
          GameData.isAutoMove = false;
          GameData.isCreateShape = false;
     }

     public void QuitGame()
     {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif     
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
          GameData.LoadNodePlane(this.transform,GameData.TotalLine,GameData.TotalColumn);
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
