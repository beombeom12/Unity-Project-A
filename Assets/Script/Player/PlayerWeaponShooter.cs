using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponShooter : MonoBehaviour
{
    public static PlayerWeaponShooter Instance
    {
        get
        {
            if (PlayerWeaponInstance == null)
            {
                PlayerWeaponInstance = FindObjectOfType<PlayerWeaponShooter>();
                if (PlayerWeaponInstance == null)
                {
                    var InstanceContainer = new GameObject("PlayerWeaponShooter");
                    PlayerWeaponInstance = InstanceContainer.AddComponent<PlayerWeaponShooter>();
                }
            }
            return PlayerWeaponInstance;
        }
    }



    private static PlayerWeaponShooter PlayerWeaponInstance;
    public int ibouneCount = 2;
    public int iWallBounceCount = 2;

    Rigidbody WeaponRigidbody;
    Vector3 vDirection;
    public float fDamage;


    // Start is called before the first frame update
    void Start()
    {
        WeaponRigidbody = GetComponent<Rigidbody>();
        vDirection =  transform.forward;
        WeaponRigidbody.velocity = vDirection * 20f;
        fDamage = PlayerSkillData.Instance.fDamage; // null üũ �߰�




        Destroy(gameObject, 2f);


    }

    // �������� �ε��� ������ ������ ����ϴ� �Լ�
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
        if (other.CompareTag("Monster") && WeaponRigidbody != null)
        {
            if (PlayerSkillData.Instance.PlayerSkill[0] != 0 && PlayerTargeting.Instance.MonsterList.Count >= 2)
            {
                if (ibouneCount > 0)
                {
                    ibouneCount--;
                    fDamage *= 0.5f;
                    vDirection = GetNextMonsterDirection(other) * 30f; // ���� ���� �������� ������ ����
                    WeaponRigidbody.velocity = vDirection;
                    return;
                }
            }

            WeaponRigidbody.velocity = Vector3.zero;
            Destroy(gameObject, 0.1f);
        }
        else if (other.CompareTag("Wall") && WeaponRigidbody != null)
        {
            if (PlayerSkillData.Instance.PlayerSkill[4] == 0)
            {
                WeaponRigidbody.velocity = Vector3.zero;
                Destroy(gameObject, 0.2f);
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall") && WeaponRigidbody != null)
        {
            if (PlayerSkillData.Instance.PlayerSkill[4] != 0)
            {
                if (iWallBounceCount > 0)
                {
                    iWallBounceCount--;
                    fDamage *= 0.5f;
                    vDirection = Vector3.Reflect(vDirection, collision.contacts[0].normal);
                    WeaponRigidbody.velocity = vDirection * 20f;
                    return;
                }
            }
            WeaponRigidbody.velocity = Vector3.zero;
            Destroy(gameObject);
        }




    }



}
