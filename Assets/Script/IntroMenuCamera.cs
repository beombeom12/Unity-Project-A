using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroMenuCamera : MonoBehaviour
{
    public static IntroMenuCamera Instance
    {
        get
        {
            if (IntroMenuCaInstance == null)
            {
                IntroMenuCaInstance = FindObjectOfType<IntroMenuCamera>();
                if (IntroMenuCaInstance == null)
                {
                    var InstanceContainer = new GameObject("IntroMenuCamera");
                    IntroMenuCaInstance = InstanceContainer.AddComponent<IntroMenuCamera>();
                }
            }
            return IntroMenuCaInstance;
        }
    }

    private static IntroMenuCamera IntroMenuCaInstance;


    public void Awake()
    {
        if (IntroMenuCaInstance == null)
        {
            IntroMenuCaInstance = this;
            DontDestroyOnLoad(IntroMenuCaInstance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
