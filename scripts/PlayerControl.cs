using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float playerSpeed = 10f;//how quickly the player moves
    public float bulletVelocity = 7f;//how quickly bullets travel
    public string player = "P1";
    public string horizontalCtrl = "Horizontal_P1";//grab input for player
    public string verticalCtrl = "Vertical_P1";
    public string gunButton1 = "Fire1_P1";
    public string gunButton2 = "Fire2_P1";
    public string gunButton3 = "Fire3_P1";
    public Rigidbody2D bullet;//a bullet object for creating instances (copies)
    public Rigidbody2D homingBullet;//a homing bullet object for creating instances

    private Rigidbody2D bulletInstance;
    private float bulletInterval = .375f;
    private float lastBullet;
    private float[] bp_timers = { 0, 0, 0 };//bullet pattern timers, both for execution and cooldown; ALWAYS INITIALIZE TO 0
    private float[] bp_interval = { 0.29f, 0.08f, 0.4f };//time between each bullet pattern fires
    private int[] bp_bulletsToFire = { 5, 6, 3 };//number of rounds to fire for each bullet pattern
    private float[] bp_waves = { 0, 0, 0 };//used to keep track of how many waves of bullets have been fired; ALWAYS INITIALIZE TO 0
    private float[] bp_cooldown = { 5.0f, 4.0f, 7.0f };//how long the player must wait to use the bullet pattern
    //private float countdown;
    private EndGame endGameObject;//for checking the countdown
    private bool volumeChange = true;

    //upon startup
    void Awake()
    {
        endGameObject = GetComponentInParent<EndGame>();
        //endGameObject = GameObject.Find("Players");
        //countdown = Time.time + 6;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.V) && AudioListener.volume < 1 && volumeChange)
        {
            volumeChange = false;
            AudioListener.volume += 0.1f;
        }
        if (Input.GetKey(KeyCode.C) && AudioListener.volume > 0 && volumeChange)
        {
            volumeChange = false;
            AudioListener.volume -= 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.V) || Input.GetKeyUp(KeyCode.C))
            volumeChange = true;

        if (AudioListener.volume < 0)
            AudioListener.volume = 0;
        if (AudioListener.volume > 1)
            AudioListener.volume = 1;


        if (bp_waves[0] == 0 && bp_waves[1] == 0 && bp_waves[2] == 0 && endGameObject.getStartGameCountdown() < Time.time)//&& countdown < Time.time)//if no patterns are currently being used...
        {
            //check for input for any available bullet pattern, and begin firing if able and input was received
            if (Input.GetButtonDown(gunButton1) && bp_timers[0] <= Time.time)
            {
                bp_waves[0] = bp_bulletsToFire[0];
            }
            if (Input.GetButtonDown(gunButton2) && bp_timers[1] <= Time.time)
            {
                bp_waves[1] = bp_bulletsToFire[1];
            }
            if (Input.GetButtonDown(gunButton3) && bp_timers[2] <= Time.time)
            {
                bp_waves[2] = bp_bulletsToFire[2];
            }
        }
    }

    void FixedUpdate()
    {
        //use bullet pattern 1 if activated
        if (bp_waves[0] > 0 && Time.time - bp_timers[0] > bp_interval[0])
        {
            CreateBullet(0.1f);
            CreateBullet(-0.1f);
            CreateBullet(0.23f);
            CreateBullet(-0.23f);

            bp_timers[0] = Time.time;
            bp_waves[0]--;

            //if the bullet pattern is done firing
            if (bp_waves[0] == 0)
                bp_timers[0] = Time.time + bp_cooldown[0];
        }

        //use bullet pattern 2 if activated
        if (bp_waves[1] > 0 && Time.time - bp_timers[1] > bp_interval[1])
        {
            //creates the bullets for the bullet pattern.
            CreateBullet(bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f / 6 + bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f * 2f / 6 + bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f * 3f / 6 + bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f * 4f / 6 + bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f * 5f / 6 + bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f + bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f * 7f / 6 + bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f * 8f / 6 + bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f * 9f / 6 + bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f * 10f / 6 + bp_waves[1] / bp_bulletsToFire[1]);
            CreateBullet(3.14f * 11f / 6 + bp_waves[1] / bp_bulletsToFire[1]);

            //store the current time for firing the next wave of the bullet pattern
            bp_timers[1] = Time.time;
            //decrement the number of waves to fire
            bp_waves[1]--;

            //if we're out of waves, the bullet pattern ends and goes on cooldown
            if (bp_waves[1] == 0)
                bp_timers[1] = Time.time + bp_cooldown[1];
        }

        //use bullet pattern 2 if activated.
        if (bp_waves[2] > 0 && Time.time - bp_timers[2] > bp_interval[2])
        {
            //create a homing missile
            CreateHomingBullet(0);

            //store the current time for firing the next wave of the bullet pattern
            bp_timers[2] = Time.time;
            //decrement the number of waves to fire
            bp_waves[2]--;

            //if we're out of waves, the bullet pattern ends and goes on cooldown
            if (bp_waves[2] == 0)
                bp_timers[2] = Time.time + bp_cooldown[2];
        }

        //fire a bullet straight forward
        if (Time.time - lastBullet > bulletInterval && endGameObject.getStartGameCountdown() < Time.time)
        {
            //Create the bullet
            CreateBullet(0.0f);
            //Store the current time for firing the next wave of the bullet pattern
            lastBullet = Time.time;
        }

        //cache horizontal and vertical input
        float h = Input.GetAxis(horizontalCtrl);
        float v = Input.GetAxis(verticalCtrl);

        GetComponent<Rigidbody2D>().velocity = new Vector2(h * playerSpeed, v * playerSpeed);
    }

    //Creates a regular bullet and fires it at the given angle. The bullet will move in a straight line.
    void CreateBullet(float angle)
    {
        bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        bulletInstance.velocity = new Vector2(-Mathf.Cos(3.14f - angle) * bulletVelocity, Mathf.Sin(angle) * bulletVelocity);
        bulletInstance.GetComponent<AudioSource>().pitch *= Time.timeScale;
        bulletInstance.name = gameObject.name;
        //bulletInstance.transform.parent = this.transform;
    }

    //Creates a homing bullet and fires it at the given angle.
    void CreateHomingBullet(float angle)
    {
        bulletInstance = Instantiate(homingBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        bulletInstance.velocity = new Vector2(-Mathf.Cos(3.14f - angle) * bulletVelocity, Mathf.Sin(angle) * bulletVelocity);
        bulletInstance.name = gameObject.name;
        //bulletInstance.transform.parent = this.transform;
    }

    //if any element in bp_waves is greater than zero, a bullet pattern is currently being fired.
    //Therefore, this function returns true if a bullet pattern is being cast. False otherwise.
    public bool bulletPatternBeingCast()
    {
        if (bp_waves[0] > 0 || bp_waves[1] > 0 || bp_waves[2] > 0)
            return true;
        return false;
    }

    //Checks if the specified bullet pattern can be cast.
    public bool canCastBulletPattern(int n)
    {
        if (bp_timers[n] > Time.time)
            return false;
        return true;
    }

    //calculates a bullet pattern's cast time by the variables given.
    public float getBulletCastTime(int n)
    {
        return bp_cooldown[n] + (bp_interval[n] * bp_bulletsToFire[n]);
    }

    //returns the position of the player.
    public Vector3 getPosition()
    {
        if(this != null)
            return gameObject.transform.position;
        return new Vector3();
    }

    public float getBulletVelocity()
    {
        return bulletVelocity;
    }
}
