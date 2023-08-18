using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    [SerializeField] private float offSetY;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y + offSetY < GameMaster.Instance.get_cameraLimits().Bottom.position.y)
        {
            gameObject.SetActive(false);
        }
    }
}
