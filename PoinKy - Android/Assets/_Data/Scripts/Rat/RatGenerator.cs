using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RatGenerator : MonoBehaviour
{

    [FormerlySerializedAs("Rat")] [SerializeField]
    private GameObject blueBird;
    [SerializeField] private GameObject yellowBird;

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
        int randomBird = Random.Range(1, 3);
        int numberInPool;
        switch (randomBird)
        {
            case 0:
                numberInPool = PoolManager.instance.SearchPool(blueBird);
                break;
            case 1:
                numberInPool = PoolManager.instance.SearchPool(yellowBird);
                break;
            default:
                numberInPool = PoolManager.instance.SearchPool(blueBird);
                break;
        }
        
        GameObject bird = PoolManager.instance.GetPooledObject(numberInPool);

        if (bird != null)
        {
            //Creates new properties
            RatProperties();
            //Assigns them a position based on the properties
            bird.transform.position = new Vector3(Random.Range(minX, maxX), lastPosY, 0f);
            //Enables the bird
            bird.SetActive(true);
            //Sends the bird the info to move
            bird.GetComponent<Rat>().Info(maxX, minX, maxY, minY, speed, directionY, directionX, true);
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
