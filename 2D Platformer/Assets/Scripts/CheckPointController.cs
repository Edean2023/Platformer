using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    // sets a value to redFlag
    public Sprite redFlag;
    // sets a value to greenFlag
    public Sprite greenFlag;
    private SpriteRenderer checkpointSpriteRenderer;
    // sets the bool for checkpointReached
    public bool checkpointReached;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        // Gets the sprite renderer
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // changes the checkpoint's color when the player touches it
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the colliding object has a tag labeled 'Player', do this
        if (other.tag == "Player")
        {
            // change the flag to green
            checkpointSpriteRenderer.sprite = greenFlag;
            // set checkpointReached to true
            checkpointReached = true;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

}
