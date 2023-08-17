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
    [SerializeField] private float offSetX = 0f;
    [SerializeField] private float offSetY = 0f;

    // Update is called once per frame
    void Update()
    {
        switch (followType)
        {
            case EFollowType.FollowXAxis:
                transform.position = new Vector3(GameMaster.Instance.player.transform.position.x + offSetX, transform.position.y + offSetY, transform.position.z);
                break;
            case EFollowType.FollowYAxis:
                transform.position = new Vector3(transform.position.x + offSetX, GameMaster.Instance.highestY + offSetY, transform.position.z);
                break;
            case EFollowType.FollowBothAxis:
                transform.position = new Vector3(GameMaster.Instance.player.transform.position.x + offSetX, GameMaster.Instance.highestY + offSetY, transform.position.z);
                break;
        }
    }
}
