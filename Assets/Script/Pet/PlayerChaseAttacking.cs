using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaseAttacking : MonoBehaviour
{

    public static PlayerChaseAttacking Instance
    {
        get
        {
            if (PlayerChaseInstnace == null)
            {
                PlayerChaseInstnace = FindObjectOfType<PlayerChaseAttacking>();
                if (PlayerChaseInstnace == null)
                {
                    var InstanceContainer = new GameObject("PlayerChaseAttacking");
                    PlayerChaseInstnace = InstanceContainer.AddComponent<PlayerChaseAttacking>();
                }
            }
            return PlayerChaseInstnace;
        }
    }
    private static PlayerChaseAttacking PlayerChaseInstnace;


    //Pet

    public Transform Player;


    public float yPosition = 1f;

    public float fRadius = 2.5f;

    public float fAngularVeloctiy = 40f;

    public float fAngle = 0f;

    public float fSpeed = 0.2f;

    public float fSphereDamage;



    //Pet RIgidi Anim

    public Rigidbody PetRigidBody;
    public Animator Anim;




    void Start()
    {
        Vector3 relativePosition = transform.position - Player.position;
        fAngle = Mathf.Atan2(relativePosition.z , relativePosition.x  ) * Mathf.Rad2Deg;


        PetRigidBody = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
    
    }


    void Update()
    {



        
        fAngle += fAngularVeloctiy * Time.deltaTime;
        float xPosition = Player.transform.position.x  + Mathf.Cos(fAngle * Mathf.Deg2Rad) * fRadius;
        float zPosition = Player.transform.position.z  + Mathf.Sin(fAngle * Mathf.Deg2Rad) * fRadius;
        transform.position = new Vector3(xPosition, yPosition -0.5f, zPosition);

      
        //Instantiate(EffectScript.Instance.PlayerChasingWeaponEffect, transform.position, Quaternion.identity);


    
    }


}
