using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{

    public float health { get; protected set; }
    // Start is called before the first frame update
    void Start()
    {


    }



    public virtual void RestroeHelath(float newHelath)
    {
        health += newHelath;
    }

}

