using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretArrow : MonoBehaviour
{


    public static TurretArrow Instance
    {
        get
        {
            if (TurretArrowInstance == null)
            {
                TurretArrowInstance = FindObjectOfType<TurretArrow>();
                if (TurretArrowInstance == null)
                {
                    var InstanceContainer = new GameObject("TurretArrow");
                    TurretArrowInstance = InstanceContainer.AddComponent<TurretArrow>();
                }
            }
            return TurretArrowInstance;
        }
    }
    private static TurretArrow TurretArrowInstance;


    public Rigidbody TurretArrowRigid;
    Vector3 vDirection;
    public float fDamage = 100f;   
    // Start is called before the first frame update
    void Start()
    {
        TurretArrowRigid = GetComponent<Rigidbody>();
        vDirection = transform.up + transform.forward;
        TurretArrowRigid.velocity = vDirection * 10f;

        Destroy(gameObject, 5f);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerHpBar.Instance.TakeDamaged(100f);
            PlayerSkillData.Instance.PlayerTakeDamage(100f);
            Destroy(gameObject, 0.01f);
        }

        if (collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.01f); 
        }
    }


}
