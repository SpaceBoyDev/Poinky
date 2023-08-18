 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 using UnityEngine.UI;
 using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mousePaw;

    [SerializeField] private Toggle autoRetryToggle;
    [SerializeField] private Toggle infiniteJumpsToggle;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private GameObject mousePawInstance;
    
    /// <summary>
    /// Due to the timeScale being changed in the Game scene, this function does reset the timeScale to 1 on Awake
    /// </summary>
    private void Awake()
    {
        Application.targetFrameRate = 60;

        Time.timeScale = 1;

        LoadSettings();
    }

    private void Start()
    {
        mousePawInstance = Instantiate(mousePaw);
        mousePawInstance.SetActive(false);
        mousePawInstance.transform.SetParent(transform);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePawInstance.transform.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y + 80);
            mousePawInstance.SetActive(true);
        }
        else
        {
            mousePawInstance.SetActive(false);
        }
    }

    /// <summary>
    /// Function made to be accesible for the button. It loads the desired scene
    /// </summary>
    public void PlayButton()
    {
        SceneLoader.Instance.Load(1);
    }

    /// <summary>
    /// Function made to be accesible for the button. It closes the application
    /// </summary>
    //public void ExitButton()
    //{
    //    print("Exit game");
    //    Application.Quit();
    //}

    public void SaveSettings()
    {
        SaveData saveData = new SaveData();
        saveData.autoRetry = autoRetryToggle.isOn;
        saveData.infiniteJumps = infiniteJumpsToggle.isOn;
        SaveManager.SaveGameData(saveData);
    }

    public void LoadSettings()
    {
        SaveData saveData = SaveManager.LoadGameState();
        
        autoRetryToggle.isOn = saveData.autoRetry;
        infiniteJumpsToggle.isOn = saveData.infiniteJumps;
        bestScoreText.text = "Best Score:\n" + saveData.bestScore.ToString("F0") + "m.";
    }
}
