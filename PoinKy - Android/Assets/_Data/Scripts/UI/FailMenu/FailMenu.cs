using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailMenu : MonoBehaviour
{
    public void RetryButton()
    {
        SceneLoader.Instance.Load(1);
    }

    public void ExitButton()
    {
        SceneLoader.Instance.Load(0);
    }
}
