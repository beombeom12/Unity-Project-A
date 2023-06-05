using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneMgr : MonoBehaviour
{

    public static SceneMgr Instance
    {
        get
        {
            if (SceneInstance == null)
            {
                SceneInstance = FindObjectOfType<SceneMgr>();
                if (SceneInstance == null)
                {
                    var InstanceContainer = new GameObject("SceneMgr");
                    SceneInstance = InstanceContainer.AddComponent<SceneMgr>();
                }
            }
            return SceneInstance;
        }
    }


    public static SceneMgr SceneInstance;


    public GameObject Player;
    public GameObject OpenDoor;
    public GameObject CloseDoor;
    public GameObject StageText;
    public Text StageTexting;

    [System.Serializable]
    public class StartPositionArray
    {
        public List<Transform> StartPosition = new List<Transform>();

    }

    public StartPositionArray[] startPositionArrays;

    public List<Transform> StartPositionAngle = new List<Transform>();
    public List<Transform> StartPositionBoss = new List<Transform>();
    public Transform StartPoisitionLastBoss;

    public int CurrentStage = 0;
    public int LastStage = 20;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");


    }

    public void NextStage()
    {
        CurrentStage++;

        if (CurrentStage > LastStage)
        {
            //진짜 게임이 끝났을떄 실행되는 EndGameAllClear가 실행 
            UIGameController.Instance.EndGameAllClear();
            return;
        }

        if (CurrentStage % 5 != 0)
        {
            int iArrayIndex = CurrentStage / 10;
            int randomIndex = Random.Range(0, startPositionArrays[iArrayIndex].StartPosition.Count);
            Player.transform.position = startPositionArrays[iArrayIndex].StartPosition[randomIndex].position;
            startPositionArrays[iArrayIndex].StartPosition.RemoveAt(randomIndex);
        }
        else
        {
            if (CurrentStage % 10 == 5)
            {
                int randomIndex = Random.Range(0, StartPositionAngle.Count);
                Player.transform.position = StartPositionAngle[randomIndex].position;
            }
            else
            {
                if (CurrentStage == LastStage)
                {
                    SoundManager.Instance.MonsterSound("LastBossSound", SoundManager.Instance.bgList[26]);
                    Player.transform.position = StartPoisitionLastBoss.position;
                    UIGameController.Instance.CheckBossRoom(true);
                }
                else
                {
                    int randomIndex = Random.Range(0, StartPositionBoss.Count);
                    Player.transform.position = StartPositionBoss[randomIndex].position;
                    StartPositionBoss.RemoveAt(CurrentStage / 10);
                }
            }
        }
        CameraScript.Instance.CameraMoveRoom();
    }

    // Update is called once per frame
    void Update()
    {

        GameStageTexting();
    }



    //DoorStageTexting
    public void GameStageTexting()
    {
        StageTexting.text = "" + CurrentStage;
    }
}
