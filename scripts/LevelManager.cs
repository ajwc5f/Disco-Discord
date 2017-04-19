using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public Transform mainMenu, gameModesMenu, optionsMenu, soundsMenu, controlsMenu;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitScene()
    {
        Application.Quit();
    }

    public void GameModesMenu(bool clicked)
    {
        if (clicked == true)
        {
            gameModesMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            gameModesMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }

    public void OptionsMenu(bool clicked)
    {
        if (clicked == true)
        {
            optionsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            optionsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }
    public void SoundsMenu(bool clicked)
    {
        if (clicked == true)
        {
            soundsMenu.gameObject.SetActive(clicked);
            optionsMenu.gameObject.SetActive(false);
        }
        else
        {
            soundsMenu.gameObject.SetActive(clicked);
            optionsMenu.gameObject.SetActive(true);
        }
    }

    public void ControlsMenu(bool clicked)
    {
        if (clicked == true)
        {
            controlsMenu.gameObject.SetActive(clicked);
            optionsMenu.gameObject.SetActive(false);
        }
        else
        {
            controlsMenu.gameObject.SetActive(clicked);
            optionsMenu.gameObject.SetActive(true);
        }
    }
}
