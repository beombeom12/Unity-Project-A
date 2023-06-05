using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LogoScript : MonoBehaviour
{

    public GameObject LogoPenal;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayTime(3));
    }

    IEnumerator DelayTime(float fTime)
    {
        yield return new WaitForSeconds(fTime);
        LogoPenal.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("IntroMenu");

        


    }


    
}
