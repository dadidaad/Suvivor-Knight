using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.activeInHierarchy == false)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
        
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
        TimeManager.StartGame();
    }
    
    public void OpenMenu()
    {
        panel.SetActive(true);
        TimeManager.PasueGame();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
