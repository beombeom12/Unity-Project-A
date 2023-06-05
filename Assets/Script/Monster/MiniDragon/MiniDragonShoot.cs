using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDragonShoot : MonoBehaviour
{
    public static MiniDragonShoot Instance
    {
        get
        {
            if (DragonShootInstance == null)
            {
                DragonShootInstance = FindObjectOfType<MiniDragonShoot>();
                if (DragonShootInstance == null)
                {
                    var InstanceContainer = new GameObject("MiniDragonShoot");
                    DragonShootInstance = InstanceContainer.AddComponent<MiniDragonShoot>();
                }
            }
            return DragonShootInstance;
        }
    }
    private static MiniDragonShoot DragonShootInstance;

    Rigidbody MiniDragonWeaponrigid;
    public float fDamage = 200f;

    Vector3 vDirection;

    // Start is called before the first frame update
    void Start()
    {
        MiniDragonWeaponrigid = GetComponent<Rigidbody>();
        vDirection = transform.forward;
        MiniDragonWeaponrigid.velocity = vDirection * 10f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Wall"))
        {
            SoundManager.Instance.MonsterSound("WallDead", SoundManager.Instance.bgList[48]);
            Instantiate(EffectScript.Instance.MonsterWallHitted, transform.position, Quaternion.Euler(90f, 0f, 0f));

            Destroy(gameObject, 0.07f);
        }

        if(other.transform.CompareTag("Player"))
        {
            PlayerHpBar.Instance.TakeDamaged(200f);
            PlayerSkillData.Instance.PlayerTakeDamage(200f);
            Destroy(gameObject, 0.01f);
        }
    }
}
