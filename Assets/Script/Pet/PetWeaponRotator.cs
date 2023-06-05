using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetWeaponRotator : MonoBehaviour
{
    float fPetWeaponRotateSpeed = 720f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0f, fPetWeaponRotateSpeed * Time.deltaTime * 10f, 0f);

    }
}
