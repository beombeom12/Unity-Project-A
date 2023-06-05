using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    public static ArrowShot Instance
    {
        get
        {
            if (ArrowShotInstance == null)
            {
                ArrowShotInstance = FindObjectOfType<ArrowShot>();
                if (ArrowShotInstance == null)
                {
                    var InstanceContainer = new GameObject("ArrowShot");
                    ArrowShotInstance = InstanceContainer.AddComponent<ArrowShot>();
                }
            }
            return ArrowShotInstance;
        }
    }

    private static ArrowShot ArrowShotInstance;
    Rigidbody arrowRigid;
    public float fDamage = 230f;
    public int bounceCount = 3;
    Vector3 Direction;




    // Start is called before the first frame update
    void Start()
    {

        arrowRigid = GetComponent<Rigidbody>();

        Direction = transform.up + transform.forward * 20f;

        arrowRigid.velocity = Direction;
    }


    private void OnCollisionEnter(Collision collision)
    {
   

        if(collision.transform.CompareTag("Wall"))
        {
            SoundManager.Instance.MonsterSound("WallDead", SoundManager.Instance.bgList[48]);
            Instantiate(EffectScript.Instance.MonsterWallHitted, transform.position, Quaternion.Euler(90f, 0f, 0f));

            Destroy(gameObject, 0.01f);
        }
  
            
        

        if(collision.transform.CompareTag("Player"))
        {
            PlayerHpBar.Instance.TakeDamaged(fDamage);
            PlayerSkillData.Instance.PlayerTakeDamage(fDamage);
            Destroy(gameObject, 0.01f);
        }

    }



}
