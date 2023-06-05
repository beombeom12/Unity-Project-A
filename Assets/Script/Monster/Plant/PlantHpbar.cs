using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantHpbar : MonoBehaviour
{
    public static PlantHpbar Instance
    {
        get
        {
            if (PlantHpBarInstance == null)
            {
                PlantHpBarInstance = FindObjectOfType<PlantHpbar>();
                if (PlantHpBarInstance == null)
                {
                    var InstanceContainer = new GameObject("PlantHpbar");
                    PlantHpBarInstance = InstanceContainer.AddComponent<PlantHpbar>();
                }
            }
            return PlantHpBarInstance;
        }
    }

    private static PlantHpbar PlantHpBarInstance;


    public Slider PlantHpSlider;
    public Slider LateHpSlider;

    public Transform Plant;

    public float fPlantHp = 1490f;
    public float fPlantMaxHp = 1490f;

    public bool bLateBackHit = false;


    // Start is called before the first frame update
    void Update()
    {
        transform.position = new Vector3(Plant.transform.position.x, Plant.transform.position.y + 1f, Plant.transform.position.z);

        PlantHpSlider.value = Mathf.Lerp(PlantHpSlider.value, fPlantHp / fPlantMaxHp, Time.deltaTime * 5f);
        
        if(bLateBackHit)
        {
            LateHpSlider.value = Mathf.Lerp(LateHpSlider.value, PlantHpSlider.value, Time.deltaTime * 1f);
        }
    }



    public void Damage(float fDamage)
    {
        fPlantHp -= fDamage;

        Invoke("LateDamage", 0.7f);
    }

    public void LateDamage()
    {
        bLateBackHit = true;
    }

}
