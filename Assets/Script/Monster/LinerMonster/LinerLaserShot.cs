using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerLaserShot : MonoBehaviour
{
    public static LinerLaserShot Instance
    {
        get
        {
            if (LinerMonsterInstance == null)
            {
                LinerMonsterInstance = FindObjectOfType<LinerLaserShot>();
                if (LinerMonsterInstance == null)
                {
                    var InstanceContainer = new GameObject("LinerLaserShot");
                    LinerMonsterInstance = InstanceContainer.AddComponent<LinerLaserShot>();
                }
            }
            return LinerMonsterInstance;
        }
    }
    private static LinerLaserShot LinerMonsterInstance;

    Rigidbody LaserRigid;
    GameObject Player;
    public BoxCollider box;
    public float fDamage = 50f;

    public float fCollisionDelay = 10f;
    public bool bCollisionDealy = true;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");


        LaserRigid = GetComponent<Rigidbody>();

        LaserRigid.velocity = Vector3.zero;
    }


    private void OnTriggerEnter(Collider other)
    {
       if(other.transform.CompareTag("Player") && bCollisionDealy)
        {
            StartCoroutine(CollisionDelay(other));
           

        }
    }




    IEnumerator CollisionDelay(Collider other)
    {
        bCollisionDealy = false;

        yield return new WaitForSeconds(0.5f);
        PlayerHpBar.Instance.TakeDamaged(fDamage);
        PlayerSkillData.Instance.PlayerTakeDamage(fDamage);
        //Instantiate(EffectScript.Instance.PlayerWalkEffect, new Vector3(Player.transform.position.x, Player.transform.position.y + 0.3f, Player.transform.position.z), Quaternion.identity);
        bCollisionDealy = true;

    }




}