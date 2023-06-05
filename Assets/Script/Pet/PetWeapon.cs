using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetWeapon : MonoBehaviour
{
    public static PetWeapon Instance
    {
        get
        {
            if (PetWeaponInstance == null)
            {
                PetWeaponInstance = FindObjectOfType<PetWeapon>();
                if (PetWeaponInstance == null)
                {
                    var InstanceContainer = new GameObject("PetWeapon");
                    PetWeaponInstance = InstanceContainer.AddComponent<PetWeapon>();
                }
            }
            return PetWeaponInstance;
        }
    }
    private static PetWeapon PetWeaponInstance;



    Rigidbody PetWeaponRigid;
    Vector3 vDirection;
     public float fDamage;

    // Start is called before the first frame update
    void Start()
    {
        PetWeaponRigid = GetComponent<Rigidbody>();
        vDirection = transform.forward;
        PetWeaponRigid.velocity = vDirection * 20f;
        fDamage = PlayerSkillData.Instance.fPetDamage;
    }


    public void Update()
    {
        Destroy(gameObject, 5f);
    }

}
