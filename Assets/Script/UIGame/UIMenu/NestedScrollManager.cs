using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class NestedScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    const int Size = 4;
    float[] pos = new float[Size];
    float fDistance;//포스들같의 간격
    float fTargetPos, fCurrentPos;
    public Scrollbar scorllbar;
    bool bIsDrag;

    public Transform ContextTr;
    public RectTransform[] BtnRect, BtnImageRect;

    public Slider tabSlider;
    int iTargetIndex;

    public AudioClip clip;

    void Start()
    {
        fDistance = 1f / (Size -  1);
        for(int i = 0; i < Size; i++)
        {
            pos[i] = fDistance * i;
        }
    }

    float SetPos()
    {
        for (int i = 0; i < Size; i++)
            if (scorllbar.value < pos[i] + fDistance * 0.5f && scorllbar.value > pos[i] - fDistance * 0.5f)
            {
                iTargetIndex = i;
                return pos[i];
            }
        return 0;
    }




    // Update is called once per frame
    public void OnBeginDrag(PointerEventData eventData) => fCurrentPos = SetPos();

    public void OnDrag(PointerEventData eventData) => bIsDrag = true;

    public void OnEndDrag(PointerEventData eventData)
    {
        bIsDrag = false;
        fTargetPos = SetPos();


        if(fCurrentPos == fTargetPos)
        {
            if(eventData.delta.x > 18 && fCurrentPos - fDistance >= 0)
            {
                --iTargetIndex;
                fTargetPos = fCurrentPos - fDistance;
            }
            else if(eventData.delta.x < -18 && fCurrentPos + fDistance <= 1.01f)
            {
                ++iTargetIndex;
                fTargetPos = fCurrentPos + fDistance;
            }


        }


            for(int i = 0; i < Size; i++)
            if(ContextTr.GetChild(i).GetComponent<ScrollScript>() && fCurrentPos != pos[i] && fTargetPos == pos[i])
            {
                ContextTr.GetChild(i).GetChild(1).GetComponent<Scrollbar>().value = 1;
            }

    }

    // Start is called before the first frame update
    void Update()
    {

        tabSlider.value = scorllbar.value;

        if (!bIsDrag)
        {
            scorllbar.value = Mathf.Lerp(scorllbar.value, fTargetPos, 0.1f);


            for (int i = 0; i < Size; i++)
            {
                BtnRect[i].sizeDelta = new Vector2(i == iTargetIndex ? 360 : 180, BtnRect[i].sizeDelta.y);
            }
        }

        if (Time.time < 0.1f)
            return;

        for(int i = 0; i < Size; i++)
        {
            Vector3 BtnTargetPos = BtnRect[i].anchoredPosition3D;
            Vector3 BtnTargetScale = new Vector3(0.8f, 0.8f, 1f);
            bool textActive = false;
            if(i == iTargetIndex)
            {
                BtnTargetPos.y = -23f;
                BtnTargetScale = new Vector3(1f, 1f, 1f);
                textActive = true;
            }





            BtnImageRect[i].anchoredPosition3D = Vector3.Lerp(BtnImageRect[i].anchoredPosition3D, BtnTargetPos, 0.25f);
            BtnImageRect[i].localScale = Vector3.Lerp(BtnImageRect[i].localScale, BtnTargetScale, 0.25f);
            BtnImageRect[i].transform.GetChild(0).gameObject.SetActive(textActive);
        }


    }



    public void TabClick(int n)
    {
        iTargetIndex = n;
        fTargetPos = pos[n];
        SoundManager.Instance.ButtonSound("BottomButtonClick", clip);
    }
}
