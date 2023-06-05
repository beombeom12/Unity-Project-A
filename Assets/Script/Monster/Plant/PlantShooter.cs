using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShooter : MonoBehaviour
{
    public static PlantShooter Instance
    {
        get
        {
            if (PlantShooterInstance == null)
            {
                PlantShooterInstance = FindObjectOfType<PlantShooter>();
                if (PlantShooterInstance == null)
                {
                    var InstanceContainer = new GameObject("PlantShooter");
                    PlantShooterInstance = InstanceContainer.AddComponent<PlantShooter>();
                }
            }
            return PlantShooterInstance;
        }
    }
    private static PlantShooter PlantShooterInstance;

    Rigidbody PlantShooterRigid;
    public float fDamage = 180f;


    // Start is called before the first frame update
    void Start()
    {
       
        PlantShooterRigid = GetComponent<Rigidbody>();
        PlantShooterRigid.velocity = transform.up + transform.forward * 20f;

    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.transform.CompareTag("Wall"))
        {
            SoundManager.Instance.MonsterSound("WallDead", SoundManager.Instance.bgList[48]);
            Instantiate(EffectScript.Instance.MonsterWallHitted, transform.position, Quaternion.Euler(90f, 0f, 0f));
            Destroy(gameObject, 0.05f);
        }

        if(collision.transform.CompareTag("Player"))
        {
            PlayerHpBar.Instance.TakeDamaged(180f);
            PlayerSkillData.Instance.PlayerTakeDamage(180f);
            Destroy(gameObject, 0.08f);

        }
    }
}
