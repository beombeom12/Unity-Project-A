using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpShotWeapon2 : MonoBehaviour
{
    public static UpShotWeapon2 Instance
    {
        get
        {
            if (UpShotWeaponInstance2 == null)
            {
                UpShotWeaponInstance2 = FindObjectOfType<UpShotWeapon2>();
                if (UpShotWeaponInstance2 == null)
                {
                    var InstanceContainer = new GameObject("UpShotWeapon2");
                    UpShotWeaponInstance2 = InstanceContainer.AddComponent<UpShotWeapon2>();
                }
            }
            return UpShotWeaponInstance2;
        }
    }


    private static UpShotWeapon2 UpShotWeaponInstance2;

    public Rigidbody UpShotWeaponRigidBody;
    public float fDamage = 180f;
    public float projectileSpeed = 10f;
    public float launchAngle = 45f;


    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");




        Vector3 fDistance = new Vector3(Player.transform.position.x + 0.8f, Player.transform.position.y, Player.transform.position.z) - transform.position;
        float vDistance = Mathf.Pow((fDistance.x) * (fDistance.x) + (fDistance.z) * (fDistance.z), 0.5f);




        Vector3 velocity = new Vector3(fDistance.x, vDistance * Mathf.Tan(launchAngle * Mathf.Deg2Rad), fDistance.z).normalized;

        velocity = velocity * Mathf.Sqrt(Mathf.Abs(Physics.gravity.y) * vDistance / Mathf.Sin(2 * launchAngle * Mathf.Deg2Rad));

        UpShotWeaponRigidBody = GetComponent<Rigidbody>();
        UpShotWeaponRigidBody.velocity = velocity;
    }


    private void Update()
    {
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //벽에 맞아 없어지는 경우
        if (collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.1f);

        }



        //플레이어에게 맞아 데미지를 주는경우
        if (collision.transform.CompareTag("Player"))
        {
            PlayerSkillData.Instance.PlayerTakeDamage(180f);
            PlayerHpBar.Instance.TakeDamaged(180f);
            Destroy(gameObject, 0.1f);
        }

        if (collision.transform.CompareTag("Plane"))
        {
            SoundManager.Instance.MonsterSound("UpShootPlane", SoundManager.Instance.bgList[47]);
            Instantiate(EffectScript.Instance.UpShotShoot, transform.position, transform.rotation);
            Destroy(gameObject, 0.1f);

        }

    }



}
