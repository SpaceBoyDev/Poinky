using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    [SerializeField]
    private Sprite[] coolCatSprites; //0-> Idle
                                     //1-> Aiming
                                     //2-> Launched
    
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Transform sprite;
    
    public float rotateSpeed;

    private Tween playerStateTransition;

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

        switch (playerInput.playerState)
        {
            case PlayerInput.EPlayerState.OnGround:
                sr.sprite = coolCatSprites[0];
                sprite.rotation = new Quaternion(0, 0, 0, 0);
                break;
            case PlayerInput.EPlayerState.Aiming:
                sr.sprite = coolCatSprites[1];
                sr.flipX = rb.velocity.x > 0;
                
                if (rb.velocity.x < 0)
                {
                    sprite.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
                }
                else
                { 
                    sprite.Rotate(Vector3.back * (rotateSpeed * Time.deltaTime)); 
                }
                
                break;
            case PlayerInput.EPlayerState.Launched:
                sr.sprite = coolCatSprites[2];
                sprite.rotation = new Quaternion(0, 0, 0, 0);
                sr.flipX = rb.velocity.x > 0;
                break;
            case PlayerInput.EPlayerState.Falling:
                sr.sprite = coolCatSprites[1];
                sr.flipX = rb.velocity.x > 0;
                
                if (rb.velocity.x < 0)
                {
                    sprite.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
                }
                else
                { 
                    sprite.Rotate(Vector3.back * (rotateSpeed * Time.deltaTime)); 
                }
                break;
            default:
                break;
        }
    }

    public void OnStateChange()
    {
        playerStateTransition.Kill();
        sprite.transform.localScale = new Vector3(1.253947f, 1.253947f, 1.253947f);
        playerStateTransition = sprite.DOScale(1.6f, 0.1f).SetEase(Ease.InSine).SetLoops(2, LoopType.Yoyo)
                .SetUpdate(true);
        
        playerStateTransition.Play();
    }
}
