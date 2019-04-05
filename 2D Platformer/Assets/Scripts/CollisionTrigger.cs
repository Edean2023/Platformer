using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    // sets the player collider
    private BoxCollider2D playerCollider;

    // sets the platform collider
    [SerializeField]
    private BoxCollider2D platformCollider;

    // sets the platform trigger
    [SerializeField]
    private BoxCollider2D platformTrigger;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    // Start is called before the first frame update
    void Start()
    {
        // tells the game that the player is supposed to be able to stand on a certain platform
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        // tells the game to ignore the collision between the two box colliders on the platforms
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);

        
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // tells the platforms to ignore the player when the player runs into them
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the game object that is colliding with this is named 'Player' do this
        if (other.gameObject.name == "Player")
        {
            // platforms ignore the player when they run into them
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // enables the platforms to be stepped on after they have been run into
    private void OnTriggerExit2D(Collider2D other)
    {
        // if the game object that is colliding with this is named 'Player' do this
        if (other.gameObject.name == "Player")
        {
            // platforms recognize the player after they have run through the platform
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }
}
