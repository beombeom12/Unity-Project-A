using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour
{

    public static MagicShot Instance
    {
        get
        {
            if (MagicShotInstance == null)
            {
                MagicShotInstance = FindObjectOfType<MagicShot>();
                if (MagicShotInstance == null)
                {
                    var InstanceContainer = new GameObject("MagicShot");
                    MagicShotInstance = InstanceContainer.AddComponent<MagicShot>();
                }
            }
            return MagicShotInstance;
        }
    }
    private static MagicShot MagicShotInstance;

    Rigidbody MagicShotrigid;
    GameObject Player;


    public float fDamage = 230f;
    // Start is called before the first frame update
    void Start()
    {
        MagicShotrigid = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");

        MagicShotrigid.velocity =  -transform.up * 10f + transform.forward;
    }




    private void OnCollisionEnter(Collision collision)
    {
        //벽에 부딪힐 경우
        if(collision.transform.CompareTag("Wall"))
        {
            SoundManager.Instance.MonsterSound("WallDead", SoundManager.Instance.bgList[48]);
            Instantiate(EffectScript.Instance.FireHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(gameObject, 0.01f);
        }

        //플레이어에게 충돌할경우
        if(collision.transform.CompareTag("Player"))
        {
            PlayerSkillData.Instance.PlayerTakeDamage(160f);
            PlayerHpBar.Instance.TakeDamaged(160f);

            Instantiate(EffectScript.Instance.FireHittedEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(gameObject, 0.01f);
        }
    }
}
