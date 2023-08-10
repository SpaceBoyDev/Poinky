using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollow : MonoBehaviour
{
    /// <summary>
    /// Script for the wall to follow the player on the Y axis
    /// </summary>
    void Update()
    {
        transform.position = new Vector3(transform.position.x, GameMaster.Instance.highestY, transform.position.z);
    }
}
