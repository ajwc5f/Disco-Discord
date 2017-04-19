using UnityEngine;
using System.Collections;

public class BattleAudioScript : MonoBehaviour {
    private GameObject introMusic;
    private GameObject battleMusic;
    //private bool loop = false;

	// Use this for initialization
	void Start () {
        introMusic = GameObject.Find("battle_music_intro");
        battleMusic = GameObject.Find("battle_music");
    }
	
	// Update is called once per frame
	void Update () {
        //when the intro music finishes playing
        if (!introMusic.GetComponent<AudioSource>().isPlaying && !battleMusic.GetComponent<AudioSource>().isPlaying)
        {
            //play the battle music and stop the intro music
            battleMusic.GetComponent<AudioSource>().Play();
            introMusic.GetComponent<AudioSource>().Stop();
        }

        if (Input.GetButtonDown("Pause_Game"))
        {
            /*
            if (!introMusic.GetComponent<AudioSource>().isPlaying && !loop)
                introMusic.

            if (battleMusic.GetComponent<AudioSource>().isPlaying)
                battleMusic.GetComponent<AudioSource>().Pause();
            if (introMusic.GetComponent<AudioSource>().isPlaying)
                introMusic.GetComponent<AudioSource>().Pause();
            */
        }
	}
}
