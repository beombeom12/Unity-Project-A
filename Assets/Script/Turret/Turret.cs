using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject ShooterPosition;

    public GameObject Shooter;
    public GameObject Shooter1;
    public GameObject Shooter2;
    public GameObject Shooter3;


    public GameObject FakeArrow;

    public void TurretShoot()
    {
        Instantiate(Shooter, ShooterPosition.transform.position, Quaternion.Euler(90f, 0f, 0f));
    }



    public void TurretShoot1()
    {
        Instantiate(Shooter1, ShooterPosition.transform.position, Quaternion.Euler(90f, 90f, 0f));
    }

    public void TurretShoot3()
    {
        Instantiate(Shooter2, ShooterPosition.transform.position, Quaternion.Euler(-90f, 0f, 0f));


    }

    public void TurretShoot4()
    {
        Instantiate(Shooter3, ShooterPosition.transform.position, Quaternion.Euler(-90f, 90f, 0f));
    }



    public void FakeArrowOn()
    {
        FakeArrow.SetActive(true);
    }

    public void FakeArrowOff()
    {
        FakeArrow.SetActive(false);
    }
}
