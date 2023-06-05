using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShooterScript : MonoBehaviour
{
    public static TreeShooterScript Instance
    {
        get
        {
            if (TreeShooterInstance == null)
            {
                TreeShooterInstance = FindObjectOfType<TreeShooterScript>();
                if (TreeShooterInstance == null)
                {
                    var InstanceContainer = new GameObject("TreeShooterScript");
                    TreeShooterInstance = InstanceContainer.AddComponent<TreeShooterScript>();
                }
            }
            return TreeShooterInstance;
        }
    }

    private static TreeShooterScript TreeShooterInstance;

    Rigidbody ThrowTreeRigid;
    public float fDamage = 180f;

    // Start is called before the first frame update
    void Start()
    {
        ThrowTreeRigid = GetComponent<Rigidbody>();
        ThrowTreeRigid.velocity = transform.up +transform.forward  * 20f; 


    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.transform.CompareTag("Wall"))
        {
            SoundManager.Instance.MonsterSound("WallDead", SoundManager.Instance.bgList[48]);
            Instantiate(EffectScript.Instance.MonsterWallHitted, transform.position, Quaternion.Euler(90f, 0f, 0f));

            Destroy(gameObject, 0.05f);
        }



        //각 몬스터쏘는건 직접 가지고있는다.
        if (collision.transform.CompareTag("Player"))
        {

            PlayerHpBar.Instance.TakeDamaged(180f);
            PlayerSkillData.Instance.PlayerTakeDamage(180f);
            Destroy(gameObject, 0.08f);
        }



    }


}
