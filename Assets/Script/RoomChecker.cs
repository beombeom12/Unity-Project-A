using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    public static RoomChecker Instance
    {
        get
        {
            if (RoomCheckerInstance == null)
            {
                RoomCheckerInstance = FindObjectOfType<RoomChecker>();
                if (RoomCheckerInstance == null)
                {
                    var InstanceContainer = new GameObject("RoomChecker");
                    RoomCheckerInstance = InstanceContainer.AddComponent<RoomChecker>();
                }
            }
            return RoomCheckerInstance;
        }
    }
    private static RoomChecker RoomCheckerInstance;



    List<GameObject> RoomMonster = new List<GameObject>();

    public bool bRoomEnterPlayer = false;
    public bool bMissionClear = false;

    GameObject NextGate;


    // Start is called before the first frame update
    void Start()
    {
        NextGate = transform.GetChild(0).gameObject;

      
    }

    // Update is called once per frame
    void Update()
    {

        if(bRoomEnterPlayer)
        {
            if(PlayerTargeting.Instance.MonsterList.Count <= 0 && !bMissionClear)
            {
                bMissionClear = true;
                Debug.Log("Clear");
         
                StartCoroutine(OpenDoor());
   
            }

       
        }

        
    }

    IEnumerator OpenDoor()
    {
        yield return null;

        yield return new WaitForSeconds(1.5f);
        if (PlayerTargeting.Instance.MonsterList.Count <= 0)
        {
            SceneMgr.Instance.CloseDoor.SetActive(false);
            SceneMgr.Instance.OpenDoor.SetActive(true);
            Instantiate(EffectScript.Instance.OpenDoorEffect, SceneMgr.Instance.transform.position, Quaternion.identity);
            SoundManager.Instance.GameUISound("DoorOpen", SoundManager.Instance.bgList[10]);
        }
        else
        {
            bMissionClear = false;
        }


    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            bRoomEnterPlayer = true;
            //플레이어타켓팅
            PlayerTargeting.Instance.MonsterList = new List<GameObject>(RoomMonster);
            //펫 타겟팅
            PetTargeting.Instance.MonsterList = new List<GameObject>(RoomMonster);

            BatPetTargeting.Instance.MonsterList = new List<GameObject>(RoomMonster);
            Debug.Log("Enter the room : " + PlayerTargeting.Instance.MonsterList.Count);

            SceneMgr.Instance.CloseDoor.transform.position = NextGate.transform.position + new Vector3(-1.1f, 0.55f, 18.96f);
            SceneMgr.Instance.OpenDoor.transform.position = NextGate.transform.position + new Vector3(-1.119998f, -0.4100001f, 17.96f);
            //DoorText
            SceneMgr.Instance.StageText.transform.position = NextGate.transform.position + new Vector3(-1.05f, 1.9f, 20.3495f);
            //z 145.4 

            SceneMgr.Instance.CloseDoor.SetActive(true);
            SceneMgr.Instance.OpenDoor.SetActive(false);
        }

        if (other.CompareTag("Monster"))
        {
            RoomMonster.Add(other.gameObject);
            Debug.Log("Mob name " + other.gameObject.name);
        }
    }


        private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bRoomEnterPlayer = false;
            PetTargeting.Instance.MonsterList.Clear();
            BatPetTargeting.Instance.MonsterList.Clear();
            PlayerTargeting.Instance.MonsterList.Clear();
            Debug.Log("Player out");

        }

        if (other.CompareTag("Monster"))
        {
            PetTargeting.Instance.MonsterList.Remove(other.gameObject);
            BatPetTargeting.Instance.MonsterList.Remove(other.gameObject);
            PlayerTargeting.Instance.MonsterList.Remove(other.gameObject);
            RoomMonster.Remove(other.gameObject);
        }




    }

}
