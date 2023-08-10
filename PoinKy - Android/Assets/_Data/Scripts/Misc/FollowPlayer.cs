using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public enum EFollowType
    {
        FollowXAxis,
        FollowYAxis,
        FollowBothAxis
    }

    [SerializeField] private EFollowType followType;

    // Update is called once per frame
    void Update()
    {
        switch (followType)
        {
            case EFollowType.FollowXAxis:
                transform.position = new Vector3(GameMaster.Instance.player.transform.position.x, transform.position.y, transform.position.z);
                break;
            case EFollowType.FollowYAxis:
                transform.position = new Vector3(transform.position.x, GameMaster.Instance.highestY, transform.position.z);
                break;
            case EFollowType.FollowBothAxis:
                transform.position = new Vector3(GameMaster.Instance.player.transform.position.x, GameMaster.Instance.highestY, transform.position.z);
                break;
        }
    }
}
