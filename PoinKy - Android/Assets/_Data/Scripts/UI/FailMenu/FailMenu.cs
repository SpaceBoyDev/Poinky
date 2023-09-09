using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public void RetryButton()
    {
        SceneLoader.Instance.Load(1);
    }

    public void ExitButton()
    {
        SceneLoader.Instance.Load(0);
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            GameMaster.Instance.SetIsInputAllowed(false);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            GameMaster.Instance.SetIsInputAllowed(true);
        }
    }
}
