using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGolemShot : MonoBehaviour
{
    public static SmallGolemShot Instance
    {
        get
        {
            if (SmallGolemShotInstance == null)
            {
                SmallGolemShotInstance = FindObjectOfType<SmallGolemShot>();
                if (SmallGolemShotInstance == null)
                {
                    var InstanceContainer = new GameObject("SmallGolemShot");
                    SmallGolemShotInstance = InstanceContainer.AddComponent<SmallGolemShot>();
                }
            }
            return SmallGolemShotInstance;
        }
    }
    private static SmallGolemShot SmallGolemShotInstance;

    Rigidbody SmallGolemShotRigid;
    public float fDmaage = 200f;




    // Start is called before the first frame update
    void Start()
    {
        SmallGolemShotRigid = GetComponent<Rigidbody>();
        SmallGolemShotRigid.velocity =  transform.forward * 10f;

    }



    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.transform.CompareTag("Wall"))
        {
            Instantiate(EffectScript.Instance.LastBossWallHittedEffect, transform.position, Quaternion.Euler(0f, 0f, 0f));
            SoundManager.Instance.MonsterSound("WallDead", SoundManager.Instance.bgList[45]);
            Destroy(gameObject, 0.01f);
        }

        if(collision.transform.CompareTag("Player"))
        {
            PlayerHpBar.Instance.TakeDamaged(fDmaage);
            PlayerSkillData.Instance.PlayerTakeDamage(fDmaage);
            Destroy(gameObject, 0.01f);
        }
    }

}
