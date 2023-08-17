using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script has everything called after the rats because, at the start, the powerups
/// were supposed to be rats, however, I changed the sprite to a bird because it fit better.
/// </summary>
public class Rat : MonoBehaviour
{
    [SerializeField] private SpriteRenderer birdSprite;
    
    [SerializeField]
    private bool isMoving = false;

    [SerializeField] private bool canChangeDirection = true;

    [SerializeField] private float ratminX;
    [SerializeField] private float ratmaxY;
    [SerializeField] private float ratminY;
    [SerializeField] private float ratmaxX;

    [SerializeField] private float ratSpeed;

    private int ratDirectionY = 1;
    private int ratDirectionX = 1;

    [SerializeField] private float lerpX;
    [SerializeField] private float lerpY;

    private Vector3 actualPos;

    /// <summary>
    /// Resets the isMoving bool to false so that all the properties are properly reseted
    /// </summary>
    private void OnDisable()
    {
        isMoving= false;
        lerpX = 0;
        lerpY = 0;
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

        if (Mathf.Abs(ratmaxX - ratminX) <= 1f && ratSpeed > 1f)
        {
            ratDirectionX = 0;
        }

        if (Mathf.Abs(ratmaxY - ratminY) <= 1f && ratSpeed > 1f)
        {
            ratDirectionY = 0;
        }
        
    }

    /// <summary>
    /// The behaviour of the bird when moving. If the bird goes beyond its max point on Y or X, the direction of the individual axis will change.
    /// </summary>
    public void Move()
    {
        lerpX += ratSpeed * Time.deltaTime * ratDirectionX;
        lerpY += ratSpeed * Time.deltaTime * ratDirectionY;

        actualPos.x = Mathf.Lerp(ratminX, ratmaxX, lerpX);
        actualPos.y = Mathf.Lerp(ratminY, ratmaxY, lerpY);
        actualPos.z = transform.position.z;

        transform.position = actualPos;

        if (transform.position.y > ratmaxY || transform.position.y < ratminY)
        {
            ratDirectionY *= -1;
        }

        if (lerpX <= 0)
        {
            ratDirectionX = 1;
        }
        else if (lerpX >= 1)
        {
            ratDirectionX = -1;
        }
        
        if (lerpY <= 0)
        {
            ratDirectionY = 1;
        }
        else if (lerpY >= 1)
        {
            ratDirectionY = -1;
        }

        birdSprite.flipX = ratDirectionX > 0;
        
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

    private IEnumerator CooldownChangeDirection()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        canChangeDirection = true;
    }

}
