using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossBolt : MonoBehaviour
{
    Rigidbody LastBossRigidBody;
    public float fDamage = 100f;

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        LastBossRigidBody = GetComponent<Rigidbody>();
        LastBossRigidBody.velocity = transform.forward * 10f;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            Instantiate(EffectScript.Instance.LastBossWallHittedEffect, transform.position, Quaternion.Euler(0f, 0f, 0f));
            SoundManager.Instance.MonsterSound("WallDead", SoundManager.Instance.bgList[45]);
            Destroy(gameObject, 0.1f);
        }


        if (collision.transform.CompareTag("Player"))
        {
            PlayerSkillData.Instance.PlayerTakeDamage(100f);
            PlayerHpBar.Instance.TakeDamaged(100f);
            Instantiate(EffectScript.Instance.BombGhostHittingEffect, Player.transform.position, Quaternion.identity);

            Destroy(gameObject, 0.01f);

        }
    }
}
