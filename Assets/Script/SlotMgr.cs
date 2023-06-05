using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotMgr : MonoBehaviour
{


    public static SlotMgr Instance
    {
        get
        {
            if (SlotInstance == null)
            {
                SlotInstance = FindObjectOfType<SlotMgr>();
                if (SlotInstance == null)
                {
                    var InstanceContainer = new GameObject("SlotMgr");
                    SlotInstance = InstanceContainer.AddComponent<SlotMgr>();
                }
            }
            return SlotInstance;
        }
    }


    public AudioSource SlotAudioSource;

    public AudioClip SlotingClip;
    public AudioClip SlotEndClip;
    public AudioClip SelectButtonClip;

    public GameObject[] SlotPlayerSkill;
    public GameObject SlotPenal;
    public GameObject SlotLogo;
    public GameObject SlotUpImage;


    public Button[] SlotButton;

    public Sprite[] SlotSkillSprite;

    [System.Serializable]
    public class ItemSlot
    {
        public List<Image> SlotSprite = new List<Image>();
    }

    public ItemSlot[] ItemSlots;

    public Image ResultItemImage;

    public List<int> StartList = new List<int>();
    public List<int> ResultIndexList = new List<int>();

    int ItemCount = 3;

    int[] answer = { 2, 3, 1 };

    public bool bIsPause = false;

    private static SlotMgr SlotInstance;
    private Vector3[] SlotSkillObjectOriginalPositions; // SlotSkillObject의 초기 위치를 저장할 배열


    private void Start()
    {
        SlotAudioSource = GetComponent<AudioSource>();

        SlotAudioSource.clip = SlotingClip;


        SlotSkillObjectOriginalPositions = new Vector3[SlotPlayerSkill.Length];
        for (int i = 0; i < SlotPlayerSkill.Length; i++)
        {
            SlotSkillObjectOriginalPositions[i] = SlotPlayerSkill[i].transform.localPosition;
        }

    }

    private void OnEnable()
    {

        for (int i = 0; i < SlotButton.Length; i++)
        {
            for (int j = 0; j < ItemCount; j++)
            {
                ItemSlots[i].SlotSprite[j].sprite = null;
            }
        }

        for (int i = 0; i < ItemCount * SlotButton.Length; i++)
        {
            StartList.Add(i);
        }

        for (int i = 0; i < SlotButton.Length; i++)
        {
            for (int j = 0; j < ItemCount; j++)
            {
                SlotButton[i].interactable = false;

                int randomIndex = Random.Range(0, StartList.Count);

                if (i == 0 && j == 1 || i == 1 && j == 0 || i == 2 && j == 2)
                {
                    ResultIndexList.Add(StartList[randomIndex]);
                }
                ItemSlots[i].SlotSprite[j].sprite = SlotSkillSprite[StartList[randomIndex]];


                if (j == 0)
                {
                    ItemSlots[i].SlotSprite[ItemCount].sprite = SlotSkillSprite[StartList[randomIndex]];

                }
                StartList.RemoveAt(randomIndex);
            }
        }
        for (int i = 0; i < SlotButton.Length; i++)
        {
            StartCoroutine(GoSlot(i, 1));
            SlotAudioSource.pitch = 3.7f;
            SlotAudioSource.loop = true;

        }

    }

    IEnumerator GoSlot(int SlotIndex, int answerOffset)
    {



      

        yield return new WaitForSeconds(0.5f);
        if (bIsPause)
        {
            Time.timeScale = 0f;
        }

        var PlayerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = PlayerObj.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = bIsPause;
        }

        // SlotSkillObject를 초기 위치로 되돌림
        SlotPlayerSkill[SlotIndex].transform.localPosition = SlotSkillObjectOriginalPositions[SlotIndex];
        for (int i = 0; i < (ItemCount * (6 + SlotIndex * 4) + answerOffset) * 2; i++)
        {
            SlotPlayerSkill[SlotIndex].transform.localPosition -= new Vector3(0f, 50f, 0f);
            if (SlotPlayerSkill[SlotIndex].transform.localPosition.y < 50f)
            {
                SlotPlayerSkill[SlotIndex].transform.localPosition += new Vector3(0f, 300f, 0f);
            }
            yield return new WaitForSeconds(0.02f);
        }

        SlotButton[SlotIndex].interactable = true;

        if (SlotButton[2].interactable == true)
        {
            SlotAudioSource.loop = false;
            SoundManager.Instance.ButtonSound("SlotEnd", SlotEndClip);
        }
    }


    //슬롯 첫번쨰 버튼 클릭시 이벤트 발생
    public void ClickButton(int iIndex)
    {
        SoundManager.Instance.GameUISound("GameSlotSound", SoundManager.Instance.bgList[23]);
        if (SlotButton[0])
        {
            if (ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[3]
              || ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[8]
              || ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[9]
              || ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[10]
              || ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[11]
              )

            {
                PlayerTargeting.Instance.fAttackingSpeed += 1f;
            }
            else if (ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[6])
            {
                PlayerSkillData.Instance.fDamage += PlayerSkillData.Instance.fDamage * 0.15f;
            }
            else if (ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[1])
            {
                PlayerSkillData.Instance.PlayerSkill[4] = PlayerSkillData.Instance.PlayerSkill[4] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[4] = PlayerSkillData.Instance.BowPlayerSkill[4] + 1;
            }
            else if (ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[4] || ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[7])
            {
                PlayerSkillData.Instance.PlayerSkill[3] = PlayerSkillData.Instance.PlayerSkill[3] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[3] = PlayerSkillData.Instance.BowPlayerSkill[3] + 1;
            }
            else if (ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[0] )
            {
                PlayerSkillData.Instance.PlayerSkill[2] = PlayerSkillData.Instance.PlayerSkill[2] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[2] = PlayerSkillData.Instance.BowPlayerSkill[2] + 1;
            }
            else if (ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[2] )
            {
                PlayerSkillData.Instance.PlayerSkill[1] = PlayerSkillData.Instance.PlayerSkill[1] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[1] = PlayerSkillData.Instance.BowPlayerSkill[1] + 1;
            }
            else if (ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[5])
            {
                PlayerSkillData.Instance.PlayerSkill[0] = PlayerSkillData.Instance.PlayerSkill[0] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[0] = PlayerSkillData.Instance.BowPlayerSkill[0] + 1;
            }
        }
        UIGameController.Instance.PlayerLevelUp(false);
        if (bIsPause)
        {
            Time.timeScale = 1f;
        }

        var PlayerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = PlayerObj.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = !bIsPause;
        }
    }


    //슬롯버튼 두번째 클릭시 이벤트 발생

    public void ClickTwoButton()
    {

        SoundManager.Instance.GameUISound("GameSlotSound", SoundManager.Instance.bgList[23]);
        if (SlotButton[1])
        {
            if (ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[3]
              || ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[8]
              || ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[9]
              || ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[10]
              || ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[11]
              )

            {
                PlayerTargeting.Instance.fAttackingSpeed += 1f;
            }
            else if (ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[6])
            {
                PlayerSkillData.Instance.fDamage += PlayerSkillData.Instance.fDamage * 0.15f;
            }
            else if (ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[1])
            {
                PlayerSkillData.Instance.PlayerSkill[4] = PlayerSkillData.Instance.PlayerSkill[4] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[4] = PlayerSkillData.Instance.BowPlayerSkill[4] + 1;
            }
            else if (ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[4] || ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[7])
            {
                PlayerSkillData.Instance.PlayerSkill[3] = PlayerSkillData.Instance.PlayerSkill[3] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[3] = PlayerSkillData.Instance.BowPlayerSkill[3] + 1;
            }
            else if (ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[0])
            {
                PlayerSkillData.Instance.PlayerSkill[2] = PlayerSkillData.Instance.PlayerSkill[2] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[2] = PlayerSkillData.Instance.BowPlayerSkill[2] + 1;
            }
            else if (ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[2])
            {
                PlayerSkillData.Instance.PlayerSkill[1] = PlayerSkillData.Instance.PlayerSkill[1] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[1] = PlayerSkillData.Instance.BowPlayerSkill[1] + 1;
            }
            else if (ItemSlots[1].SlotSprite[2].sprite == SlotSkillSprite[5])
            {
                PlayerSkillData.Instance.PlayerSkill[0] = PlayerSkillData.Instance.PlayerSkill[0] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[0] = PlayerSkillData.Instance.BowPlayerSkill[0] + 1;
            }
        }
        UIGameController.Instance.PlayerLevelUp(false);
        if (bIsPause)
        {
            Time.timeScale = 1f;
        }

        var PlayerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = PlayerObj.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = !bIsPause;
        }
    }






    //슬롯버튼 세번쨰 버튼 이벤트 발생
    public void ClickThreeButton()
    {
        SoundManager.Instance.GameUISound("GameSlotSound", SoundManager.Instance.bgList[23]);
        if (SlotButton[2])
        {
            if (ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[3]
               || ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[8]
               || ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[9]
               || ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[10]
               || ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[11]
               )

            {
                PlayerTargeting.Instance.fAttackingSpeed += 1f;
            }
            else if (ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[6])
            {
                PlayerSkillData.Instance.fDamage += PlayerSkillData.Instance.fDamage * 0.15f;
            }
            else if (ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[1])
            {
                PlayerSkillData.Instance.PlayerSkill[4] = PlayerSkillData.Instance.PlayerSkill[4] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[4] = PlayerSkillData.Instance.BowPlayerSkill[4] + 1;
            }
            else if (ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[4] || ItemSlots[0].SlotSprite[2].sprite == SlotSkillSprite[7])
            {
                PlayerSkillData.Instance.PlayerSkill[3] = PlayerSkillData.Instance.PlayerSkill[3] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[3] = PlayerSkillData.Instance.BowPlayerSkill[3] + 1;
            }
            else if (ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[0])
            {
                PlayerSkillData.Instance.PlayerSkill[2] = PlayerSkillData.Instance.PlayerSkill[2] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[2] = PlayerSkillData.Instance.BowPlayerSkill[2] + 1;
            }
            else if (ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[2])
            {
                PlayerSkillData.Instance.PlayerSkill[1] = PlayerSkillData.Instance.PlayerSkill[1] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[1] = PlayerSkillData.Instance.BowPlayerSkill[1] + 1;
            }
            else if (ItemSlots[2].SlotSprite[2].sprite == SlotSkillSprite[5])
            {
                PlayerSkillData.Instance.PlayerSkill[0] = PlayerSkillData.Instance.PlayerSkill[0] + 1;
                PlayerSkillData.Instance.BowPlayerSkill[0] = PlayerSkillData.Instance.BowPlayerSkill[0] + 1;
            }
        }
        UIGameController.Instance.PlayerLevelUp(false);
        if (bIsPause)
        {
            Time.timeScale = 1f;
        }

        var PlayerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = PlayerObj.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = !bIsPause;
        }
    }
}
