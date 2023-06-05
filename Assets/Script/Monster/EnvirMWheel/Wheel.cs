using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public static Wheel Instance
    {
        get
        {
            if (WheelInstance == null)
            {
                WheelInstance = FindObjectOfType<Wheel>();
                if (WheelInstance == null)
                {
                    var InstanceContainer = new GameObject("Wheel");
                    WheelInstance = InstanceContainer.AddComponent<Wheel>();
                }
            }
            return WheelInstance;
        }
    }





    private static Wheel WheelInstance;

    public float RotationSpeed = 150f;
    public float MovementSpeed = 3f;


    Rigidbody WheelRigidBody;

    Vector3 vDirection;
    public float fDamage;




    private void Start()
    {
        WheelRigidBody = GetComponent<Rigidbody>();
        vDirection = transform.right;
        fDamage = 50f;
    }

    // Update is called once per frame
    void Update()
    {


        WheelRigidBody.velocity = Vector3.zero;

        transform.Rotate(0f, 0f, RotationSpeed + Time.deltaTime);

        transform.position += -vDirection * MovementSpeed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (WheelRigidBody != null && collision.transform.CompareTag("Wall"))
        {
            // 벽에 부딪히면 vDirection을 반대 방향으로 변경합니다.
            vDirection = -vDirection;
        }

    }





}
