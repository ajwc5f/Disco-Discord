using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseManager : MonoBehaviour {
    public Transform pauseMenu;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitScene()
    {
        Application.Quit();
    }

    /*public void PauseMenuResume(bool clicked)
    {
        if (clicked == true)
        {
            pauseMenuResume.gameObject.SetActive(clicked);
            pauseMenu.gameObject.SetActive(false);
        }
        else
        {
            pauseMenuResume.gameObject.SetActive(clicked);
            pauseMenu.gameObject.SetActive(true);
        }
    }*/
}
