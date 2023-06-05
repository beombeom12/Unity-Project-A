using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheroDangerLine : MonoBehaviour
{

    TrailRenderer trailRender;
    Rigidbody trailRigid;


    //Testing
    public int iBoundCount = 3;
    public   Vector3 vDirection;
    public Vector3 EndPoisition;
    // Start is called before the first frame update
    void Start()
    {
 

        trailRender = GetComponent<TrailRenderer>();
        trailRigid = GetComponent<Rigidbody>();
        trailRender.startColor = new Color(1f, 0f, 0f, 0.7f);
        trailRender.endColor = new Color(1f, 0f, 0f, 0.7f);


        Destroy(gameObject, 3f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, EndPoisition, Time.deltaTime * 3f);    
    }

 

}
