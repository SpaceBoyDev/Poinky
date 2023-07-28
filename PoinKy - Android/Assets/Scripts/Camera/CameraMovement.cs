using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float offsetZ = -10;
    public float offsetY = 0;
    public float speed;


    public void Update()
    {
        FollowPlayer();
    }

    /// <summary>
    /// FollowPlayer makes the camera smoothly follow the player
    /// </summary>
    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(0, GameMaster.instance.highestY + offsetY,offsetZ), 
            speed * Time.deltaTime);
    }

}
