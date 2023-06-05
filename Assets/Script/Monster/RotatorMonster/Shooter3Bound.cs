using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter3Bound : MonoBehaviour
{

     GameObject Player;
    RoomChecker isRoomChecker;
    
    LineRenderer lineRender;
    
    public GameObject BoundWeapon;
    public GameObject Shooter3BoundCanvas;

    public Transform ShooterPosition;

    public  LayerMask layerMask;

    public float fShooter3BoundHp = 1500f;
    public float fShooter3BoundMaxHp = 1500f;
    
    public float fWeaponDamage;
    public float fSphereWeaponDamage;
    public float fWeaponCritical = 1.3f;
    public float fCollisionDelay = 2f;

    public bool bLookAtPlayer = true;
    public bool bCollsionDelay = true;

    // Start is called before the first frame update
     void  Start()
    {
        lineRender = GetComponent<LineRenderer>();

        Player = GameObject.FindGameObjectWithTag("Player");

        isRoomChecker = transform.parent.transform.parent.gameObject.GetComponent<RoomChecker>();


        lineRender.startColor =  new Color(1f, 1f, 1f, 0.3f);
        lineRender.endColor = new Color(1f, 0f, 0f, 0.3f);
        lineRender.startWidth = 1f;
        lineRender.endWidth = 1f;

        StartCoroutine(StayWait());
    }


    private void Update()
    {
        if(fShooter3BoundHp <= 0)
        {
            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.iTargetingIndex = -1;

            Destroy(transform.gameObject);
        }
    }

    IEnumerator StayWait()
    {
        yield return null;


        while(!isRoomChecker.bRoomEnterPlayer)
        {
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShootTargetSet());



        while (true)
        {
            yield return new WaitForSeconds(1f);
            LineTargetSet();
            yield return new WaitForSeconds(3f);
            Shoot();
        }
    }

    IEnumerator ShootTargetSet()
    {
        while (true)
        {
            yield return null;

            if (!bLookAtPlayer)
                break;
            transform.LookAt(Player.transform.position);

            LineTargetSet();

        }
    }

    public void LineTargetSet()
    {
        Vector3 vShootPoint = ShooterPosition.position;

        Vector3 vDir = transform.forward;

        lineRender.positionCount = 1;

        lineRender.SetPosition(0, transform.position);

        for(int i =0; i < 4; i++)
        {
            Physics.Raycast(vShootPoint, vDir, out RaycastHit hit, 30f, layerMask);

            lineRender.positionCount++;

            lineRender.SetPosition(i, hit.point);


            vShootPoint = hit.point;
            vDir = Vector3.Reflect(vDir, hit.normal);
        }




    }


    public void FindTargeting()
    {
        bLookAtPlayer = false;

        for(int i =0; i < lineRender.positionCount; i++)
        {
            lineRender.SetPosition(i, Vector3.zero);
        }
        lineRender.positionCount = 0;
    }

    public void Shoot()
    {

        Vector3 RenewalPosition = new Vector3(transform.position.x, transform.position.y * 3f, transform.position.z);
        GameObject DangerLineCopy = Instantiate(BoundWeapon, RenewalPosition, transform.rotation);

    }



    public void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Weapon"))
        {
            fWeaponDamage = other.gameObject.GetComponent<PlayerWeaponShooter>().fDamage;

            Instantiate(EffectScript.Instance.GhostDamageEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));

            GameObject DamageTextClone =
                Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);


            if(Random.value < 0.6)
            {
                fShooter3BoundHp -= fWeaponDamage;
                Shooter3BoundCanvas.GetComponent<Shooter3BoundHp>().Damage(fWeaponDamage);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, false);
            }
            else
            {
                fShooter3BoundHp -= fWeaponDamage * fWeaponCritical;
                Shooter3BoundCanvas.GetComponent<Shooter3BoundHp>().Damage(fWeaponDamage * fWeaponCritical);
                DamageTextClone.GetComponent<DmgText>().ScreenLookDamage(fWeaponDamage, true);

            }
            Destroy(other.gameObject);
        }



        if(other.transform.CompareTag("SphereWeapon") && bCollsionDelay)
        {
            StartCoroutine(DelayCollsionDelay(other));
        }
    }


    IEnumerator DelayCollsionDelay(Collider other)
    {
        bCollsionDelay = false;

        fSphereWeaponDamage = other.gameObject.GetComponent<PlayerChaseAttacking>().fSphereDamage;

        GameObject SphereTextClone =
            Instantiate(EffectScript.Instance.MonsterDamageText, transform.position, Quaternion.identity);
        if(other.gameObject.CompareTag("Weapon"))
        {
            fShooter3BoundHp -= fSphereWeaponDamage;
            Shooter3BoundCanvas.GetComponent<Shooter3BoundHp>().Damage(fSphereWeaponDamage);
            SphereTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, false);

        }
        else
        {

            fShooter3BoundHp -= fSphereWeaponDamage * fWeaponCritical;
            Shooter3BoundCanvas.GetComponent<Shooter3BoundHp>().Damage(fSphereWeaponDamage * fWeaponCritical);
            SphereTextClone.GetComponent<DmgText>().ScreenLookDamage(fSphereWeaponDamage, true);
        }


        yield return new WaitForSeconds(fCollisionDelay);
        bCollsionDelay = true;
    }

}
