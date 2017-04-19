using UnityEngine;
using System.Collections;

public class IconCooldowns : MonoBehaviour {
    public string gunButton = "Fire1_P1";
    public string player = "Player1";
    public int bulletPatternValue = 0;

    private SpriteRenderer[] sprites;
    private SpriteRenderer sprite;
    private float timer;
    private GameObject playerObject;
    private PlayerControl playerControl;
    private float fireTime;//Total time for the bullet pattern to begin and finish firing
    private EndGame endGameObject;

	// Use this for initialization
	void Start () {
        //grab the EndGame object to prevent cooldowns from shifting during the start game countdown
        endGameObject = GameObject.Find("Players").GetComponent<EndGame>();

        //grab the sprite
        sprites = GetComponentsInChildren<SpriteRenderer>();
        sprite = sprites[1];

        playerObject = GameObject.Find("Players/" + player);
        playerControl = playerObject.GetComponent<PlayerControl>();

        fireTime = playerControl.getBulletCastTime(bulletPatternValue);

        timer = -fireTime;
    }
	
	// Update is called once per frame
	void Update () {
        //if the corresponding button was pressed, and the player is allowed to use that ability, and the player isn't dead, and the game has started
	    if(Input.GetButtonDown(gunButton) && !playerControl.bulletPatternBeingCast() && playerControl.canCastBulletPattern(bulletPatternValue)
            && playerObject != null && endGameObject.getStartGameCountdown() < Time.time)
        {
            //grab a timer
            timer = Time.time;
        }

        //while the bullet pattern is being fired
        if( timer + fireTime > Time.time && sprite != null)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 60f / 255);
            
            //adjust the bar according to how much time is left
            sprite.transform.localScale = new Vector3(2.9f, 350f * (Time.time - timer) / fireTime);
            sprite.transform.localPosition = new Vector3(0, -1.75f + (1.75f * ( (Time.time - timer) / fireTime ) ) );   
        }

        //if the cooldown has finished
        if (timer + fireTime <= Time.time)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 100f / 255);
            timer = -fireTime;
        }
    }
}
