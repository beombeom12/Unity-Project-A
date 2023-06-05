using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniDragonHp : MonoBehaviour
{

    public static MiniDragonHp Instance
    {
        get
        {
            if (MiniDragonInstance == null)
            {
                MiniDragonInstance = FindObjectOfType<MiniDragonHp>();
                if (MiniDragonInstance == null)
                {
                    var InstanceContainer = new GameObject("MiniDragonHp");
                    MiniDragonInstance = InstanceContainer.AddComponent<MiniDragonHp>();
                }
            }
            return MiniDragonInstance;
        }
    }
    private static MiniDragonHp MiniDragonInstance;

    public Slider MiniDragonHpSlider;
    public Slider LateHpSlider;

    public Transform MiniDragons;

    public float fMiniDragonHp = 1600f;
    public float fMiniDragonMaxHp = 1600f;


    public bool bLateHit = false;
    // Start is called before the first frame update
    private void Update()
    {
        transform.position = new Vector3(MiniDragons.transform.position.x, MiniDragons.transform.position.y + 1f, MiniDragons.transform.position.z);

        MiniDragonHpSlider.value = Mathf.Lerp(MiniDragonHpSlider.value, fMiniDragonHp / fMiniDragonMaxHp, Time.deltaTime * 5f);

        if(bLateHit)
        {
            LateHpSlider.value = Mathf.Lerp(LateHpSlider.value, MiniDragonHpSlider.value, Time.deltaTime);
        }

    }

    public void Damage(float fDamage)
    {
        fMiniDragonHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }    

    public void LateDamage()
    {
        bLateHit = true;
    }
}
