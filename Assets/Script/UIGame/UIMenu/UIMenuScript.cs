using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMenuScript : MonoBehaviour
{




    public static UIMenuScript Instance
    {
        get
        {
            if (UIInstance == null)
            {
                UIInstance = FindObjectOfType<UIMenuScript>();
                if (UIInstance == null)
                {
                    var InstanceContainer = new GameObject("UIMenuScript");
                    UIInstance = InstanceContainer.AddComponent<UIMenuScript>();
                }
            }
            return UIInstance;
        }
    }
    private static UIMenuScript UIInstance;
    //메뉴플레이어 레벨
    public Slider MenuPlayerExp;
    public Text MenuPlayerLv;


    //메뉴 플레이어 에너지
    public Slider MenuEnergySlider;
    public Text CurrencyMenuEnergy;

    public Slider MenuShooterSlider;
    public Text CurrentMenuShooterText;

    public Slider MenuDiamonSlider;
    public Text CurrentMenuDiamondText;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerExp
        MenuPlayerExp.value = MenuData.Instance.fMPlayerCurrentExp / MenuData.Instance.fMPlayerLvUpExp;



        //Energy
        MenuEnergySlider.value = MenuData.Instance.fCurrentEnergy / MenuData.Instance.fMax;
        
        
        
        //Shooter
        MenuShooterSlider.value = MenuData.Instance.fCurrentShooter / MenuData.Instance.fShooterMax;



        //Diamond
        MenuDiamonSlider.value = MenuData.Instance.fDiamonCurrent / MenuData.Instance.fDiamonMax;


    }

    // Update is called once per frame
    void Update()
    {
        MenuPlayerExp.value = Mathf.Lerp(MenuPlayerExp.value, MenuData.Instance.fMPlayerCurrentExp / MenuData.Instance.fMPlayerLvUpExp, 0.78f);
        MenuPlayerLv.text = "" + MenuData.Instance.iMenuPlayerLevel;


        MenuEnergySlider.value = Mathf.Lerp(MenuEnergySlider.value, MenuData.Instance.fCurrentEnergy / MenuData.Instance.fMax, 0.75f);
        CurrencyMenuEnergy.text = ("" + MenuData.Instance.fCurrentEnergy ) + "/" + ("" + MenuData.Instance.fMax);


        MenuShooterSlider.value = Mathf.Lerp(MenuShooterSlider.value, MenuData.Instance.fCurrentShooter / MenuData.Instance.fShooterMax, 0.75f);
        CurrentMenuShooterText.text = "" + (MenuData.Instance.fCurrentShooter + MenuData.Instance.fShooterUI);


        MenuDiamonSlider.value = Mathf.Lerp(MenuDiamonSlider.value, MenuData.Instance.fDiamonCurrent / MenuData.Instance.fDiamonMax, 0.75f);
        CurrentMenuDiamondText.text = "" + MenuData.Instance.fDiamonCurrent;

    }
}
