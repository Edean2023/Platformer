using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // sets the x and y maximum 
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;

    // sets the x and y minimum
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMin;

    // sets a value to target
    private Transform target;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        // tells the camera to find the player and move with them
        target = GameObject.Find("Player").transform;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void LateUpdate()
    {
        // Makes sure that the camera doesn't follow past certain x and y max and mins
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////

}
