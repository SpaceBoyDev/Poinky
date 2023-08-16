using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LineController : MonoBehaviour
{
    public PlayerInput playerInput;
    
    [FormerlySerializedAs("lineRenderer")] public LineRenderer dragLine;

    [FormerlySerializedAs("lineRenderer2")] public LineRenderer playerTrajectory;

    private Vector2 endPos;

    public Transform center;

    [SerializeField] private Vector2 playerTrajectoryOffset;

    [Header("New Trajectory")] [SerializeField]
    private int dotsNumber;

    [SerializeField] private GameObject dotsParent;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private float dotSpacing;
    private Transform[] dotsList;
    
    private Vector2 pos;
    private float timeStamp;

    /// <summary>
    /// Disables the lineRenderers so that they won't be displayed
    /// </summary>
    private void Start()
    {
        dragLine.enabled = false; 
        playerTrajectory.enabled= false;
        
        //NEW TRAJECTORY
        Hide();
        PrepareDots();
    }

    private void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;
        }
    }

    public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;

        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics.gravity.magnitude * timeStamp * timeStamp) / 2f;

            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    private void Show()
    {
        dotsParent.SetActive(true);
    }
    
    private void Hide()
    {
        dotsParent.SetActive(false);
    }

    /// <summary>
    /// Makes the calculations for the lineRenderers
    /// </summary>
    public void SetLine(Vector3 mousePosStart, Vector3 mousePosFinal)
    {
        //Drag line
        dragLine.SetPosition(0, mousePosStart);
        dragLine.SetPosition(1, mousePosFinal + new Vector3(0, 0, 10));

        //Trayectory line
        Vector2 mousePosStart2 = Vector2.zero;

        float distanceX = (mousePosStart.x- mousePosFinal.x) + mousePosStart2.x;
        float distanceY = (mousePosStart.y- mousePosFinal.y) + mousePosStart2.y;
        //Clamps the distance of the line so that the line has the same max and min distance as the force
        distanceY = Mathf.Clamp(distanceY, -1,1);
        distanceX = Mathf.Clamp(distanceX, -1, 1);
        Vector3 mousePosFinal2 = new Vector3(distanceX, distanceY, 0);

        //playerTrajectory.SetPosition(0, mousePosStart2 + playerTrajectoryOffset);
        //playerTrajectory.SetPosition(1, mousePosFinal2);
        
        //NEW TRAJECTORY
        UpdateDots(transform.position, playerInput.force * playerInput.power);
        
    }

    /// <summary>
    /// This function is accesible from other scripts and enables or disables the lineRenderers based on the given bool
    /// </summary>
    public void SetLines(bool isActive)
    {
        dragLine.enabled = isActive;
        //playerTrajectory.enabled = isActive;
        dotsParent.SetActive(isActive);
    }
}
