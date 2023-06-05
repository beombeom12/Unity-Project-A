using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    GameObject Player;

    private void Start()
    {
        Player = PlayerSkillData.Instance.Player;

        StartCoroutine(FinishRoom());
    }

    IEnumerator FinishRoom()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.3f);
            while(transform.parent.gameObject.GetComponent<RoomChecker>().bMissionClear)
            {
                transform.position = Vector3.Lerp(transform.position, Player.transform.position, 0.3f);
                yield return null;
            }
        }

    }

}
