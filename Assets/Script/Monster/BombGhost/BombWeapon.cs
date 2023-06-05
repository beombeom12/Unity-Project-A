using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{


    public static BombWeapon Instance
    {
        get
        {
            if (BombWeaponInstance == null)
            {
                BombWeaponInstance = FindObjectOfType<BombWeapon>();
                if (BombWeaponInstance == null)
                {
                    var InstanceContainer = new GameObject("BombWeapon");
                    BombWeaponInstance = InstanceContainer.AddComponent<BombWeapon>();
                }
            }
            return BombWeaponInstance;
        }
    }
    private static BombWeapon BombWeaponInstance;


    public Rigidbody BombWeaponRigid;
    public float fDamage = 200f;
    public float launchAngel = 70f;

    GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");


        Vector3 fDistance = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z) - transform.position;
        float vDistance = Mathf.Pow((fDistance.x) * (fDistance.x) + (fDistance.z) * (fDistance.z), 0.5f);

        Vector3 velocity = new Vector3(fDistance.x, vDistance * Mathf.Tan(launchAngel * Mathf.Deg2Rad), fDistance.z).normalized;

        velocity = velocity * Mathf.Sqrt(Mathf.Abs(Physics.gravity.y) * vDistance / Mathf.Sin(2 * launchAngel * Mathf.Deg2Rad));

        BombWeaponRigid = GetComponent<Rigidbody>();
        BombWeaponRigid.velocity = velocity;



    }
    private void OnCollisionEnter(Collision collision)
    {
        //벽에 맞아 없어지는 경우
        if (collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.1f);

        }



        //플레이어에게 맞아 데미지를 주는경우
        if (collision.transform.CompareTag("Player"))
        {
            PlayerSkillData.Instance.PlayerTakeDamage(180f);
            PlayerHpBar.Instance.TakeDamaged(180f);
            Instantiate(EffectScript.Instance.BombGhostHittingEffect, Player.transform.position, Quaternion.identity);
            Destroy(gameObject, 0.001f);
        }

        //벽에 맞아 없어지는 경우
        if (collision.transform.CompareTag("Plane"))
        {
            SoundManager.Instance.MonsterSound("BombDropPlane", SoundManager.Instance.bgList[46]);
            Instantiate(EffectScript.Instance.BombGhostHittingEffect, transform.position, transform.rotation);
            Destroy(gameObject, 0.1f);

        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Ground"))
        {
            Instantiate(EffectScript.Instance.BombGhostHittingEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

}
