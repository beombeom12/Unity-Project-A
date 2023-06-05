using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPetChasing : MonoBehaviour
{
    public static BatPetChasing Instance
    {
        get
        {
            if (BatPetChasingInstance == null)
            {
                BatPetChasingInstance = FindObjectOfType<BatPetChasing>();
                if (BatPetChasingInstance == null)
                {
                    var InstanceContainer = new GameObject("BatPetChasing");
                    BatPetChasingInstance = InstanceContainer.AddComponent<BatPetChasing>();
                }
            }
            return BatPetChasingInstance;
        }
    }





    private static BatPetChasing BatPetChasingInstance;

    //BatPet

    public Transform Player;


    public float yPosition = 1f;

    public float fRadius = 2.5f;

    public float fAngularVeloctiy = 40f;

    public float fAngle = 0f;

    public float fSpeed = 0.2f;

    public float fSphereDamage;


   public  Rigidbody BatPetChasingRigid;
   public  Animator BatPetAnim;



    // Start is called before the first frame update
    void Start()
    {
        BatPetChasingRigid = GetComponent<Rigidbody>();
        BatPetAnim = GetComponent<Animator>();

        Vector3 relativePosition = transform.position - Player.position;
        fAngle = Mathf.Atan2(relativePosition.z, relativePosition.x) * Mathf.Rad2Deg;



    }


    void Update()
    {




        fAngle += fAngularVeloctiy * Time.deltaTime;
        float xPosition = Player.transform.position.x + Mathf.Cos(fAngle * Mathf.Deg2Rad) * fRadius;
        float zPosition = Player.transform.position.z + Mathf.Sin(fAngle * Mathf.Deg2Rad) * fRadius;
        transform.position = new Vector3(xPosition, yPosition - 0.5f, zPosition);


        //Instantiate(EffectScript.Instance.PlayerChasingWeaponEffect, transform.position, Quaternion.identity);



    }


}
