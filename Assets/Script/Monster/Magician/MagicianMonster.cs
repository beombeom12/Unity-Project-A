using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianMonster : MonoBehaviour
{
    public static MagicianMonster Instance
    {
        get
        {
            if (MagicianMonsterInstance == null)
            {
                MagicianMonsterInstance = FindObjectOfType<MagicianMonster>();
                if (MagicianMonsterInstance == null)
                {
                    var InstanceContainer = new GameObject("MagicianMonster");
                    MagicianMonsterInstance = InstanceContainer.AddComponent<MagicianMonster>();
                }
            }
            return MagicianMonsterInstance;
        }
    }


    private static MagicianMonster MagicianMonsterInstance;

    public GameObject MagicianCanvas;
    public GameObject FireShot;

    public Transform MeleeAttack;

    public float fWeaponDamage;
    public float fWeaponCritical = 1.3f;
    public float fSphereWeaponDamage;
    public float fCollisionDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
