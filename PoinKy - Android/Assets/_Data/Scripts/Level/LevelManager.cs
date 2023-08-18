using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float offSetY;
    [SerializeField] private GameObject skyLevel;

    private float lastPosY = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnSky();
    }

    private void SpawnSky()
    {
        if (GameMaster.Instance.player.transform.position.y > lastPosY)
        {
            int numberInPool = PoolManager.instance.SearchPool(skyLevel);

            GameObject level = PoolManager.instance.GetPooledObject(numberInPool);

            lastPosY += offSetY;

            if (level != null)
            {
                level.transform.position = new Vector3(level.transform.position.x, lastPosY, level.transform.position.z);
                level.SetActive(true);
            }
        }
    }

    public void ResetLevelManager()
    {
        lastPosY = 0;
    }
}
