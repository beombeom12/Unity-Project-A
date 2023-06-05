using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class ButtonController : MonoBehaviour
{
    public static ButtonController Instance
    {
        get
        {
            if (ButtonControllerInstanxe == null)
            {
                ButtonControllerInstanxe = FindObjectOfType<ButtonController>();
                if (ButtonControllerInstanxe == null)
                {
                    var InstanceContainer = new GameObject("ButtonController");
                    ButtonControllerInstanxe = InstanceContainer.AddComponent<ButtonController>();
                }
            }
            return ButtonControllerInstanxe;
        }
    }


    private static ButtonController ButtonControllerInstanxe;

    public List<GameObject> panels; // 16개의 패널을 담을 리스트
    public List<Button> buttons; // 16개의 버튼을 담을 리스트
    public Button startButton; // 스타트 버튼

    private bool isStarted = false; // 패널이 순회 중인지 여부를 나타내는 변수
    private int selectedIndex; // 선택된 버튼의 인덱스를 저장할 변수


    //LevelUpButton
    public float fLevelUpButtonShooter; 


    //p1
    public float fHp;
    public float fMaxHp;
    //p2
    public float fPower;
    //p3
    public float Healthpack;

    //p4
    public float fShooter;

    //p6
    public float fAttackingSpeed;


    //p7
    public float fEnergyMax;
    public float fEnergy;

    //p8
    public float fDiamond;


    //p10
    public float fAllHp;
    public float fAllMaxHp;
    public float fAllPower;

    public void Awake()
    {
        if (ButtonControllerInstanxe == null)
        {
            ButtonControllerInstanxe = this;
            DontDestroyOnLoad(ButtonControllerInstanxe);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

    }


    void Start()
    {
        CheckForInformation();
        HideAllPanels(); // 모든 패널 비활성화
        startButton.onClick.AddListener(StartButtonClicked); // 스타트 버튼 클릭 이벤트 등록
    }

    void StartButtonClicked()
    {
        if (!isStarted)
        {
            StartCoroutine(StartRandomSelection()); // 랜덤 선택 시작
        }
    }

    IEnumerator StartRandomSelection()
    {
        isStarted = true;
        float delayTime = 0.1f;
        int index = 0;
        while (index < buttons.Count * 3) // 버튼을 2바퀴 돌도록 설정
        {
            SoundManager.Instance.GameUISound("Random", SoundManager.Instance.bgList[24]);
            int randomIndex = Random.Range(0, buttons.Count); // 랜덤한 버튼 선택
            buttons[randomIndex].interactable = false; // 활성화된 버튼 비활성화
            yield return new WaitForSeconds(delayTime); // 0.5초 대기
            buttons[randomIndex].interactable = true; // 선택된 버튼 활성화
            index++;
        }
        selectedIndex = Random.Range(0, buttons.Count); // 랜덤한 버튼 선택
        float blinkTime = 0f;
        while (blinkTime < 3f) // 3초간 결과 버튼 깜빡이기
        {
            buttons[selectedIndex].interactable = !buttons[selectedIndex].interactable; // 버튼 깜빡이기
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
            blinkTime += 0.2f;
        }
        buttons[selectedIndex].interactable = false; // 결과 버튼 비활성화
        yield return new WaitForSeconds(delayTime); // 0.5초 대기
        buttons[selectedIndex].interactable = true; // 결과 버튼 활성화
        yield return new WaitForSeconds(delayTime); // 0.5초 대기
        panels[selectedIndex].SetActive(true); // 선택된 패널 활성화
        isStarted = false;
    }

    void HideAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }






    public void CheckForInformation()
    {
        fLevelUpButtonShooter = 0f;
        fHp = 0f;
        fMaxHp = 0f;
        Healthpack = 0f;
        fPower = 0f;
        fShooter = 0f;
        fAttackingSpeed = 0f;
        fEnergy = 0f;
        fEnergyMax = 0f;
        fDiamond = 0f;


        fAllHp = 0f;
        fAllMaxHp = 0f;
        fAllPower = 0f;



    }

    public void LevelUpButton()
    {
        fLevelUpButtonShooter = 2000f;
        MenuData.Instance.fCurrentShooter = MenuData.Instance.fCurrentShooter - fLevelUpButtonShooter;
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
    }



    public void PanelPlusHp()
    {
        fHp = 100f;
        fMaxHp = 100f;
        panels[0].SetActive(false);
        PlayerSkillData.Instance.fPlayerCurrentHp += fHp;
        PlayerSkillData.Instance.fPlayerMaxHp += fMaxHp;
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
    }


    public void PanelPlusDamage()
    {
        fPower = 20f;
        panels[1].SetActive(false);

        PlayerSkillData.Instance.fDamage += fPower;
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);

    }
    public void PanelPlusHealthPack()
    {
        Healthpack = 50f;
        PlayerSkillData.Instance.fHealthPack = PlayerSkillData.Instance.fHealthPack + Healthpack;
        panels[2].SetActive(false);
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);

    }

    public void PanelPlusDamageMinus()
    {
        panels[3].SetActive(false);
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
    }

    public void PanelPlusShooter()
    {
        fShooter = 2000f;
        MenuData.Instance.fCurrentShooter = MenuData.Instance.fCurrentShooter + fShooter;
        panels[4].SetActive(false);
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
    }

    public void PanelPlusAgility()
    {
        panels[5].SetActive(false);
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
    }

    public void PanelPlusAttackingSpeed()
    {
        fAttackingSpeed = 1f;
        panels[6].SetActive(false);
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
    }

    public void PanelPlusEnergy()
    {
        fEnergy = 5f;
        fEnergyMax = 5f;

        panels[7].SetActive(false);
        MenuData.Instance.fCurrentEnergy = MenuData.Instance.fCurrentEnergy + fEnergy;
        MenuData.Instance.fMax = MenuData.Instance.fMax + fEnergyMax;
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
    }

    public void PanelPlusDiamond()
    {
        fDiamond = 1000f;

        panels[8].SetActive(false);

        MenuData.Instance.fDiamonCurrent = MenuData.Instance.fDiamonCurrent + fDiamond;
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
    }

    public void PanelPlusKey()
    {
        panels[9].SetActive(false);
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
    }

    public void PanelPlusAll()
    {
        fAllHp = 100f;
        fAllMaxHp = 100f;
        fAllPower = 20f;
        panels[10].SetActive(false);
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
        PlayerSkillData.Instance.fPlayerCurrentHp += fAllHp;
        PlayerSkillData.Instance.fPlayerMaxHp += fAllMaxHp;
        PlayerSkillData.Instance.fDamage += fAllPower;
    }
    


    public void PanelPlusEnd()
    {
        fEnergy = 10f;
        fShooter = 10000f;
        fDiamond = 10000f;
        panels[11].SetActive(false);
        SoundManager.Instance.PlayerSound("RandomLevelUp", SoundManager.Instance.bgList[23]);
        MenuData.Instance.fCurrentEnergy = MenuData.Instance.fCurrentEnergy + fEnergy;
        MenuData.Instance.fCurrentShooter = MenuData.Instance.fCurrentShooter + fShooter;
        MenuData.Instance.fDiamonCurrent = MenuData.Instance.fDiamonCurrent + fDiamond;

    }




}


