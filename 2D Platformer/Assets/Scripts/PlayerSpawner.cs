using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject playerInstance;

    public int numLives = 4;

    float respawnTimer = 1;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    void SpawnPlayer()
    {
        // removes one life once the player spawns (This should make it where the player has 3 lives total)
        numLives--;
        // respawn timer in secconds
        respawnTimer = 1;
        // returns the player to the spawn location
        playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
        playerInstance.name = "Player";
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Update is called once per frame
    void Update()
    {
        // tells when to respawn the player
        if (playerInstance == null && numLives > 0)
        {

            respawnTimer -= Time.deltaTime;

            if (respawnTimer <= 0)
            {
                SpawnPlayer();
            }
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
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "GAME OVER");
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    
}
