using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    SpriteRenderer sprite;
    Vector2 vDirection;
    public float fMoveSpeed = 0.1f;
    public float minsize = 0.1f;
    public float maxsize = 0.3f;
    public float fSizeSpeed = 1f;
    public Color[] colors;
    public float fColorSpeed = 5f; 
    // Start is called before the first frame update
    void Start()
    {
        vDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        float fSize = Random.Range(minsize, maxsize);
        transform.localScale = new Vector2(fSize, fSize);
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(vDirection * fMoveSpeed);
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * fSizeSpeed);

        Color color = sprite.color;
        color.a = Mathf.Lerp(sprite.color.a, 0, Time.deltaTime * fColorSpeed);
        sprite.color = color;

        if(sprite.color.a <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
