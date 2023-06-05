using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowShooter : MonoBehaviour
{
    public static PlayerBowShooter Instance
    {
        get
        {
            if (PlayerBowShooterInstance == null)
            {
                PlayerBowShooterInstance = FindObjectOfType<PlayerBowShooter>();
                if (PlayerBowShooterInstance == null)
                {
                    var InstanceContainer = new GameObject("PlayerBowShooter");
                    PlayerBowShooterInstance = InstanceContainer.AddComponent<PlayerBowShooter>();
                }
            }
            return PlayerBowShooterInstance;
        }
    }
    private static PlayerBowShooter PlayerBowShooterInstance;

    Rigidbody BowShooterRigid;
    Vector3 vDirection;
    public float fDamage;

    public int ibouneCount = 2;
    public int iWallBounceCount = 2;


    // Start is called before the first frame update
    void Start()
    {
        BowShooterRigid = GetComponent<Rigidbody>();
        vDirection = transform.forward;

        BowShooterRigid.velocity = vDirection * 20f;

        fDamage = PlayerSkillData.Instance.fDamage;

        Destroy(gameObject, 2f);
        
    }
    Vector3 GetNextMonsterDirection(Collider currentMonster)
    {


        int currentIndex = PlayerTargeting.Instance.MonsterList.IndexOf(currentMonster.gameObject.transform.parent.gameObject);




        int nextIndex = currentIndex + 1;
        if (nextIndex >= PlayerTargeting.Instance.MonsterList.Count) // ������ ������ ��� ù ��° ���͸� �������� ����
        {
            nextIndex = 0;
        }

        //���⿡ ���� ���� ��ġ�� ���̿��� �����Ƿ� ���⼭ CurrentMonster���ԵǸ� ���� Ÿ���ÿ��� ������ ����� �ȴ�. 
        return (PlayerTargeting.Instance.MonsterList[nextIndex].transform.position - transform.position).normalized;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster") && BowShooterRigid != null)
        {
            if(PlayerSkillData.Instance.BowPlayerSkill[0] != 0 && PlayerTargeting.Instance.MonsterList.Count >= 2)
            {
                if(ibouneCount > 0)
                {
                    ibouneCount--;
                    fDamage *= 0.5f;
                    vDirection = GetNextMonsterDirection(other) * 30f;
                    BowShooterRigid.velocity = vDirection;
                    return;
                }
            }
            BowShooterRigid.velocity = Vector3.zero;
            Destroy(gameObject, 0.01f);

        }
        else if(other.CompareTag("Wall") && BowShooterRigid != null)
        {
            if (PlayerSkillData.Instance.BowPlayerSkill[4] == 0)
            {
                BowShooterRigid.velocity = Vector3.zero;
                Destroy(gameObject, 0.01f);
            }
        }
    }




    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.transform.CompareTag("Wall") && BowShooterRigid != null)
        {
            if(PlayerSkillData.Instance.BowPlayerSkill[4] != 0)
            {
                if(iWallBounceCount > 0)
                {
                    iWallBounceCount--;
                    fDamage *= 0.5f;
                    vDirection = Vector3.Reflect(vDirection, collision.contacts[0].normal);
                    BowShooterRigid.velocity = vDirection * 20f;
                    return;
                }
            }
            BowShooterRigid.velocity = Vector3.zero;
            Destroy(gameObject);
        }


    }
}
