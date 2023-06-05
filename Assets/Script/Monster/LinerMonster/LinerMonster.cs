using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerMonster : ShooterFSM
{

    public static LinerMonster Instance
    {
        get
        {
            if (LinerMonsterInstance == null)
            {
                LinerMonsterInstance = FindObjectOfType<LinerMonster>();
                if (LinerMonsterInstance == null)
                {
                    var InstanceContainer = new GameObject("LinerMonster");
                    LinerMonsterInstance = InstanceContainer.AddComponent<LinerMonster>();
                }
            }
            return LinerMonsterInstance;
        }
    }



    private static LinerMonster LinerMonsterInstance;

    public GameObject MeleeAttack;
    public GameObject LaserShot;
    public Transform PlayerTrasform;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fMonsterFeeling);

        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, fAttackRange);
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        fAttackCoolTime = 4f;

        fAttackRange = 15f;
        fMonsterFeeling = 25f;
        fCurrenHp = 10000f;
        fMaxHp = 10000f;

        StartCoroutine(ResetAttackArea());


    }

    // Update is called once per frame
    void Update()
    {
        if (fCurrenHp <= 0)
        {
            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);

            PlayerTargeting.Instance.iTargetingIndex = -1;
            PetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PetTargeting.Instance.iTargetingIndex = -1;
            BatPetTargeting.Instance.MonsterList.Remove(transform.gameObject);
            BatPetTargeting.Instance.iTargetingIndex = -1;
            Destroy(transform.parent.gameObject);
        }

    }

    IEnumerator ResetAttackArea()
    {
        while (!MeleeAttack.activeInHierarchy && currentState == ShooterState.Attack)
        {
            yield return new WaitForSeconds(fAttackCoolTime);




        }

    }



    public void Shoot()
    {
        Instantiate(LaserShot, MeleeAttack.transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(90f, 0f, 0f)));
    }


}
