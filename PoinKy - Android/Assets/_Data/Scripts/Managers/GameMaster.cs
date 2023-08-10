using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct cameraLimits
{
    public Transform Top, Bottom, Left, Right;
}

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    [SerializeField]
    private Camera mainCamera;
    
    [SerializeField]
    private cameraLimits cameraLimits;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI textJump;
    public float numberOfJumps = 3;
    [SerializeField]
    private Slider sliderForce;
    [SerializeField]
    private GameObject retryPanel;
    [SerializeField]
    private TextMeshProUGUI textHighestY;
    [SerializeField]
    private TextMeshProUGUI FPSCounter;

    [Header("Player")]
    [SerializeField]
    public GameObject player;
    private bool isAlive = true;
    [SerializeField] private float deathPositionOffset;

    [SerializeField]
    private GameObject mousePaw;

    private GameObject mousePawInstance;
    
    [Header("Camera rotation properties")]
    [SerializeField]
    private Transform cameraTr;
    public float highestY = 0;
    private float rotateSpeed = 15f;
    private float rotateDestinationZ = 0;
    private bool canRotate = false;
    private int cameraCooldown = 10;
    
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;

    /// <summary>
    /// Makes this instance be the only one to exist in scene
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        //Resets the timeScale on Awake. This updates the timeScale after retrying the level.
        Time.timeScale = 1;
    }
    
    /// <summary>
    /// Starts the Coroutine so that the cooldown of the camera's rotation works correctly
    /// </summary>
    private void Start()
    {
        mousePawInstance = Instantiate(mousePaw);
        mousePawInstance.SetActive(false);
        mousePawInstance.transform.SetParent(retryPanel.transform);
        retryPanel.SetActive(false);
        StartCoroutine(CameraCooldown());
    }

    private void Update()
    {
        HighestPosition();
        ShowFPS();
        CheckPlayerStatus();

        if (Input.GetMouseButton(0))
        {
            mousePawInstance.transform.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y + 50);
            mousePawInstance.SetActive(true);
        }
        else
        {
            mousePawInstance.SetActive(false);
        }
    }

    private void ShowFPS()
    {
        if( m_timeCounter < m_refreshTime )
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float)m_frameCounter/m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }

        FPSCounter.text = "FPS: " + m_lastFramerate;
    }
    
    /// <summary>
    /// Function made to be accesible for the button. It loads the desired scene.
    /// Made to reload the scene when hitting the Retry button
    /// </summary>
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Checks if the player is alive or not.
    /// </summary>
    private void CheckPlayerStatus()
    {
        if (player.transform.position.y <= highestY - deathPositionOffset)
        {
            isAlive = false;
            PlayerStatus();
        }
    }

    /// <summary>
    /// Updates the status of the player and the game when the player dies
    /// </summary>
    private void PlayerStatus()
    {
        if (!isAlive)
        {
            //Sets the highest position reached, the panel and the buttons to be displayed
            textHighestY.text = "Score: " + highestY;
            player.SetActive(false);
            retryPanel.SetActive(true);
            //Stops time
            Time.timeScale = 0;
        }
    }
    /// <summary>
    /// Updates the Slider value to display the force. For debug purposes.
    /// </summary>
    public void SliderUpdate(float value)
    {
        sliderForce.value = value;
    }

    /// <summary>
    /// Updates the numberOfJumps value.
    /// Made to be easily updated from another scripts.
    /// </summary>
    public void JumpUpdate(int quantity)
    {
        numberOfJumps = numberOfJumps + quantity;
        //numberOfJumps cannot reach 10
        if (numberOfJumps >= 10)
        {
            numberOfJumps = 9;
        }
        textJump.text = numberOfJumps.ToString();
    }

    /// <summary>
    /// Resets the numberOfJumps value to the default one
    /// </summary>
    public void JumpReset()
    {
        numberOfJumps = 3;
        textJump.text = numberOfJumps.ToString();
    }

    /// <summary>
    /// Keeps track of the player highest Y position
    /// </summary>
    private void HighestPosition()
    {
        if (player.transform.position.y > highestY)
        {
            highestY = player.transform.position.y;
        }
    }

    /// <summary>
    /// Rotates the camera one direction or another.
    /// </summary>
    //public void RotateCamera()
    //{
    //    if (canRotate)
    //    {
    //        //Smoothly transitions the camera from its actual rotation to the desired rotation
    //        cameraTr.rotation = Quaternion.Slerp(cameraTr.rotation, Quaternion.Euler(0, 0, rotateDestinationZ), rotateSpeed * Time.deltaTime);
//
    //        if (cameraTr.rotation.z <= -1)
    //        {
    //            canRotate = false;
    //            rotateDestinationZ = 0;
    //            StartCoroutine(CameraCooldown());
    //        }
    //        else if (cameraTr.rotation == Quaternion.Euler(0, 0, 0))
    //        {
    //            canRotate = false;
    //            rotateDestinationZ = 180f;
    //            StartCoroutine(CameraCooldown());
    //        }
    //    }
    //}

    /// <summary>
    /// Cooldown for the camera. It waits the desired amount of seconds and then sets canRotate to true
    /// </summary>
    private IEnumerator CameraCooldown()
    {
        yield return new WaitForSecondsRealtime(cameraCooldown);
        canRotate = true;
    }

    /// <summary>
    /// Function made so that other scripts can get the cameraLimits values
    /// </summary>
    public cameraLimits get_cameraLimits()
    {
        return cameraLimits;
    }
}

