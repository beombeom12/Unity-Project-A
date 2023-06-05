using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    LeftRing,
    RightRing,
    RightPet,
    LeftPet

}

public enum WeaponType
{
    AncientBow,
    Bow,
    Sickle,
    AncientSickle,
    AncientSword,
    Sword,
    Commendation,
    AncientCommendation,
}

public enum ArmorType
{ 
    Armor, 
    Rob,
    Doctorst,
    AncientRob
}

public enum RightRingType
{
    BearMetalRing,
    BearSilverRing,
    WolfMetalRing,
    WolfSilverRing
}


public enum LeftRingRype
{
    EagleMetalRing,
    EagleSilverRing,
    SnakeMetalRing,
    SnakeSilverRing
}


public enum RightPetType
{
    Angel,
    Reaper,
}

public enum LeftPetType
{
    Ghost,
    Bat
}

[System.Serializable]
public class Item
{


    
    public string itemName;

    public Sprite itemIcon;
   //������ȣ
    public int itemID;
    //������ ���
    public string ItemGrade;
    //����
    public int quantity;
    //�ִ� ����
    public int Maxquantity;
    
    //�ο�����
    public string Description;

    //���� �ο�����
    public string SubDescription;

    //������ ������
    public float fDamage;

    //�������ҽ� ���׷��̵�� ������
    public float fLevelUpPlusDamage;

    //���� HP
    public float fHp;
    public float fMaxHp;

    //������ �ö󰥽� ������ �÷��� Hp
    public float fLevelUpPlusHp;
    public float fLevelUpPlusMaxHp;

    //Plus Option

    //��ư�� ������ �ʱ�ȭ ��Ű�� ���ؼ� ���⼭ �߻���Ű�� fShooterLevelUp
    public float fLevelShootButton;


    public ItemType itemType;





    public Item(string name, Sprite icon, int id, string _ItemGrade, int quantity, int MaxQuantity, string _Description, string _SubDescription,
        float _fDamage, float _fLevelUpPlusDamage, float _fHp, float _fMaxHp, float _fLevelUpPlusHp, float _fLevelUpMaxHp,  float _fLevelShootButton, ItemType type)
    {
        itemName = name;
        itemIcon = icon;
        itemID = id;
        ItemGrade = _ItemGrade;
        this.quantity = quantity;
        this.Maxquantity = MaxQuantity;
        Description = _Description;
        SubDescription = _SubDescription;
        fDamage = _fDamage;
        fLevelUpPlusDamage = _fLevelUpPlusDamage;
        fHp = _fHp;
        fMaxHp = _fMaxHp;
        fLevelUpPlusDamage = _fLevelUpPlusHp;
        fLevelUpPlusMaxHp = _fLevelUpMaxHp;
        fLevelShootButton = _fLevelShootButton;
        itemType = type;
    }
}

[System.Serializable]
public class WeaponItem : Item
{
    public float damage;
    //public float LevelUpPlusDamage;
    public float Hp;
    public float MaxHp;
    public float LevelUpPlusHp;
    public float LevelUpPlusMaxHp;
    public WeaponType weaponType;

    public WeaponItem(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Description, string SubDescription, 
        float damage,  float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpMaxHp, float fLevelShootButton, WeaponType weaponType)
        : base(name, icon, id, ItemGrade, quantity, Maxquantity, Description, SubDescription, damage, LevelUpPlusDamage, fHp, fMaxHp, fLevelUpPlusHp, fLevelUpMaxHp, fLevelShootButton, ItemType.Weapon)
    {
        this.damage = damage;
       // this.LevelUpPlusDamage = LevelUpPlusDamage;
        this.Hp = fHp;
        this.MaxHp = fMaxHp;
        this.LevelUpPlusHp = fLevelUpPlusHp;
        this.LevelUpPlusMaxHp = fLevelUpPlusMaxHp;
        this.weaponType = weaponType;
    }
}


[System.Serializable]

