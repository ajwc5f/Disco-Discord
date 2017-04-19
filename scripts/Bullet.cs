using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	// Use this for initialization
	void Start () {
        //destroy the bullet if five seconds have gone by and it still isn't destroyed yet.
        Destroy (gameObject, 5);
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //ignore the player firing the bullet, since bullets begin inside of the ship
        if (col.gameObject.name != gameObject.name)//destroy the object once it collides with something.
        {
            if( col != null && col.gameObject.layer == 8 )
                col.gameObject.GetComponent<PlayerHealth>().TakeDamage();

            Destroy(gameObject);
        }
    }
}
