using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RouletteMgr : MonoBehaviour
{

    //Instance 

    public static RouletteMgr Instance
    {
        get
        {
            if (RouletteInstance == null)
            {
                RouletteInstance = FindObjectOfType<RouletteMgr>();
                if (RouletteInstance == null)
                {
                    var InstanceContainer = new GameObject("RouletteMgr");
                    RouletteInstance = InstanceContainer.AddComponent<RouletteMgr>();
                }
            }
            return RouletteInstance;
        }
    }

    public GameObject RouletteRight;
    public GameObject RouletteBackGround;
    public GameObject RoulettePenal;
    public GameObject JoyStickPanel;
    public GameObject JoyStickBg;


    public GameObject Gold1;
    
    public GameObject Player;

    public Transform Pointer;

    public Image[] ItemImage;
    public Sprite[] SkillupSprite;



    private int itemCount = 6;

    List<int> RouletteStartList = new List<int>();


    private static RouletteMgr RouletteInstance;


    public bool bIsPause = false;

    void Start()
    {

        //리스트에 각각 룰렛에 있는것을 추가하고
        for (int i = 0; i < itemCount; i++)
        {
            RouletteStartList.Add(i);
        }
        for (int i = 0; i < itemCount; i++)
        {
            int iRandomIndex = Random.Range(0, RouletteStartList.Count);

           
            ItemImage[i].sprite = SkillupSprite[RouletteStartList[iRandomIndex]];

            RouletteStartList.RemoveAt(iRandomIndex);


        }






        //if(PlayerMove.Instance.transform.CompareTag("Angel"))
        //{



     
    }
    

    public void Letgo()
    {
        //StartCoroutine(RouletteGo());
    }



    //public IEnumerator RouletteGo()
    //{


    //    yield return new WaitForSeconds(2f);

    //    float fSpeed = Random.Range(1f, 5f);
    //    float fRotateSpeed = 100f * fSpeed;



    //    while (true)
    //    {

    //        yield return null;
    //        if (fRotateSpeed <= 0.01f)
    //        {

    //            break;


    //        }
    //        fRotateSpeed = Mathf.Lerp(fRotateSpeed, 0, Time.deltaTime * 2f);
    //        RouletteBackGround.transform.Rotate(0, 0, fRotateSpeed);



    //    }

    //    yield return new WaitForSeconds(1.5f);

    //    SetResult();

    //    yield return new WaitForSeconds(0.5f);

    //    RouletteRight.SetActive(false);
    //    RoulettePenal.SetActive(false);
    //    RouletteBackGround.SetActive(false);



    //    JoyStickPanel.SetActive(true);
    //    JoyStickBg.SetActive(true);


    //}







    public void SetResult()
    {
        int iClosingIndex = -1;

        float fCloseDistance = 500f;

        float fCurrentDistance = 0f;


        for (int i = 0; i < itemCount; i++)
        {
            fCurrentDistance = Vector2.Distance(ItemImage[i].transform.position, Pointer.position);
            if (fCloseDistance > fCurrentDistance)
            {
                fCloseDistance = fCurrentDistance;
                iClosingIndex = i;
            }



        }

        Debug.Log("ClosetIndex : " + iClosingIndex);


        if (iClosingIndex == -1)
        {
            Debug.Log("What the fuck!");


        }


        SoundManager.Instance.GameUISound("RouletteResult", SoundManager.Instance.bgList[11]);
        if (ItemImage[iClosingIndex].sprite == ItemImage[0].sprite)
        {
            MenuData.Instance.PlayerGetShooter(200f);
            Instantiate(Gold1, transform.position, transform.rotation);
        }
        else if (ItemImage[iClosingIndex].sprite == ItemImage[1].sprite)
        {
            MenuData.Instance.PlayerGetShooter(600f);
            Instantiate(Gold1, transform.position, transform.rotation);
        }
        else if (ItemImage[iClosingIndex].sprite == ItemImage[2].sprite)
        {
            MenuData.Instance.PlayerGetShooter(400f);
            Instantiate(Gold1, transform.position, transform.rotation);
        }
        else if (ItemImage[iClosingIndex].sprite == ItemImage[3].sprite)
        {
            MenuData.Instance.PlayerGetShooter(200f);
            Instantiate(Gold1, transform.position, transform.rotation);
        }
        else if (ItemImage[iClosingIndex].sprite == ItemImage[4].sprite)
        {
            MenuData.Instance.PlayerGetShooter(600f);
            Instantiate(Gold1, transform.position, transform.rotation);
        }
        else if (ItemImage[iClosingIndex].sprite == ItemImage[5].sprite)
        {
            MenuData.Instance.PlayerGetShooter(400f);
            Instantiate(Gold1, transform.position, transform.rotation);
        }


        SoundManager.Instance.GameUISound("RouletteEnd", SoundManager.Instance.bgList[25]);

    }



}
