using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound3Weapon : MonoBehaviour
{

    Rigidbody Shooter3Bound;


    Vector3 vDir;
    int iBoundCount = 3;


    // Start is called before the first frame update
    void Start()
    {
        Shooter3Bound = GetComponent<Rigidbody>();

        vDir = transform.up + transform.forward  * -20f;

        Shooter3Bound.velocity = vDir;
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.CompareTag("Wall"))
        {
            iBoundCount--;

            if (iBoundCount > 0)
            {
                vDir = Vector3.Reflect(vDir, collision.contacts[0].normal);

                Shooter3Bound.velocity = vDir * -20f;
            }
            else
            {
                Destroy(gameObject, 0.1f);

            }

        }


        if(collision.transform.CompareTag("Player"))
        {

        }

    }


}
