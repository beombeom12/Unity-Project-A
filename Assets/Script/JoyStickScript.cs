using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoyStickScript : MonoBehaviour
{

    public static JoyStickScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JoyStickScript>();
                if (instance == null)
                {
                    var InstanceContainer = new GameObject("JoyStickScript");
                    instance = InstanceContainer.AddComponent<JoyStickScript>();
                }
            }
            return instance;
        }
    }



    private static JoyStickScript instance;

    public Transform player;

    public GameObject smallStick;
    public GameObject bGStick;
    Vector3 stickFirstPosition;

    public Vector3 joyVec;

    float stickRadius;

    public float fDelayTime = 10f;


    public bool isPlayerMoving = false;




    // Start is called before the first frame update
    void Start()
    {
        stickRadius = bGStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2f;


    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PointDown()
    {
        bGStick.transform.position = Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirstPosition = Input.mousePosition;

        if (!PlayerMove.Instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            PlayerMove.Instance.anim.SetBool("Run", true);
            PlayerMove.Instance.anim.SetBool("Idle", false);
            PlayerMove.Instance.anim.SetBool("Attack", false);

        }







        isPlayerMoving = true;
        PlayerTargeting.Instance.bIsTargetIng = false;
          

       
    }

    public void Drag(BaseEventData baseEvenetData)
    {

        PointerEventData pointerEventData = baseEvenetData as PointerEventData;

        Vector3 DragPosition = pointerEventData.position;
        joyVec = (DragPosition - stickFirstPosition).normalized;

        float stickDistance = Vector3.Distance(DragPosition, stickFirstPosition);
      

        if (stickDistance < stickRadius)
        {
            smallStick.transform.position = stickFirstPosition + joyVec *stickDistance;
        }
        else
        {
            smallStick.transform.position = stickFirstPosition + joyVec * stickRadius;
        }


        


    }




    public void Drop()
    {
  
        joyVec = Vector3.zero;
        //bGStick.transform.position = stickFirstPosition;
        smallStick.transform.position = stickFirstPosition;

        stickFirstPosition = bGStick.transform.position;
        //stickFirstPosition = smallStick.transform.position;


        if (!PlayerMove.Instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            PlayerMove.Instance.anim.SetBool("Run", false);
            PlayerMove.Instance.anim.SetBool("Idle", true);
            PlayerMove.Instance.anim.SetBool("Attack", false);

        }
        isPlayerMoving = false;






    }


}
