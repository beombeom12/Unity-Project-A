using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoWeaponDesert : MonoBehaviour
{

    //FakeShooter

    public GameObject FakeShot;

    //슛하는곳 필요하고
    public GameObject ShooterPosition;
    public GameObject Weapon;


    public void FakeOn()
    {
        FakeShot.SetActive(true);
    }
    public void Fakeoff()
    {
        FakeShot.SetActive(false);
    }



    public void Shoot()
    {
        Instantiate(Weapon, ShooterPosition.transform.position, Quaternion.Euler(90f, 0f, 0f));
    }
}
