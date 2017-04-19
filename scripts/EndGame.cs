using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGame : MonoBehaviour {
    private bool gameOver = false;//contains whether or not the game has ended; true if a player has died
    private float timeDead = 0.0f;//countdown for restarting the level
    private PlayerHealth[] players;//references to both players
    private Renderer[] sChildRenderers;
    private Renderer[] iChildRenderers;
    private bool paused = false;//
    private float savedTimeScale = 1;
    private float startGameCountdown;//countdown for beginning the game
    public Canvas canvas;
    public GameObject ships;
    public GameObject icons;
    //private Text

	// Use this for initialization
	void Start () {
        //player1 = GameObject.Find("Players/Player1");
        //player2 = GameObject.Find("Players/Player2");
        canvas.enabled = false;
        players = GetComponentsInChildren<PlayerHealth>();
        Renderer[] sChildRenderers = ships.GetComponentsInChildren<Renderer>();
        Renderer[] iChildRenderers = icons.GetComponentsInChildren<Renderer>();
        startGameCountdown = Time.time + 6;
	}
	
	// Update is called once per frame
	void Update () {
        //if the game has been over for five seconds
	    if (gameOver && Time.time - timeDead > 1.0f)
        {
            //unload any unused assets
            Resources.UnloadUnusedAssets();
            gameOver = false;
            Time.timeScale = 1;

            //reload the scene
            SceneManager.LoadScene("battlescene");
        }

        //if a player died
        if (timeDead == 0.0f && (players[0] == null || players[1] == null))
        {
            //set gameOver flag and start the timer
            gameOver = true;
            timeDead = Time.time;

            if (players[0] == null && players[1] != null)
                players[1].GetComponent<PlayerHealth>().becomeInvincible();
            if (players[1] == null && players[0] != null)
                players[0].GetComponent<PlayerHealth>().becomeInvincible();
        }

        if (Input.GetButtonDown("Pause_Game"))
        {
            if (!paused && Time.time - startGameCountdown > 0 )
            {
                canvas.enabled = true;
                savedTimeScale = Time.timeScale;
                Time.timeScale = 0;
            }
            else
            {
                canvas.enabled = false;
                Time.timeScale = savedTimeScale;
            }
            paused = !paused;
        }

        if (Input.GetButtonDown("Quit_Game"))
        {
            //quit the application
            Resources.UnloadUnusedAssets();
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        if (gameOver && Time.timeScale > 0.1f)
        {
            Time.timeScale *= 0.95f;
        }
    }

    public float getStartGameCountdown()
    {
        return startGameCountdown;
    }
}