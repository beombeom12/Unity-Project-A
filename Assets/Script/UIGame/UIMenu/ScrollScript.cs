using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScrollScript : ScrollRect
{
    //부모에게 전달할 
    bool forparent;
     NestedScrollManager NestScroll;
    ScrollRect parentScrollRect;

    protected override void Start()
    {
        NestScroll = GameObject.FindWithTag("NestedScroll").GetComponent<NestedScrollManager>();
        parentScrollRect = GameObject.FindWithTag("NestedScroll").GetComponent<ScrollRect>();
    }


    public override void OnBeginDrag(PointerEventData eventData)
    {

        forparent = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);



        if (forparent)
        {
            NestScroll.OnBeginDrag(eventData);
            parentScrollRect.OnBeginDrag(eventData);
        }
        else
        {
            base.OnBeginDrag(eventData);

        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (forparent)
        {
            NestScroll.OnDrag(eventData);
            parentScrollRect.OnDrag(eventData);
        }
        else
        {
            base.OnDrag(eventData);

        }

    }


    public override void OnEndDrag(PointerEventData eventData)
    {
        if (forparent)
        {
            NestScroll.OnEndDrag(eventData);
            parentScrollRect.OnEndDrag(eventData);
        }
        else
        {
            base.OnEndDrag(eventData);

        }
    }
}
