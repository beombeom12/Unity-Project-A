using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{

    public static CameraScript Instance
    {
        get
        {
            if (CameraInstance == null)
            {
                CameraInstance = FindObjectOfType<CameraScript>();
                if (CameraInstance == null)
                {
                    var InstanceContainer = new GameObject("CameraScript");
                    CameraInstance = InstanceContainer.AddComponent<CameraScript>();
                }
            }
            return CameraInstance;
        }
    }




    public static CameraScript CameraInstance;


    public float fPlayerChaseY = 23.34f;
    public float fPlayerChaseZ = -12f;

    public GameObject Player;

    public Vector3 cameraPosition;

    public Image FadeInOutImage;




    public float fShakeAmount = 200f;
    public float fShakeSpeed = 50f;
    public float fShakeTime;



    private void Update()
    {

        cameraPosition.y = Player.transform.position.y + fPlayerChaseY;
        cameraPosition.z = Player.transform.position.z + fPlayerChaseZ;


        transform.position = cameraPosition;
    }




    public void CameraMoveRoom()
    {


        StartCoroutine(FadeInOut());
        cameraPosition.x = Player.transform.position.x;
    }


    IEnumerator FadeInOut()
    {
        float a = 1;
        FadeInOutImage.color = new Vector4(1f, 1f, 1f, a);

        yield return new WaitForSeconds(0.3f);

        while (a >= 0)
        {
            FadeInOutImage.color = new Vector4(1f, 1f, 1f, a);

            a -= 0.02f;

            yield return null;
        }


    }







}

