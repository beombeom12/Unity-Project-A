using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDangerLine : MonoBehaviour
{
    TrailRenderer traillDanger;
    public Vector3 EndPosition;


    // Start is called before the first frame update
    void Start()
    {
        traillDanger = GetComponent<TrailRenderer>();


        traillDanger.startColor = new Color(1f, 0f, 0f, 0.7f);
        traillDanger.endColor = new Color(1f, 0f, 0f, 0.7f);

        Destroy(gameObject, 3f);

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, EndPosition, Time.deltaTime * 3f);

    }
}
