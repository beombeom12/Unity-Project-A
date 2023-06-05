using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSkeletonBallWeapon : MonoBehaviour
{

    public float fDamage = 130f;




    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            PlayerSkillData.Instance.PlayerTakeDamage(130f);
            PlayerHpBar.Instance.TakeDamaged(130f);
        }
            
    }
}
