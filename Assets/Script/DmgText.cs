using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DmgText : MonoBehaviour
{


    public static DmgText Instance
    {
        get
        {
            if (DmgTextInstance == null)
            {
                DmgTextInstance = FindObjectOfType<DmgText>();
                if (DmgTextInstance == null)
                {
                    var InstanceContainer = new GameObject("DmgText");
                    DmgTextInstance = InstanceContainer.AddComponent<DmgText>();
                }
            }
            return DmgTextInstance;
        }
    }


    private static DmgText DmgTextInstance;

    public TextMesh DamageText;






    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 2f);

    }

    public void ScreenLookDamage(float WeaponDamage, bool IsCritical)
    {
        if (IsCritical)
        {
            DamageText.text = "<color=#ff0000>" + "-" + WeaponDamage + "</color>";
        }
        else
        {
            DamageText.text = "<color=#ffffff>" + "-" + WeaponDamage + "</color>";
        }
    }


}