public class ArmorItem : Item
{
    public float damage;
    public float SubDamage;
    public float Hp;
    public float MaxHp;
    public float LevelUpPlusHp;
    public float LevelUpPlusMaxHp;
    public ArmorType armorType;
    public ArmorItem(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Description, string SubDescription,
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlusMaxHp, float fLevelShootButton, ArmorType armortype)
    : base(name, icon, id, ItemGrade, quantity, Maxquantity, Description, SubDescription, damage, LevelUpPlusDamage,  fHp, fMaxHp, fLevelUpPlusHp, fLevelUpPlusMaxHp, fLevelShootButton, ItemType.Armor)
    {
        this.damage = damage;
        this.SubDamage = LevelUpPlusDamage;
        this.Hp = fHp;
        this.MaxHp = fMaxHp;
        this.LevelUpPlusHp = fLevelUpPlusHp;
        this.LevelUpPlusMaxHp = fLevelUpPlusMaxHp;
        this.armorType = armortype;
    }

}


[System.Serializable]
public class RightRingItem : Item
{
    public float damage;
    public float SubDamage;
    public float Hp;
    public float MaxHp;
    public float LevelUpPlusHp;
    public float LevelUpPlusMaxHp;
    public RightRingType rightringType;
    public RightRingItem(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Description, string SubDescription, 
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlustMaxHp, float fLevelShootButton, RightRingType rightringType)
    : base(name, icon, id, ItemGrade, quantity, Maxquantity, Description, SubDescription, damage, LevelUpPlusDamage, fHp, fMaxHp, fLevelUpPlusHp, fLevelUpPlustMaxHp, fLevelShootButton, ItemType.RightRing)
    {
        this.damage = damage;
        this.SubDamage = LevelUpPlusDamage;
        this.Hp = fHp;
        this.MaxHp = fMaxHp;
        this.LevelUpPlusHp = fLevelUpPlusHp;
        this.LevelUpPlusMaxHp = fLevelUpPlustMaxHp;
        this.rightringType = rightringType;
    }

}

[System.Serializable]
public class LeftRingItem : Item
{
    public float damage;
    public float SubDamage;
    public float Hp;
    public float MaxHp;
    public float LevelUpPlusHp;
    public float LevelUpPlusMaxHp;
    public LeftRingRype leftringType;
    public LeftRingItem(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Desciption, string SubDescription,
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlusMaxHp, float fLevelShootButton, LeftRingRype leftringType)
    : base(name, icon, id, ItemGrade, quantity, Maxquantity, Desciption, SubDescription, damage, LevelUpPlusDamage, fHp, fMaxHp, fLevelUpPlusHp, fLevelUpPlusMaxHp, fLevelShootButton, ItemType.LeftRing)
    {
        this.damage = damage;
        this.SubDamage = LevelUpPlusDamage;
        this.Hp = fHp;
        this.MaxHp = fMaxHp;
        this.LevelUpPlusHp = fLevelUpPlusHp;
        this.LevelUpPlusMaxHp = fLevelUpPlusMaxHp;
        this.leftringType = leftringType;
    }

}



[System.Serializable]
public class RightPet : Item
{
    public float damage;
    public float SubDamage;
    public float Hp;
    public float MaxHp;
    public float LevelUpPlusHp;
    public float LevelUpPlusMaxHp;
    public RightPetType rightPet;
    public RightPet(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Desciption, string SubDescription, 
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlusMaxHp, float fLevelShootButton, RightPetType Rightpet)
    : base(name, icon, id, ItemGrade, quantity, Maxquantity, Desciption, SubDescription, damage, LevelUpPlusDamage, fHp, fMaxHp, fLevelUpPlusHp, fLevelUpPlusMaxHp, fLevelShootButton, ItemType.RightPet)
    {
        this.damage = damage;
        this.SubDamage = LevelUpPlusDamage;
        this.Hp = fHp;
        this.MaxHp = fMaxHp;
        this.LevelUpPlusHp = fLevelUpPlusHp;
        this.LevelUpPlusMaxHp = fLevelUpPlusMaxHp;
        this.rightPet = Rightpet;
    }

}



[System.Serializable]
public class LeftPet : Item
{
    public float damage;
    public float SubDamage;
    public float Hp;
    public float MaxHp;
    public float LevelUpPlusHp;
    public float LevelUpPlusMaxHp;
    public LeftPetType leftPetType;
    public LeftPet(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Desciption, string SubDescription,
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlusMaxHp,  float fLevelShootButton, LeftPetType leftPettype)
    : base(name, icon, id, ItemGrade, quantity, Maxquantity, Desciption, SubDescription, damage, LevelUpPlusDamage, fHp, fMaxHp, fLevelUpPlusHp, fLevelUpPlusMaxHp, fLevelShootButton, ItemType.LeftPet)
    {
        this.damage = damage;
        this.SubDamage = LevelUpPlusDamage;
        this.Hp = fHp;
        this.MaxHp = fMaxHp;
        this.LevelUpPlusHp = fLevelUpPlusHp;
        this.LevelUpPlusMaxHp = fLevelUpPlusMaxHp;
        this.leftPetType = leftPettype;
    }

}



