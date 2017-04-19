using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public float health = 1f;//amount of damage a player can take before losing
    public string playerTag = "Player1";//the player to associate health to

    private Vector3 healthScale;
    private float timeDead;
    private bool invincible = false;

	// Use this for initialization
	void Start () {

	}

    public void TakeDamage()
    {
        //a player becomes invincible when the other player is defeated
        if (!invincible)
            health--;

        if (health <= 0)
            Death();
    }

    void Death()
    {
        //disable player input
        GetComponentInParent<PlayerControl>().GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponentInParent<PlayerControl>().enabled = false;

        //destroy this game object
        Destroy(gameObject);
    }

    //call when the other player is defeated
    public void becomeInvincible()
    {
        invincible = true;
    }
}
