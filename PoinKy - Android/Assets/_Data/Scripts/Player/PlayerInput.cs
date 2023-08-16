using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    public enum EPlayerState
    {
        OnGround,
        Aiming,
        Launched,
        Falling
    }
    
    [SerializeField] public EPlayerState playerState;
    public EPlayerState GetPlayerState() { return playerState; }
    
    [Header("Player Components")]
    private Rigidbody2D rb;
    private Transform tr;

    [Header("Mouse's Positions")]
    private Vector2 startPos;
    private Vector2 finalPos;

    [Header("Jump Properties")]
    private Vector2 jumpDirection;
    private float power = 10f;
    RaycastHit2D rayHit;
    [SerializeField] private bool isJumping = false;
    [SerializeField]
    LayerMask Floor;
    public Vector2 minPower;
    public Vector2 maxPower;
    public bool rayDetect = true;

    GameObject clon;

    [SerializeField]
    private GameObject jumpIndicator;

    [SerializeField]
    private LineController lineController;

    private void Start()
    {
        clon = Instantiate(jumpIndicator);
        clon.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        //It only calculates the jump if the number of jumps is more than 0
        if (GameMaster.Instance.numberOfJumps > 0)
        {
            CalculateJump();
        }
        //The raycast is enabled when rayDetect is true
        if(rayDetect)
        {
            FloorDetection();
        }
        //Paw when click
        
        UpdatePlayerState();
    }

    private void UpdatePlayerState()
    {
        if (rb.velocity.y == 0 && rayHit)
        {
            playerState = EPlayerState.OnGround;
        }
        else if (isJumping)
        {
            playerState = EPlayerState.Aiming;
        }
        else if (rb.velocity.y > 0)
        {
            playerState = EPlayerState.Launched;
        }
        else if (rb.velocity.y < -1)
        {
            playerState = EPlayerState.Falling;
        }
    }

    /// <summary>
    /// Calculates start, final and current position of the mouse
    /// </summary>
    private void CalculateJump()
    {
        //Gets the mouse's start and final position when you click down and let go the button
        if (Input.GetMouseButtonDown(0) && !isJumping)
        {
            isJumping = true;
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            clon.transform.position = new Vector3(startPos.x - 0.04f, startPos.y + 0.2f);
            clon.SetActive(true);

            //Enables line renderers
            lineController.SetLines(true);

            Time.timeScale = 0.1f;
        }

        //Gets the current position of the mouse
        if (Input.GetMouseButton(0) && isJumping)
        {
            lineController.SetLine(startPos, Camera.main.ScreenToWorldPoint(Input.mousePosition));

            Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), startPos, Color.red);

            Vector2 sliderValue = new Vector2(Mathf.Clamp(startPos.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x,minPower.x, maxPower.x), 
                Mathf.Clamp(startPos.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y, minPower.y, maxPower.y));

            GameMaster.Instance.SliderUpdate(sliderValue.magnitude);

            Mathf.Clamp(startPos.y - finalPos.y, minPower.y, maxPower.y);

            //If the right button is pressed when the player is jumping, it cancels the jump
            if(Input.GetMouseButtonDown(1))
            {
                clon.SetActive(false);
                isJumping = false;
                lineController.SetLines(false);
                Time.timeScale = 1f;
                return;
            }
        }

        //Gets the final position of the mouse and calls de Jump
        if (Input.GetMouseButtonUp(0) && isJumping)
        {
            isJumping = false;
            finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Time.timeScale = 1;

            clon.SetActive(false);

            lineController.SetLines(false);

            rayDetect = false;
            
            Jump();
        }
    }

    /// <summary>
    /// Executes the jump.
    /// </summary>
    private void Jump()
    {
        //Raycast cooldown, disables it when it jumps
        StartCoroutine(JumpCooldown());

        //Sets the rigidbody velocity to zero
        rb.velocity = Vector2.zero;


        //Calculates the force and direction of the jump
        Vector2 force = new Vector2(Mathf.Clamp(startPos.x - finalPos.x, minPower.x, maxPower.x),
            Mathf.Clamp(startPos.y - finalPos.y, minPower.y, maxPower.y));

        rb.AddForce(force * power, ForceMode2D.Impulse);

        //Substracts a jump
        GameMaster.Instance.JumpUpdate(-1);
    }

    /// <summary>
    /// After every jump, the raycast is disabled. This Coroutine waits the desired amount of time to reenable the raycast
    /// </summary>
    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.03f);
        rayDetect = true;
    }

    /// <summary>
    /// Detects the floor with a raycast. It only detects the Floor layer.
    /// </summary>
    private void FloorDetection()
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.blue);

        rayHit = Physics2D.Raycast(transform.position, -Vector2.up, 0.7f, Floor);

        //If the raycast hits the floor, the jump is reseted
        if (rayHit.collider != null)
        {
            GameMaster.Instance.JumpReset();
        }
    }
}
