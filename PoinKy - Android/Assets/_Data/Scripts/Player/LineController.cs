using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public LineRenderer lineRenderer2;

    private Vector2 endPos;

    public Transform center;

    /// <summary>
    /// Disables the lineRenderers so that they won't be displayed
    /// </summary>
    private void Start()
    {
        lineRenderer.enabled = false; 
        lineRenderer2.enabled= false;
    }

    /// <summary>
    /// Makes the calculations for the lineRenderers
    /// </summary>
    public void SetLine(Vector3 mousePosStart, Vector3 mousePosFinal)
    {
        //Drag line
        lineRenderer.SetPosition(0, mousePosStart);
        lineRenderer.SetPosition(1, mousePosFinal + new Vector3(0, 0, 10));

        //Trayectory line
        Vector3 mousePosStart2 = Vector3.zero;

        float distanceX = (mousePosStart.x- mousePosFinal.x) + mousePosStart2.x;
        float distanceY = (mousePosStart.y- mousePosFinal.y) + mousePosStart2.y;
        //Clamps the distance of the line so that the line has the same max and min distance as the force
        distanceY = Mathf.Clamp(distanceY, -1,1);
        distanceX = Mathf.Clamp(distanceX, -1, 1);
        Vector3 mousePosFinal2 = new Vector3(distanceX, distanceY, 0);

        lineRenderer2.SetPosition(0, mousePosStart2);
        lineRenderer2.SetPosition(1, mousePosFinal2);
    }

    /// <summary>
    /// This function is accesible from other scripts and enables or disables the lineRenderers based on the given bool
    /// </summary>
    public void SetLines(bool active)
    {
        lineRenderer.enabled = active;
        lineRenderer2.enabled = active;
    }
}
