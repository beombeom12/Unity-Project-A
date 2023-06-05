using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float RotationSpeed = 720f;
    public AudioClip CommandRotateSoundClip;



    private void Start()
    {
        SoundManager.Instance.PlayerSound("CommandSwordRotateSound", SoundManager.Instance.bgList[9]);
    }
    private void Update()
    {
        transform.Rotate(RotationSpeed, 0, RotationSpeed + Time.deltaTime * 10f);

       
    }


}
