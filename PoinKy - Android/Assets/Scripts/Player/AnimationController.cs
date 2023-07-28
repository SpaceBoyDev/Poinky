using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] coolCatSprites;

    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Transform sprite;

    public float rotateSpeed;

    /// <summary>
    /// Gets the Rigidbody2D component
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Animate();
    }

    /// <summary>
    /// Depending on the rigidbody velocity, it changes the sprite and makes it rotate to left or right
    /// </summary>
    private void Animate()
    {
        if(rb.velocity.x > 0)
        {
            sr.flipX = false;
            sr.sprite = coolCatSprites[1];
            sprite.Rotate(Vector3.back * (rotateSpeed * Time.deltaTime));
        }
        else if (rb.velocity.x < 0)
        {
            sr.sprite = coolCatSprites[1];
            sr.flipX= true;
            sprite.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
        }
        else if (rb.velocity.x == 0)
        {
            sr.sprite = coolCatSprites[0];
            sprite.rotation = new Quaternion(0,0,0,0);
        }

    }
}
