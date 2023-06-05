using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    public GameObject Ghost;
    public Transform GhostHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.transform.CompareTag("Weapon"))
        //{
        //    Ghost.GetComponent<GhostHpBar>().Damage();
        //}

    }
}
