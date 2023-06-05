using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = PlayerSkillData.Instance.Player;

        StartCoroutine(FinishRoom());
    }

    IEnumerator FinishRoom()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.3f);
            while (transform.parent.gameObject.GetComponent<RoomChecker>().bMissionClear)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + 0.5f, Player.transform.position.z), 0.2f);
                yield return null;
            }

        }
    }


}
