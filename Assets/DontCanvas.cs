using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DontCanvas : MonoBehaviour
{

    public static DontCanvas Instance
    {
        get
        {
            if (DontCanvasInstance == null)
            {
                DontCanvasInstance = FindObjectOfType<DontCanvas>();
                if (DontCanvasInstance == null)
                {
                    var InstanceContainer = new GameObject("DontCanvas");
                    DontCanvasInstance = InstanceContainer.AddComponent<DontCanvas>();
                }
            }
            return DontCanvasInstance;
        }
    }

    private static DontCanvas DontCanvasInstance;
    // Start is called before the first frame update

    public void Awake()
    {
       if(DontCanvasInstance == null)
        {
            DontCanvasInstance = this;
            DontDestroyOnLoad(DontCanvasInstance);
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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
