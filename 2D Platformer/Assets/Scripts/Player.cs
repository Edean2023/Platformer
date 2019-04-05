using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // sets the rigid body
    private Rigidbody2D myRigidBody;
    // sets the animator
    private Animator myAnimator;

    [SerializeField]
    // Sets movement speed 
    private float movementSpeed;
    // sets the bool for facingRight
    private bool facingRight;

    // Allows me to set ground points for the player
    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    // describes what counts as ground
    [SerializeField]
    private LayerMask whatIsGround;

    // sets the starting number of lives the player has
    public int numLives = 3;

    // sets the bool for isGrounded
    private bool isGrounded;

    // sets the bool for jump
    private bool jump;

    // sets the jumping force for the player
    [SerializeField]
    private float jumpForce;

    // stores the position for the respawn point
    public Vector3 respawnPoint;

    public GameObject playerPrefab;

    GameObject playerInstance;

    public AudioSource jumpSound;
    public AudioSource deathSound;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        // player starts the game facing to the right
        facingRight = true;
        // Gets the rigid body
        myRigidBody = GetComponent<Rigidbody2D>();
        // Gets the animator
        myAnimator = GetComponent<Animator>();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    // Update is called once per frame
    private void Update()
    {
        HandleInput();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // FixedUpdate is called once per frame a fixed amount of times
    void FixedUpdate()
    {
        // gets the input for the horizontal axis
        float horizontal = Input.GetAxis("Horizontal");
        // sets 'isGrounded' to the 'IsGrounded' function
        isGrounded = IsGrounded();
        // calls the HandleMovement function
        HandleMovement(horizontal);
        // calls the Flip function
        Flip(horizontal);
        // calls the HandleLayers function
        HandleLayers();
        // calls the ResetValues function
        ResetValues();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Handles the movement of the player
    private void HandleMovement(float horizontal)
    {
        // if the rigid body has a y velocity of 0, do this
        if (myRigidBody.velocity.y < 0)
        {
            // sets the land trigger to true when the player is in the process of landing
            myAnimator.SetBool("land", true);
        }

        myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);
        // starts the running animation when the player starts moving
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        // if the player is grounded and jump is true then do this
        if (isGrounded && jump)
        {
            // the player is no longer grounded
            isGrounded = false;
            myRigidBody.AddForce(new Vector2(0, jumpForce));
            // plays the jumping animation when the player is jumping
            myAnimator.SetTrigger("jump");

        }

    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // handles the players inputs
    private void HandleInput()
    {
        // if the spacebar is pressed do this
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpSound.Play();
            // sets jump to true
            jump = true;

        }
                
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // flips the character depending on what direction they are moving
    private void Flip(float horizontal)
    {
        // if horizontal is greater than 0 and the player is not facing right, or horizontal is less than 0 and the player is facing right then do this.
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            // Flips the character
            facingRight = !facingRight;
            // Changes the scale of the player
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // resets values so they don't go on forever
    private void ResetValues()
    {
        // stops the player from jumping when they aren't hitting spacebar
        jump = false;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // checks to see if the player is touching the ground
    private bool IsGrounded()
    {
        // if the players rigid bodys y velocity is 0 then do this
        if (myRigidBody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    // Return true if the player is colliding with something else
                    if (colliders[i].gameObject != gameObject)
                    {
                        // resets the jump trigger
                        myAnimator.ResetTrigger("jump");
                        // sets land to false once the player has landed
                        myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        // return false if the player isn't colliding with anything
        return false;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // handles the different layers of animation
    private void HandleLayers()
    {
        // if the player is not grounded to this
        if (!isGrounded)
        {
            // set the air layer weight to 1 to show the jumping and landing animations
            myAnimator.SetLayerWeight(1, 1);
        }
        // otherwise do this
        else
        {
            // set the air layer weight back to 0 to show the ground animations
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the player collides with an object tagged 'KillZone', do this
        if (other.tag == "KillZone")
        {
            // takes away a life
            numLives--;
            // plays the death sound
            deathSound.Play();
            // spawn the player at a checkpoint
            transform.position = respawnPoint;
        }
        // if the player collieds with an object tagged 'Checkpoint', do this
        if (other.tag == "Checkpoint")
        {
            // sets the respawn point to the checkpoint
            respawnPoint = other.transform.position;
        }
        // if the player collides with an object tagged 'WinZone', do this
        if (other.tag == "WinZone")
        {
            // change the scene to the win screen
            SceneManager.LoadScene("Win");
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    void OnGUI()
    {
        if (numLives > 0 || playerInstance != null)
        {
            GUI.Label(new Rect(0, 0, 100, 50), "Lives: " + numLives);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
            Destroy(gameObject);

        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    




}