public class ItemDatabase : MonoBehaviour
{



    public static ItemDatabase Instance
    {
        get
        {
            if (ItemDataInstance == null)
            {
                ItemDataInstance = FindObjectOfType<ItemDatabase>();
                if (ItemDataInstance == null)
                {
                    var InstanceContainer = new GameObject("ItemDatabase");
                    ItemDataInstance = InstanceContainer.AddComponent<ItemDatabase>();
                }
            }
            return ItemDataInstance;
        }
    }

    public static ItemDatabase ItemDataInstance;

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (ItemDataInstance == null)
        {
            ItemDataInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

      
    }

    //����
    public WeaponItem CreateWeaponItem(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Description, string SubDescription, 
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlusMaxHp, float fLevelShootButton, WeaponType weaponType)
    {
        WeaponItem weaponItem = new WeaponItem(name, icon, id, ItemGrade, quantity, Maxquantity, Description, SubDescription, damage, LevelUpPlusDamage, fHp, fMaxHp,fLevelUpPlusHp, fLevelUpPlusMaxHp, fLevelShootButton, weaponType);
        items.Add(weaponItem);

        return weaponItem;
    }

    //����
    public ArmorItem CreateArmorItem(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Description, string SubDescription, 
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlusMaxHp, float fLevelShootButton, ArmorType ArmorType)
    {

        ArmorItem armorItem = new ArmorItem(name, icon, id, ItemGrade, quantity, Maxquantity,Description, SubDescription, damage,LevelUpPlusDamage, fHp, fMaxHp, fLevelUpPlusHp, fLevelUpPlusMaxHp, fLevelShootButton, ArmorType);
        items.Add(armorItem);

        return armorItem;
    }

    //�����ʹ���
    public RightRingItem CreateRightRingItem(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Description, string SubDescription,
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlusMaxHp, float fLevelShootButton , RightRingType rightringType)
    {

        RightRingItem rightringItem = new RightRingItem(name, icon, id, ItemGrade, quantity, Maxquantity,Description, SubDescription,  damage, LevelUpPlusDamage, fHp, fMaxHp,fLevelUpPlusHp,fLevelUpPlusMaxHp,fLevelShootButton, rightringType);
        items.Add(rightringItem);

        return rightringItem;
    }

    //���ʹ���
    public LeftRingItem CreateLeftRingItem(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Description, string SubDescription,
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlusMaxHp, float fLevelShootButton,LeftRingRype leftringType)
    {

        LeftRingItem leftringItem = new LeftRingItem(name, icon, id,ItemGrade, quantity, Maxquantity, Description,SubDescription, damage,LevelUpPlusDamage, fHp, fMaxHp, fLevelUpPlusHp, fLevelUpPlusMaxHp, fLevelShootButton, leftringType);
        items.Add(leftringItem);

        return leftringItem;
    }

    public RightPet CreateRightPetItem(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Description, string SubDescription, 
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlusMaxHp, float fLevelShootButton, RightPetType RightPettype)
    {

        RightPet rightpetType = new RightPet(name, icon, id, ItemGrade, quantity, Maxquantity, Description, SubDescription, damage, LevelUpPlusDamage, fHp, fMaxHp, fLevelUpPlusHp, fLevelUpPlusMaxHp, fLevelShootButton, RightPettype);
        items.Add(rightpetType);

        return rightpetType;
    }


    public LeftPet CreateLeftPetItem(string name, Sprite icon, int id, string ItemGrade, int quantity, int Maxquantity, string Description, string SubDescription,
        float damage, float LevelUpPlusDamage, float fHp, float fMaxHp, float fLevelUpPlusHp, float fLevelUpPlusMaxHp,  float fLevelShootButton, LeftPetType LeftPettype)
    {

        LeftPet LeftPetItem = new LeftPet(name, icon, id, ItemGrade, quantity, Maxquantity, Description, SubDescription, damage, LevelUpPlusDamage, fHp, fMaxHp, fLevelUpPlusHp, fLevelUpPlusMaxHp, fLevelShootButton, LeftPettype);
        items.Add(LeftPetItem);

        return LeftPetItem;
    }
}