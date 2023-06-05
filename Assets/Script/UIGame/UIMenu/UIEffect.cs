using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffect : MonoBehaviour
{

    public static UIEffect Instance
    {
        get
        {
            if (UIEffectInstance == null)
            {
                UIEffectInstance = FindObjectOfType<UIEffect>();
                if (UIEffectInstance == null)
                {
                    var InstanceContainer = new GameObject("UIEffect");
                    UIEffectInstance = InstanceContainer.AddComponent<UIEffect>();
                }
            }
            return UIEffectInstance;
        }
    }
    private static UIEffect UIEffectInstance;


    [Header("UIButton")]
    public GameObject ButtonEffect;



    [Header("UIPlayer")]
    public GameObject UIPlayerEquipWeaponEffect;
    public GameObject UIPlayerEquipArmorEffect;
    public GameObject UIPlayerEquipRightRingEffect;
    public GameObject UIPlayerEquipLeftRingEffct;
    public GameObject UIPlayerEquipRightPetEffect;
    public GameObject UIPlayerEquipLeftPetEffect;


    // Start is called before the first frame update
    void Start()
    {
      
    }


}
