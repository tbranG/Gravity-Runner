using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float speed;
    public float max_distance;
    
    private float start_pos;
    private bool moving;

    void Awake()
    {
        start_pos = transform.position.x;
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) 
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);

            if (transform.position.x <= -max_distance)
            {
                transform.position = new Vector3(start_pos, transform.position.y);
            }
        }
        
        if (GameManager.gameManager.playerDead && moving)
            moving = false;
    }
}
