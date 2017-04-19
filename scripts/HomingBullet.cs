using UnityEngine;
using System.Collections;

public class HomingBullet : MonoBehaviour {
    public float bulletSpeedPercentage = 1.0f;
    public float vectorPercentage = 1.0f;

    private PlayerControl enemyPlayerControl;
    private float oppositeSide;
    private float adjacentSide;
    private float hypotenuse;
    private float v;//bullet velocity
    private float horizontal;
    private float vertical;
    private float velocityMagnitude;

	// Use this for initialization
	void Start () {
        //Destroy the object in 3.4 seconds if it doesn't hit anything
        Destroy(gameObject, 3.4f);

        if (gameObject.name == "Player1" && GameObject.Find("Players/Player2") != null)
            enemyPlayerControl = GameObject.Find("Players/Player2").GetComponent<PlayerControl>();
        else if (GameObject.Find("Players/Player1") != null)
            enemyPlayerControl = GameObject.Find("Players/Player1").GetComponent<PlayerControl>();

        if (enemyPlayerControl != null)
            v = enemyPlayerControl.getBulletVelocity() * bulletSpeedPercentage;

        calcVelocityComponents();

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal, vertical);
    }
	
	void FixedUpdate () {
        calcVelocityComponents();

        //set velocity
        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * Mathf.Abs(v), vertical * Mathf.Abs(v));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //ignore the player firing the bullet, since bullets begin inside of the ship
        if (col.gameObject.name != gameObject.name)//destroy the object once it collides with something.
        {
            if (col != null && col.gameObject.layer == 8)
                col.gameObject.GetComponent<PlayerHealth>().TakeDamage();

            Destroy(gameObject);
        }
    }

    void calcVelocityComponents()
    {
        if (enemyPlayerControl != null)
        {
            //calculate sides of the triangle
            oppositeSide = -(GetComponent<Rigidbody2D>().position.y - enemyPlayerControl.getPosition().y);
            adjacentSide = -(GetComponent<Rigidbody2D>().position.x - enemyPlayerControl.getPosition().x);
            hypotenuse = Mathf.Sqrt(oppositeSide * oppositeSide + adjacentSide * adjacentSide);

            //calculate the velocity magnitude
            velocityMagnitude = Mathf.Sqrt(GetComponent<Rigidbody2D>().velocity.x * GetComponent<Rigidbody2D>().velocity.x
                + GetComponent<Rigidbody2D>().velocity.y * GetComponent<Rigidbody2D>().velocity.y);

            //calculate the velocity components to use
            horizontal = (vectorPercentage * Mathf.Abs(v) * adjacentSide) / hypotenuse + GetComponent<Rigidbody2D>().velocity.x / velocityMagnitude;
            vertical = (vectorPercentage * Mathf.Abs(v) * oppositeSide) / hypotenuse + GetComponent<Rigidbody2D>().velocity.y / velocityMagnitude;
        }
    }
}
