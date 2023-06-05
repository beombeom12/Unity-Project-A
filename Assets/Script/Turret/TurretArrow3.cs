using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretArrow3 : MonoBehaviour
{
    Rigidbody TurretArrowRigid;
    Vector3 vDirection;

    // Start is called before the first frame update
    void Start()
    {

        TurretArrowRigid = GetComponent<Rigidbody>();

        vDirection = transform.up + transform.forward;

        TurretArrowRigid.velocity = vDirection * 10f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
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
