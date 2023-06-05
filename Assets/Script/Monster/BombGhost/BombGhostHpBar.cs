using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BombGhostHpBar : MonoBehaviour
{

    public static BombGhostHpBar Instance
    {
        get
        {
            if (BombGhostHpInstance == null)
            {
                BombGhostHpInstance = FindObjectOfType<BombGhostHpBar>();
                if (BombGhostHpInstance == null)
                {
                    var InstanceContainer = new GameObject("BombGhostHpBar");
                    BombGhostHpInstance = InstanceContainer.AddComponent<BombGhostHpBar>();
                }
            }
            return BombGhostHpInstance;
        }
    }
    private static BombGhostHpBar BombGhostHpInstance;


    public Slider BombGhostHpSlider;
    public Slider LateSliderHpSlider;

    public Transform BombGhost;
    public float fBombGhostHp = 1290f;
    public float fBombGhostMaxHp = 1290f;

    public bool bLateHit = false;

    private void Update()
    {
        transform.position = new Vector3(BombGhost.transform.position.x, BombGhost.transform.position.y + 1f, BombGhost.transform.position.z);

        BombGhostHpSlider.value = Mathf.Lerp(BombGhostHpSlider.value, fBombGhostHp / fBombGhostMaxHp, Time.deltaTime * 5f);

        if(bLateHit)
        {
            LateSliderHpSlider.value = Mathf.Lerp(LateSliderHpSlider.value, BombGhostHpSlider.value, Time.deltaTime * 1f);

        }
    }


    public void Damage(float fDamage)
    {
        fBombGhostHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }

    public void LateDamage()
    {
        bLateHit = true;
    }


}
