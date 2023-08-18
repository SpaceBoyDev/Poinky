using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject Rat;

    [Header("Rat Properties")]
    private float speed;
    private float maxY;
    private float minY;
    private float maxX;
    private float minX;
    
    private int directionY = 1;
    private int directionX = 1;

    [SerializeField] private Vector2 maxXRandomRange;
    [SerializeField] private Vector2 minXRandomRange;
    
    [SerializeField] private Vector2 maxYRandomRange;
    [SerializeField] private Vector2 minYRandomRange;
    
    public bool spawn;

    //This variable keeps track of the last Y position a bird was spawned.
    [SerializeField]
    private float lastPosY = 3;

    [SerializeField]
    private float offsetY = 5;

    private void Update()
    {
        if (lastPosY < GameMaster.Instance.get_cameraLimits().Top.position.y + offsetY)
        {
            SpawnRat();
        }
    }

    /// <summary>
    /// Randomly assigns values to each property.
    /// </summary>
    private void RatProperties()
    {
        speed = Random.Range(0.4f, 2f);

        maxY = lastPosY + Random.Range(maxYRandomRange.x, maxYRandomRange.y);
        minY = lastPosY - Random.Range(minYRandomRange.x, minYRandomRange.y);
        maxX = Random.Range(maxXRandomRange.x, maxXRandomRange.y);
        minX = Random.Range(minXRandomRange.x, maxX);
    }

    /// <summary>
    /// Searches for a bird to spawn in the pool and then gets that object.
    /// If the gotten object is NOT null, it will be spawned and its properties assigned.
    /// </summary>
    private void SpawnRat()
    {
        int numberInPool = PoolManager.instance.SearchPool(Rat);

        GameObject rat = PoolManager.instance.GetPooledObject(numberInPool);

        if (rat != null)
        {
            //Creates new properties
            RatProperties();
            //Assigns them a position based on the properties
            rat.transform.position = new Vector3(Random.Range(minX, maxX), lastPosY, 0f);
            //Enables the bird
            rat.SetActive(true);
            //Sends the bird the info to move
            rat.GetComponent<Rat>().Info(maxX, minX, maxY, minY, speed, directionY, directionX, true);
            //Adds 3 so that the next bird will spawn above the last one.
            lastPosY = lastPosY + 3;
        }
    }

    public void ResetGenerator()
    {
        lastPosY = 3;
        offsetY = 5;
    }

}
