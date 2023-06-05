using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{

    public float fRotateSpeed = 20f;

    private void Update()
    {
        transform.Rotate(0f, fRotateSpeed * Time.deltaTime, 0f);        
    }


}

