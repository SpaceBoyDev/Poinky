using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script has everything called after the rats because, at the start, the powerups
/// were supposed to be rats, however, I changed the sprite to a bird because it fit better.
/// </summary>
public class Rat : MonoBehaviour
{
    [SerializeField]
    private bool isMoving = false;

    private float ratmaxX;
    private float ratminX;
    private float ratmaxY;
    private float ratminY;

    private float ratSpeed;

    private int ratDirectionY = 1;
    private int ratDirectionX = 1;

    /// <summary>
    /// Resets the isMoving bool to false so that all the properties are properly reseted
    /// </summary>
    private void OnDisable()
    {
        isMoving= false;
    }

    private void Update()
    {
        if(isMoving)
        {
            Move();
        }

        if(transform.position.y < GameMaster.Instance.get_cameraLimits().Bottom.position.y)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Function made to be accesible from other scipts, so that rat generator can fill the information
    /// </summary>
    public void Info(float maxX, float minX, float maxY, float minY, float speed, int directionY, int directionX, bool move)
    {
        isMoving = move;

        ratmaxX= maxX;
        ratminX= minX;
        ratmaxY= maxY;
        ratminY= minY;
        ratSpeed= speed;
        ratDirectionY= 1;        
        ratDirectionX= 1;

        if (ratmaxX - ratminX <= 1f && ratSpeed > 1f)
        {
            ratDirectionX = 0;
        }

        if (ratmaxY - ratminY <= 1f && ratSpeed > 1f)
        {
            ratDirectionY = 0;
        }
    }

    /// <summary>
    /// The behaviour of the bird when moving. If the bird goes beyond its max point on Y or X, the direction of the individual axis will change.
    /// </summary>
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(ratDirectionX, ratDirectionY, 0), ratSpeed * Time.deltaTime);

        if (transform.position.y > ratmaxY || transform.position.y < ratminY)
        {
          
                ratDirectionY = ratDirectionY * -1;
            
        }

        if (transform.position.x > ratmaxX || transform.position.x < ratminX)
        {
           
                ratDirectionX = ratDirectionX * -1;

            
        }

        if (transform.position.x > ratmaxX +0.1)
        {
            ratDirectionX = 0;
        }

        //This flips the sprite on the X axis so that it matches the movement
        if (ratDirectionX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    /// <summary>
    /// When colliding with something, it will add a jump to the player and disable itself.
    /// It is only possible for the bird to collide with the player.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
            GameMaster.Instance.JumpUpdate(1);
            gameObject.SetActive(false);
    }

}
