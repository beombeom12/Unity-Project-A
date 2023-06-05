using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertShooter2 : MonoBehaviour
{

    Rigidbody DesertShooterRigid;
    Vector3 vDirection;
    public float fDamage;

    // Start is called before the first frame update
    void Start()
    {

        DesertShooterRigid = GetComponent<Rigidbody>();

        vDirection = - transform.up - transform.forward;

        DesertShooterRigid.velocity = vDirection * 10f;


    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {

        }

        if (collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.01f);
        }
    }
}
