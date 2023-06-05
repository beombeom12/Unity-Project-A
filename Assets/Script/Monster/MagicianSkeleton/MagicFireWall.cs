using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFireWall : MonoBehaviour
{



    Rigidbody MagicFireWallRigid;


    public bool bCollisionDelay = false;
    public float fFireWallDamage = 200f;
    float fCollisionDelay = 2f;
    
   
    // Start is called before the first frame update
    void Start()
    {
        MagicFireWallRigid = GetComponent<Rigidbody>();

        MagicFireWallRigid.velocity = Vector3.zero;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            StartCoroutine(DelayAttack(other));

        }
    }


    IEnumerator DelayAttack(Collider other)
    {

        bCollisionDelay = false;
        PlayerSkillData.Instance.PlayerTakeDamage(fFireWallDamage);
        PlayerHpBar.Instance.TakeDamaged(fFireWallDamage);

        yield return new WaitForSeconds(fCollisionDelay);

        bCollisionDelay = true;
    }

        




}
