using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance
    {
        get
        {
            if (ObstacleInstance == null)
            {
                ObstacleInstance = FindObjectOfType<ObstacleManager>();
                if (ObstacleInstance == null)
                {
                    var InstanceContainer = new GameObject("ObstacleManager");
                    ObstacleInstance = InstanceContainer.AddComponent<ObstacleManager>();
                }
            }
            return ObstacleInstance;
        }
    }
    private static ObstacleManager ObstacleInstance;
    //Wheel�� ���� �����Ѵ�.


    //Throne
    public float fThroneDamage;

    private void Start()
    {
        fThroneDamage = 140f;
    }








}
