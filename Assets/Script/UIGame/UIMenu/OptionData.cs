using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OptionData : MonoBehaviour
{
    public static OptionData Instance
    {
        get
        {
            if (OptionDataInstance == null)
            {
                OptionDataInstance = FindObjectOfType<OptionData>();
                if (OptionDataInstance == null)
                {
                    var InstanceContainer = new GameObject("OptionData");
                    OptionDataInstance = InstanceContainer.AddComponent<OptionData>();
                }
            }
            return OptionDataInstance;
        }
    }

    private static OptionData OptionDataInstance;
    
    //Option
    public GameObject OptionPanel;
    public GameObject MailPanel;
    public GameObject NoticePanel;

    //Gift
    public GameObject GiftPanel;
    public GameObject PigeonCollection;

    public GameObject NotReadyForGiftText;

    public GameObject FirstGift;
    public GameObject FirstShineCollect;

    public GameObject DiaAnim;
    public GameObject ShooterAnim;



    public GameObject SecondGift;
    public GameObject SecondShineCollect;
    public float fDiamond;
    public float fShooter;


    public AudioClip clip;
    public bool bClickIsRight;


    public bool bOptionMailRight;

    public void Awake()
    {
        if (OptionDataInstance == null)
        {
            OptionDataInstance = this;
            DontDestroyOnLoad(OptionDataInstance);
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


    public void ClickDown()
    {
        bClickIsRight = true;

        OptionPanel.SetActive(true);
        SoundManager.Instance.ButtonSound("ButtonClick", clip);


    }

    public void PanelDown()
    {


        bClickIsRight = false;
        OptionPanel.SetActive(false);
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
    }

    public void GoOptionScene()
    {
        IntroScript.Instance.PlayerInforMation();
    
        SceneManager.LoadScene("OptionScene");
        IntroScript.Instance.MenuCanvas.SetActive(false);
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
    }
    



    public void GiftClickDown()
    {
        bClickIsRight = true;
        GiftPanel.SetActive(true);
        PigeonCollection.SetActive(false);
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
    }

    public void GiftClickPanel()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
        bClickIsRight = false;
        GiftPanel.SetActive(false);
        PigeonCollection.SetActive(true);

    }


    public void GiftPanelFirstOff()
    {
        SoundManager.Instance.GameUISound("GiftPanelsound", SoundManager.Instance.bgList[23]);
        NotReadyForGiftText.SetActive(true);
        FirstShineCollect.SetActive(false);
        FirstGift.SetActive(false);
        Instantiate(DiaAnim, transform.position, transform.rotation);
      
        fDiamond = 3000f;
        MenuData.Instance.fDiamonCurrent = MenuData.Instance.fDiamonCurrent + fDiamond;
    }


    public void GiftPanelSecondOff()
    {

        SoundManager.Instance.GameUISound("GiftPanelsound", SoundManager.Instance.bgList[23]);

        SecondShineCollect.SetActive(true);
        SecondGift.SetActive(false);
        Instantiate(ShooterAnim, transform.position, transform.rotation);
        fShooter = 3000f;
        MenuData.Instance.fCurrentShooter = MenuData.Instance.fCurrentShooter + fShooter;


    }


    public void OptionMailPanelOn()
    {
        bOptionMailRight = false;
        SoundManager.Instance.GameUISound("GiftPanelsound", SoundManager.Instance.bgList[23]);
        OptionPanel.SetActive(false);
        MailPanel.SetActive(true);
        PigeonCollection.SetActive(false);
    }


    public void OptionMailPanelOff()
    {
        bOptionMailRight = true;
        SoundManager.Instance.GameUISound("GiftPanelsound", SoundManager.Instance.bgList[23]);
        MailPanel.SetActive(false);
        OptionPanel.SetActive(true);
        PigeonCollection.SetActive(true);
    }


    public void OptionNoticePanelOn()
    {
        SoundManager.Instance.GameUISound("GiftPanelsound", SoundManager.Instance.bgList[23]);
        PigeonCollection.SetActive(false);
        OptionPanel.SetActive(false);
        NoticePanel.SetActive(true);
    }

    public void OptionNoticePanelOff()
    {
        SoundManager.Instance.GameUISound("GiftPanelsound", SoundManager.Instance.bgList[23]);
        PigeonCollection.SetActive(true);
        OptionPanel.SetActive(true);
        NoticePanel.SetActive(false);
    }

}
