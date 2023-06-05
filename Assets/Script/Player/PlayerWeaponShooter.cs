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
        fDamage = PlayerSkillData.Instance.fDamage; // null 체크 추가




        Destroy(gameObject, 2f);


    }

    // 다음으로 부딪힐 몬스터의 방향을 계산하는 함수
    Vector3 GetNextMonsterDirection(Collider currentMonster)
    {

      
        int currentIndex = PlayerTargeting.Instance.MonsterList.IndexOf(currentMonster.gameObject.transform.parent.gameObject);

    


        int nextIndex = currentIndex + 1;
        if (nextIndex >= PlayerTargeting.Instance.MonsterList.Count) // 마지막 몬스터인 경우 첫 번째 몬스터를 다음으로 설정
        {
            nextIndex = 0;
        }

        //여기에 현재 몬스터 위치의 길이에서 빼지므로 여기서 CurrentMonster들어가게되면 몬스터 타켓팅에서 문제가 생기게 된다. 
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
                    vDirection = GetNextMonsterDirection(other) * 30f; // 다음 몬스터 방향으로 방향을 변경
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
