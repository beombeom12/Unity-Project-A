using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelUpText : MonoBehaviour
{
    public static LevelUpText Instance
    {
        get
        {
            if (LevelUpTextInstance == null)
            {
                LevelUpTextInstance = FindObjectOfType<LevelUpText>();
                if (LevelUpTextInstance == null)
                {
                    var InstanceContainer = new GameObject("LevelUpText");
                    LevelUpTextInstance = InstanceContainer.AddComponent<LevelUpText>();
                }
            }
            return LevelUpTextInstance;
        }
    }
    private static LevelUpText LevelUpTextInstance;



    public TextMesh LevelText;
     GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 2f);
    }


    public void ScreenLookLevelUp()
    {
        LevelText.text = "LevelUp";
    }
}
