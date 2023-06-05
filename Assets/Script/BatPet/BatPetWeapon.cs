using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPetWeapon : MonoBehaviour
{

    Rigidbody BepetWeaponrigid;
    Vector3 vDirection;
    public float fDamage;





    // Start is called before the first frame update
    void Start()
    {
        BepetWeaponrigid = GetComponent<Rigidbody>();
        vDirection = transform.forward ;

        BepetWeaponrigid.velocity = vDirection * 5f;

        fDamage = PlayerSkillData.Instance.fBatPetWeaponDamage;

        Destroy(gameObject, 10f);        
    }



    private void OnTriggerEnter(Collider other)
    {
        //몬스터에게 충돌시
        if(other.transform.CompareTag("Monster"))
        {


            Destroy(gameObject, 0.01f);
        }




         //벽에 맞는경우
        if(other.transform.CompareTag("Wall"))
        {

            Destroy(gameObject, 0.01f);
        }


    
    }

}
