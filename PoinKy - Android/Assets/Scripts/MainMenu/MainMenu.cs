using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Due to the timeScale being changed in the Game scene, this function does reset the timeScale to 1 on Awake
    /// </summary>
    private void Awake()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Function made to be accesible for the button. It loads the desired scene
    /// </summary>
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Function made to be accesible for the button. It closes the application
    /// </summary>
    public void ExitButton()
    {
        print("Exit game");
        Application.Quit();
    }
}
