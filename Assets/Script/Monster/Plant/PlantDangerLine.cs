using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDangerLine : MonoBehaviour
{

    TrailRenderer trailRenderer;

    public Vector3 EndPosition;


    void Start()
    {

        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.startColor = new Color(1f, 0f, 0f,1f);
        trailRenderer.endColor = new Color(1f, 0f, 0f, 1f);

        Destroy(gameObject, 3f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, EndPosition, Time.deltaTime * 3f);
    }
}
