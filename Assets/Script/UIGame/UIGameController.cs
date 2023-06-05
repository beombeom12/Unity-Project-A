using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIGameController : MonoBehaviour
{

    public static UIGameController Instance
    {
        get
        {
            if (UIGameInstance == null)
            {
                UIGameInstance = FindObjectOfType<UIGameController>();
                if (UIGameInstance == null)
                {
                    var InstanceContainer = new GameObject("UIGameController");
                    UIGameInstance = InstanceContainer.AddComponent<UIGameController>();
                }
            }
            return UIGameInstance;
        }
    }

    private static UIGameController UIGameInstance;



    public GameObject JoyStick;
    public GameObject JoyStickPanel;
    public GameObject SlotMachine;
    public GameObject RouletteMachine;
    public GameObject EndGameObejct;
    public GameObject RealEndGamePanel;



    //GameEnd SetPanel for  Camera False  
    public GameObject GmaeMainCamera;
    public GameObject GameEnvir;
    public GameObject GamePlayer;
    public GameObject Pet1;
    public GameObject Pet2;
    public GameObject OpenDoor;
    public GameObject OpenDoorTexting;
    public GameObject GameCanvase;



    //Text

    public Text ClearRoomConuntText;
    public Text EndGameClearRoomCountTextandText;
    public Text PlayerLevelText;

    public Text MenuLeveltext;

    public Slider PlayerExpBar;


    public Text EndGameMenuLevelText;


    public Text EndGameClearRoomCountText;
    //Angel

    public bool bAngel1Right = false;
    public bool bAngel2Right = false;
    public bool bAngel3Right = false;


    public GameObject Angel1;
    public GameObject Angel2;
    public GameObject Angel3;




    ////Boss

    public Slider BossHpBar;
    public Slider BossHpBackgroundBar;
    public Slider MenuLevelSlider;
    public Slider EndGameLvelSlider;
    public bool bBackHpHit = false;
    public bool bBossRoom = false;
    public bool bIsPause = false;
    public bool bClickDown;
    
    public float fBossCurrentHp;
    public float fBossMaxHp;


    public float fLevelUp; 


    // Start is called before the first frame update
    void Start()
    {
        PlayerExpBar.value = PlayerSkillData.Instance.PlayerCurrentExp / PlayerSkillData.Instance.PlayerLvUpExp;
        MenuLevelSlider.value = MenuData.Instance.fMPlayerCurrentExp / MenuData.Instance.fMPlayerLvUpExp;

        PlayerExpBar.gameObject.SetActive(true);
        BossHpBar.gameObject.SetActive(false);
        BossHpBackgroundBar.gameObject.SetActive(false);




        fLevelUp = 70f;
    }

    // Update is called once per frame
    void Update()
    {

        if (!bBossRoom)
        {
            PlayerExpBar.value = Mathf.Lerp(PlayerExpBar.value, PlayerSkillData.Instance.PlayerCurrentExp / PlayerSkillData.Instance.PlayerLvUpExp, 0.75f);
            PlayerLevelText.text = "Lv." + PlayerSkillData.Instance.PlayerLevel;

        }
        else
        {
            BossHpBar.value = Mathf.Lerp(BossHpBar.value, fBossCurrentHp / fBossMaxHp, Time.deltaTime * 5f);
      
            if(bBackHpHit)
            {
                BossHpBackgroundBar.value = Mathf.Lerp(BossHpBackgroundBar.value, BossHpBar.value, Time.deltaTime * 10f);
                if(BossHpBar.value >= BossHpBackgroundBar.value - 0.01f)
                {
                    bBackHpHit = false;
                    BossHpBackgroundBar.value = BossHpBar.value;
                }
            }
        
        }
        EndGameMenuLevelText.text = "" + MenuData.Instance.iMenuPlayerLevel;
        EndGameLvelSlider.value = (Mathf.Lerp(EndGameLvelSlider.value, MenuData.Instance.fMPlayerCurrentExp / MenuData.Instance.fMPlayerLvUpExp, 0.78f));

        //�г������� ����ġ ������ 
        MenuLevelSlider.value = (Mathf.Lerp(MenuLevelSlider.value, MenuData.Instance.fMPlayerCurrentExp / MenuData.Instance.fMPlayerLvUpExp, 0.78f));
        MenuLeveltext.text = "" + MenuData.Instance.iMenuPlayerLevel;
    }

    public void CheckBossRoom(bool isBossRoom)
    {
        bBossRoom = isBossRoom;

        if(isBossRoom)
        {
            PlayerExpBar.gameObject.SetActive(false);
            BossHpBar.gameObject.SetActive(true);
            BossHpBackgroundBar.gameObject.SetActive(true);

        }
        else
        {
            PlayerExpBar.gameObject.SetActive(true);
            BossHpBar.gameObject.SetActive(false);
            BossHpBackgroundBar.gameObject.SetActive(false);
        }
    }



    public void CheckAngel1(bool isAngel1Rights)
    {
        bAngel1Right = isAngel1Rights;
        bAngel2Right = false;
        bAngel3Right = false;

        Angel1.SetActive(false);
        Destroy(Angel1.gameObject);

    }


    public void Damage(float fDamage)
    {
        fBossCurrentHp -= fDamage;
        Invoke("BossBackBg", 0.5f);

    }
    

    void BossBackBg()
    {
       
        bBackHpHit = true;
    }
    public void PlayerLevelUp(bool IsSlotMachine)
    {
        if (IsSlotMachine)
        {

            //bIsPause = !bIsPause;
            //if (!bIsPause)
            //{
            //    Time.timeScale = 0f;
            //}

            //var PlayerObj = GameObject.FindGameObjectWithTag("Player");
            //var scripts = PlayerObj.GetComponents<MonoBehaviour>();
            //foreach (var script in scripts)
            //{
            //    script.enabled = !bIsPause;
            //}

            JoyStick.SetActive(false);
            JoyStickPanel.SetActive(false);
            SlotMachine.SetActive(true);





        }
        else
        {
            JoyStick.SetActive(true);
            JoyStickPanel.SetActive(true);
            SlotMachine.SetActive(false);
        

        }
    }

    //�׾����� �ߴ� �˾�â
    public void StageAllClear()
    {
        JoyStick.gameObject.SetActive(false);
        JoyStickPanel.gameObject.SetActive(false);
        //Test ĳ���� ���߰� �����.
       


        StartCoroutine(StageClear());
    }
    IEnumerator StageClear()
    {
        yield return new WaitForSeconds(0f);
        EndGameObejct.SetActive(true);
        EndGameClearRoomCountTextandText.text = "" + (SceneMgr.Instance.CurrentStage);

    }

    public void EndGame()
    {
        // ��� �ִϸ��̼� ���
        PlayerMove.Instance.anim.SetBool("Dead", true);

        PlayerMove.Instance.isMove = false;
        PlayerMove.Instance.anim.SetBool("Idle", false);
        PlayerMove.Instance.anim.SetBool("Run", false);
        PlayerMove.Instance.anim.SetBool("Attack", false);


        SoundManager.Instance.PlayerSound("PlayerDeadSound", SoundManager.Instance.bgList[6]);

        // 3�� �Ŀ� �г� Ȱ��ȭ
        StartCoroutine(ActivatePanel(0.3f));
    }

    private IEnumerator ActivatePanel(float waitTime)
    {
        // 3�� ���� ��ٸ��鼭 �ð��� ����ϴ�.
        yield return new WaitForSecondsRealtime(waitTime);

        EndGameObejct.SetActive(true);
        ClearRoomConuntText.text = "" + (SceneMgr.Instance.CurrentStage);

        Time.timeScale = 0f;
        var goGodButton = EndGameObejct.GetComponentInChildren<Button>();
        goGodButton.onClick.RemoveAllListeners();
        goGodButton.onClick.AddListener(GoGod);
    }

    //EndGamePopUp Ȩ���� ���� ��ư !
    //���⼭ ���� ����ġ + ���� �߰��ϱ� 
    public void GotoIntroMenu()
    {

        JoyStick.SetActive(false);
        JoyStickPanel.SetActive(false);
        EndGameObejct.SetActive(false);
        SceneManager.LoadScene("IntroMenu");
    }




    //�̰��� ���� ��¥ ���������� �������� ����ɰ��̴�.
    public void EndGameAllClear()
    {
        JoyStick.gameObject.SetActive(false);
        JoyStick.gameObject.SetActive(false);

  

        StartCoroutine(RealStageClear());
    }



    IEnumerator RealStageClear()
    {
        yield return new WaitForSeconds(0f);

        RealEndGamePanel.SetActive(true);
        EndGameClearRoomCountTextandText.text = "" + (SceneMgr.Instance.LastStage); 
    }

    public void RealGameEndGotoIntroMenu()
    {
        JoyStick.SetActive(false);
        JoyStickPanel.SetActive(false);
        RealEndGamePanel.SetActive(false);
        MenuData.Instance.MenuPlayerExp(130f);

        GameCanvase.SetActive(false);
        GameEnvir.SetActive(false);
        GamePlayer.SetActive(false);
        Pet1.SetActive(false);
        Pet2.SetActive(false);
        OpenDoor.SetActive(false);
        IntroScript.Instance.MenuCanvas.SetActive(true);
        IntroScript.Instance.IntroMenuCamera.SetActive(true);
        SceneManager.LoadScene("IntroMenu");



    }






    //��Ȱ
    //�׾����� �������� �г��� �߰� ��� �ִϸ��̼��� �۵��ǰ� �������Ѵ�. ��� -> �ڷ�ƾ
    public void GoGod()
    {
        // �ð� �������� 1�� �ٲٰ�, �г��� ��Ȱ��ȭ
        Time.timeScale = 1f;
        SoundManager.Instance.ButtonSound("Restart", SoundManager.Instance.bgList[7]);
        EndGameObejct.SetActive(false);
        if (PlayerSkillData.Instance.fPlayerCurrentHp <= 0)
        {
            PlayerSkillData.Instance.fPlayerCurrentHp += PlayerSkillData.Instance.fPlayerMaxHp;
            PlayerHpBar.Instance.m_fCurrentHp += PlayerHpBar.Instance.m_fMaxHp;
            PlayerSkillData.Instance.fPlayerMaxHp = PlayerSkillData.Instance.fPlayerMaxHp;

            PlayerMove.Instance.anim.SetBool("Dead", false);
            PlayerMove.Instance.isMove = true;
            PlayerMove.Instance.anim.SetBool("Idle",true);
            PlayerMove.Instance.anim.SetBool("Run", false);
            PlayerMove.Instance.anim.SetBool("Attack", false);
        }
    }

}
