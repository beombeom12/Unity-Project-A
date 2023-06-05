using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{

    public static EffectScript Instance
    {
        get
        {
            if (EffectInstance == null)
            {
                EffectInstance = FindObjectOfType<EffectScript>();
                if (EffectInstance == null)
                {
                    var InstanceContainer = new GameObject("EffectScript");
                    EffectInstance = InstanceContainer.AddComponent<EffectScript>();
                }
            }
            return EffectInstance;
        }
    }
    public static EffectScript EffectInstance;


    [Header("Monster")]
    public GameObject MonsterWallHitted;

    //���� ������ �ؽ�Ʈ
    public GameObject MonsterDamageText;
    //���� �׾����� ������ ����Ʈ
    public GameObject MonsterDeadEffect;
    //Ghost
    public GameObject GhostDamageEffect;
    public GameObject GhostHittedEffect;

    //Snake(Blood)
    public GameObject SnakeHittedEffect;

    //Archero
    public GameObject ArcheroHittedEffect;

    //TreasureBox
    public GameObject TreasureBoxHittedEffect;


    //CloseSkeleton
    public GameObject CloseSkeletonEffect;



    //Lizard
    public GameObject LizardWeaponEffect;
    public GameObject LizardHittedEffect;

    //BombGhost
    public GameObject BombGhostHittingEffect;
    public GameObject BombGhostHittedEffect;

    //MagicianSkeleton
    public GameObject MagicianSkeletonHitted;



    //MiniDragon
    public GameObject MiniDragonHitted;
    //Skeleton
    public GameObject RotateSkeletonHitted;
    //SmallGolem
    public GameObject SmallGolemHitted;

    //ThrowTree
    public GameObject ThrowTreeHitted;

    //UpshotMonster
    public GameObject UpShotMonsterHitted;
    public GameObject UpShotShoot;





    public GameObject BossHittedEffect;
    
    
    
    
    //WarriorSkeleton TrailEffect
    public GameObject SwordTrailEffect;
    
    
    
    
    //Warrior,Magician
    public GameObject SkeletonDeadEffect;






    //SmallSkeleton TrailEffect
    public GameObject SmallSkeletonTrailEffect;



    //LastBoss
    public GameObject LastBossTrailEffect;
    public GameObject LastBossTrailEffect2;

    public GameObject LastBossAttacking;

    public GameObject LastBossPutYourHandsUp;

    public GameObject LastBossHittedEffect;


    public GameObject LastBossWallHittedEffect;


    //���� Ÿ���� ���� ���� ���� ���ִ°�
    public GameObject MonsterTargetingEffect;




    [Header("Player")]
    public GameObject PlayerLevelUpEffect;
    public GameObject PlayerLevelUpTextEffect;
    public GameObject PlayerChasingWeaponEffect;
    public GameObject PlayerDeadEffect;
    public GameObject OpenDoorEffect;
    //MagicianSkeleton

    //�浹�Ǿ�����
    public GameObject FireHittedEffect;

    //Ȱ ����Ʈ
    public GameObject BowHittingEffect;

    //���긮�°�
    public GameObject PlayerHittedEffect;

    public GameObject PlayerWalkEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
}
